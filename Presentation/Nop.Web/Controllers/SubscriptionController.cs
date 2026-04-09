using Iyzipay;
using Iyzipay.Model;
using Iyzipay.Request;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Nop.Core;
using Nop.Core.Domain.Messages;
using Nop.Data;
using Nop.Services.Common;
using Nop.Services.Customers;
using Nop.Services.Messages;
using Nop.Web.Framework.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// PEKIN_CUSTOM: iyzico tek seferlik ödeme — payment token ile müşteri tanıma, 1/2/3 yıllık plan
namespace Nop.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [IgnoreAntiforgeryToken]
    [EnableCors("AllowMarketingSite")]
    public class SubscriptionController : BasePublicController
    {
        private static readonly Dictionary<string, (string Label, decimal Price, int Months)> Plans = new()
        {
            ["1y"] = ("1 Yıllık",  1990m, 12),
            ["2y"] = ("2 Yıllık",  3490m, 24),
            ["3y"] = ("3 Yıllık",  4990m, 36),
        };

        private readonly IWorkContext _workContext;
        private readonly ICustomerService _customerService;
        private readonly IGenericAttributeService _genericAttributeService;
        private readonly IEmailAccountService _emailAccountService;
        private readonly IQueuedEmailService _queuedEmailService;
        private readonly INopDataProvider _dataProvider;
        private readonly Options _iyzicoOptions;
        private readonly string _callbackUrl;
        private readonly string _frontendUrl;

        public SubscriptionController(
            IWorkContext workContext,
            ICustomerService customerService,
            IGenericAttributeService genericAttributeService,
            IEmailAccountService emailAccountService,
            IQueuedEmailService queuedEmailService,
            INopDataProvider dataProvider,
            IConfiguration configuration)
        {
            _workContext = workContext;
            _customerService = customerService;
            _genericAttributeService = genericAttributeService;
            _emailAccountService = emailAccountService;
            _queuedEmailService = queuedEmailService;
            _dataProvider = dataProvider;

            _iyzicoOptions = new Options
            {
                ApiKey    = configuration["IyzicoConfig:ApiKey"],
                SecretKey = configuration["IyzicoConfig:SecretKey"],
                BaseUrl   = configuration["IyzicoConfig:BaseUrl"]
            };
            _callbackUrl = configuration["IyzicoConfig:CallbackUrl"];
            _frontendUrl = configuration["IyzicoConfig:FrontendUrl"] ?? "https://pekinteknoloji.com";
        }

        // Plan listesi (fiyatlar sayfası için)
        [HttpGet("plans")]
        public IActionResult GetPlans()
        {
            var result = new List<object>();
            foreach (var kv in Plans)
                result.Add(new { key = kv.Key, label = kv.Value.Label, price = kv.Value.Price, months = kv.Value.Months });
            return Ok(result);
        }

        // iyzico checkout başlat
        // Session ile: POST /api/subscription/checkout?plan=1y
        // Token ile:   POST /api/subscription/checkout?plan=1y&paymentToken=xxx
        [HttpPost("checkout")]
        public async Task<IActionResult> CreateCheckout([FromQuery] string plan, [FromQuery] string paymentToken = null)
        {
            if (string.IsNullOrEmpty(plan) || !Plans.TryGetValue(plan, out var selectedPlan))
                return BadRequest(new { success = false, message = "Geçersiz plan. 1y, 2y veya 3y olmalıdır." });

            // Müşteriyi bul: önce payment token, yoksa session
            Nop.Core.Domain.Customers.Customer customer = null;

            if (!string.IsNullOrEmpty(paymentToken))
            {
                customer = await FindCustomerByPaymentTokenAsync(paymentToken);
                if (customer == null)
                    return BadRequest(new { success = false, message = "Geçersiz veya süresi dolmuş ödeme linki." });
            }
            else
            {
                customer = await _workContext.GetCurrentCustomerAsync();
                if (!await _customerService.IsRegisteredAsync(customer))
                    return Unauthorized(new { success = false, message = "Giriş yapmanız veya ödeme linki kullanmanız gerekiyor." });
            }

            var status = await _genericAttributeService.GetAttributeAsync<string>(customer, "SubscriptionStatus") ?? "trial";
            if (status == "active")
                return BadRequest(new { success = false, message = "Zaten aktif aboneliğiniz var." });

            var ip = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "85.34.78.112";
            var priceStr = selectedPlan.Price.ToString("F2", System.Globalization.CultureInfo.InvariantCulture);

            var request = new CreateCheckoutFormInitializeRequest
            {
                Locale         = Locale.TR.ToString(),
                ConversationId = $"sub_{customer.Id}_{plan}",
                Price          = priceStr,
                PaidPrice      = priceStr,
                Currency       = Currency.TRY.ToString(),
                BasketId       = $"basket_{customer.Id}_{DateTimeOffset.UtcNow.ToUnixTimeSeconds()}",
                PaymentGroup   = PaymentGroup.PRODUCT.ToString(),
                CallbackUrl    = _callbackUrl,
                Buyer = new Buyer
                {
                    Id                  = customer.Id.ToString(),
                    Name                = customer.FirstName ?? "Ad",
                    Surname             = customer.LastName  ?? "Soyad",
                    Email               = customer.Email,
                    IdentityNumber      = "11111111111",
                    GsmNumber           = customer.Phone ?? "+905000000000",
                    RegistrationAddress = "Türkiye",
                    City                = "Istanbul",
                    Country             = "Turkey",
                    Ip                  = ip
                },
                ShippingAddress = new Address
                {
                    ContactName = $"{customer.FirstName} {customer.LastName}".Trim(),
                    City        = "Istanbul",
                    Country     = "Turkey",
                    Description = "Dijital ürün"
                },
                BillingAddress = new Address
                {
                    ContactName = $"{customer.FirstName} {customer.LastName}".Trim(),
                    City        = "Istanbul",
                    Country     = "Turkey",
                    Description = "Fatura adresi"
                },
                BasketItems = new List<BasketItem>
                {
                    new BasketItem
                    {
                        Id        = plan,
                        Name      = $"Pekin Teknoloji {selectedPlan.Label} Abonelik",
                        Category1 = "Yazılım",
                        ItemType  = BasketItemType.VIRTUAL.ToString(),
                        Price     = priceStr
                    }
                }
            };

            var result = await CheckoutFormInitialize.Create(request, _iyzicoOptions);

            if (result.Status != "success")
                return BadRequest(new { success = false, message = result.ErrorMessage ?? "Ödeme formu oluşturulamadı." });

            // iyzico token → {customerId}_{plan} eşleştirmesini sakla (callback'te kullanılacak)
            await _genericAttributeService.SaveAttributeAsync(customer, $"IyzToken_{result.Token}", $"{customer.Id}_{plan}");
            // Aynı zamanda yeni bir token ile de sakla (Token kısa olabilir, güvenli taraf)
            await _dataProvider.ExecuteNonQueryAsync(
                $"INSERT INTO \"GenericAttribute\" (\"EntityId\", \"KeyGroup\", \"Key\", \"Value\", \"StoreId\", \"CreatedOrUpdatedDateUTC\") " +
                $"VALUES (0, 'IyzicoCheckout', 'token_{result.Token.Replace("'", "''")}', '{customer.Id}_{plan}', 0, NOW()) " +
                $"ON CONFLICT DO NOTHING");

            return Ok(new
            {
                success             = true,
                checkoutFormContent = result.CheckoutFormContent,
                token               = result.Token
            });
        }

        // Admin panelinden yapılan ödemelerin callback'i (public URL — auth gerektirmez)
        [HttpPost("admin-callback")]
        [HttpGet("admin-callback")]
        public Task<IActionResult> AdminCallback([FromForm] string token, [FromForm] string status)
            => CallbackCore(token, isAdmin: true);

        // iyzico callback — token ile müşteriyi ve planı bul
        [HttpPost("callback")]
        [HttpGet("callback")]
        public Task<IActionResult> Callback([FromForm] string token, [FromForm] string status)
            => CallbackCore(token, isAdmin: false);

        private async Task<IActionResult> CallbackCore(string token, bool isAdmin)
        {
            var returnBase = HttpContext.Request.Query["returnBase"].ToString();
            if (string.IsNullOrEmpty(returnBase)) returnBase = _callbackUrl?.Replace("/api/subscription/admin-callback", "") ?? "";
            var failUrl    = isAdmin ? $"{returnBase}/Admin/Subscription?result=fail"    : $"{_frontendUrl}/odeme?result=fail";
            var successUrl = isAdmin ? $"{returnBase}/Admin/Subscription?result=success" : $"{_frontendUrl}/hesabim?result=success";

            if (string.IsNullOrEmpty(token))
                return Redirect(failUrl);

            var paymentResult = await CheckoutForm.Retrieve(
                new RetrieveCheckoutFormRequest { Token = token },
                _iyzicoOptions);

            if (paymentResult.Status != "success" || paymentResult.PaymentStatus != "SUCCESS")
                return Redirect(failUrl);

            // DB'den token → customerId_plan eşleşmesini bul
            var rows = await _dataProvider.QueryAsync<CheckoutLookup>(
                $"SELECT \"Value\" AS \"Mapping\" FROM \"GenericAttribute\" " +
                $"WHERE \"KeyGroup\" = 'IyzicoCheckout' AND \"Key\" = 'token_{token.Replace("'", "''")}'");

            var mapping = rows.FirstOrDefault()?.Mapping;
            if (string.IsNullOrEmpty(mapping))
                return Redirect(failUrl);

            var parts = mapping.Split('_');
            if (parts.Length < 2 || !int.TryParse(parts[0], out var customerId))
                return Redirect(failUrl);

            var plan = parts[1];
            if (!Plans.TryGetValue(plan, out var selectedPlan))
                return Redirect(failUrl);

            var customer = await _customerService.GetCustomerByIdAsync(customerId);
            if (customer == null)
                return Redirect(failUrl);

            // Aboneliği aktif et
            var endDate = DateTime.UtcNow.AddMonths(selectedPlan.Months);
            await _genericAttributeService.SaveAttributeAsync(customer, "SubscriptionStatus",  "active");
            await _genericAttributeService.SaveAttributeAsync(customer, "SubscriptionEndDate", endDate);
            await _genericAttributeService.SaveAttributeAsync(customer, "SubscriptionPlan",    plan);
            await _genericAttributeService.SaveAttributeAsync(customer, "IyzicoPaymentId",     paymentResult.PaymentId);

            // Kullanılan token kaydını temizle
            await _dataProvider.ExecuteNonQueryAsync(
                $"DELETE FROM \"GenericAttribute\" WHERE \"KeyGroup\" = 'IyzicoCheckout' AND \"Key\" = 'token_{token.Replace("'", "''")}'");

            return Redirect(successUrl);
        }

        // Payment link maili gönder (FastRegisterController tarafından çağrılır)
        public async Task SendPaymentEmailAsync(Nop.Core.Domain.Customers.Customer customer, string storeName)
        {
            var accounts = await _emailAccountService.GetAllEmailAccountsAsync();
            var emailAccount = accounts.Count > 0 ? accounts[0] : null;
            if (emailAccount == null) return;

            // 30 günlük payment token üret
            var paymentToken = Guid.NewGuid().ToString("N");
            var expiry = DateTime.UtcNow.AddDays(30);
            await _genericAttributeService.SaveAttributeAsync(customer, "PaymentLinkToken",       paymentToken);
            await _genericAttributeService.SaveAttributeAsync(customer, "PaymentLinkTokenExpiry", expiry);

            var paymentUrl = $"{_frontendUrl}/odeme?token={paymentToken}";

            var body = $@"
                <p>Merhaba {customer.FirstName},</p>
                <p>Mağazanız <b>{storeName}</b> için deneme süreniz sona erdi.</p>
                <p>Mağazanıza erişmeye devam etmek için aşağıdaki butona tıklayarak abonelik planınızı seçebilirsiniz.</p>
                <br>
                <a href='{paymentUrl}' style='display:inline-block;background:#6366f1;color:#fff;padding:12px 32px;border-radius:8px;font-size:15px;font-weight:600;text-decoration:none;'>
                  Abonelik Planı Seç
                </a>
                <br><br>
                <p style='color:#6b7280;font-size:13px'>Bu link 30 gün geçerlidir.</p>
                <p>Sorularınız için <a href='mailto:bilgi@pekinteknoloji.com'>bilgi@pekinteknoloji.com</a></p>
                <p>İyi satışlar!<br><b>Pekin Teknoloji</b></p>";

            await _queuedEmailService.InsertQueuedEmailAsync(new QueuedEmail
            {
                Priority      = QueuedEmailPriority.High,
                From          = emailAccount.Email,
                FromName      = emailAccount.DisplayName,
                To            = customer.Email,
                ToName        = $"{customer.FirstName} {customer.LastName}",
                Subject       = $"Mağazanız için abonelik planı seçin — {storeName}",
                Body          = body,
                CreatedOnUtc  = DateTime.UtcNow,
                EmailAccountId = emailAccount.Id
            });
        }

        // Abonelik durumu
        [HttpGet("status")]
        public async Task<IActionResult> Status()
        {
            var customer = await _workContext.GetCurrentCustomerAsync();
            if (!await _customerService.IsRegisteredAsync(customer))
                return Unauthorized(new { success = false });

            var status   = await _genericAttributeService.GetAttributeAsync<string>(customer,    "SubscriptionStatus")  ?? "trial";
            var trialEnd = await _genericAttributeService.GetAttributeAsync<DateTime?>(customer, "TrialEndDate");
            var subEnd   = await _genericAttributeService.GetAttributeAsync<DateTime?>(customer, "SubscriptionEndDate");
            var plan     = await _genericAttributeService.GetAttributeAsync<string>(customer,    "SubscriptionPlan");

            int? trialDaysRemaining = null;
            if (trialEnd.HasValue)
                trialDaysRemaining = Math.Max(0, (int)(trialEnd.Value - DateTime.UtcNow).TotalDays);

            return Ok(new
            {
                success             = true,
                status,
                trialEndDate        = trialEnd?.ToString("dd.MM.yyyy"),
                trialDaysRemaining,
                subscriptionEndDate = subEnd?.ToString("dd.MM.yyyy"),
                plan,
                isTrialExpired      = trialDaysRemaining == 0 && status == "trial"
            });
        }

        private async Task<Nop.Core.Domain.Customers.Customer> FindCustomerByPaymentTokenAsync(string token)
        {
            var rows = await _dataProvider.QueryAsync<TokenCustomer>(
                $"SELECT \"EntityId\" AS \"CustomerId\", \"Value\" AS \"Expiry\" FROM \"GenericAttribute\" " +
                $"WHERE \"KeyGroup\" = 'Customer' AND \"Key\" = 'PaymentLinkToken' AND \"Value\" = '{token.Replace("'", "''")}'");

            var row = rows.FirstOrDefault();
            if (row == null) return null;

            // Expiry kontrolü
            var expiryRows = await _dataProvider.QueryAsync<TokenCustomer>(
                $"SELECT \"EntityId\" AS \"CustomerId\", \"Value\" AS \"Expiry\" FROM \"GenericAttribute\" " +
                $"WHERE \"KeyGroup\" = 'Customer' AND \"Key\" = 'PaymentLinkTokenExpiry' AND \"EntityId\" = {row.CustomerId}");

            var expiryRow = expiryRows.FirstOrDefault();
            if (expiryRow != null && DateTime.TryParse(expiryRow.Expiry, out var expiry) && expiry < DateTime.UtcNow)
                return null;

            return await _customerService.GetCustomerByIdAsync(row.CustomerId);
        }

        private class CheckoutLookup { public string Mapping { get; set; } }
        private class TokenCustomer  { public int CustomerId  { get; set; } public string Expiry { get; set; } }
    }
}
