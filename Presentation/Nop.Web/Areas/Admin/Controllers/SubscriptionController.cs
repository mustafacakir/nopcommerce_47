using Iyzipay;
using Iyzipay.Model;
using Iyzipay.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Nop.Core;
using Nop.Services.Common;
using Nop.Services.Customers;
using Nop.Services.Security;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

// PEKIN_CUSTOM: Admin paneli içinden iyzico ödeme — StoreOwner trial sonrası abonelik satın alır
namespace Nop.Web.Areas.Admin.Controllers
{
    [AuthorizeAdmin]
    [Area(AreaNames.ADMIN)]
    public class SubscriptionController : BaseAdminController
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
        private readonly IPermissionService _permissionService;
        private readonly Options _iyzicoOptions;
        private readonly string _callbackBaseUrl;

        public SubscriptionController(
            IWorkContext workContext,
            ICustomerService customerService,
            IGenericAttributeService genericAttributeService,
            IPermissionService permissionService,
            IConfiguration configuration)
        {
            _workContext = workContext;
            _customerService = customerService;
            _genericAttributeService = genericAttributeService;
            _permissionService = permissionService;

            _iyzicoOptions = new Options
            {
                ApiKey    = configuration["IyzicoConfig:ApiKey"],
                SecretKey = configuration["IyzicoConfig:SecretKey"],
                BaseUrl   = configuration["IyzicoConfig:BaseUrl"]
            };
            _callbackBaseUrl = configuration["IyzicoConfig:AdminCallbackBaseUrl"]
                               ?? configuration["IyzicoConfig:CallbackUrl"]?.Replace("/api/subscription/callback", "")
                               ?? "https://pekinteknoloji.com";
        }

        // GET /Admin/Subscription
        public async Task<IActionResult> Index()
        {
            var customer = await _workContext.GetCurrentCustomerAsync();
            var status   = await _genericAttributeService.GetAttributeAsync<string>(customer,    "SubscriptionStatus")  ?? "trial";
            var trialEnd = await _genericAttributeService.GetAttributeAsync<DateTime?>(customer, "TrialEndDate");
            var subEnd   = await _genericAttributeService.GetAttributeAsync<DateTime?>(customer, "SubscriptionEndDate");
            var plan     = await _genericAttributeService.GetAttributeAsync<string>(customer,    "SubscriptionPlan");

            int? daysRemaining = null;
            if (trialEnd.HasValue)
                daysRemaining = Math.Max(0, (int)(trialEnd.Value - DateTime.UtcNow).TotalDays);

            ViewBag.Status          = status;
            ViewBag.TrialEndDate    = trialEnd?.ToString("dd.MM.yyyy");
            ViewBag.DaysRemaining   = daysRemaining;
            ViewBag.SubEndDate      = subEnd?.ToString("dd.MM.yyyy");
            ViewBag.Plan            = plan;
            ViewBag.Plans           = Plans;

            return View();
        }

        // POST /Admin/Subscription/Checkout?plan=1y
        [HttpPost]
        public async Task<IActionResult> Checkout(string plan)
        {
            if (string.IsNullOrEmpty(plan) || !Plans.TryGetValue(plan, out var selectedPlan))
                return BadRequest("Geçersiz plan.");

            var customer = await _workContext.GetCurrentCustomerAsync();
            var status   = await _genericAttributeService.GetAttributeAsync<string>(customer, "SubscriptionStatus") ?? "trial";

            if (status == "active")
                return BadRequest("Zaten aktif aboneliğiniz var.");

            var ip      = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "85.34.78.112";
            var price   = selectedPlan.Price.ToString("F2", System.Globalization.CultureInfo.InvariantCulture);
            var returnBase  = $"{Request.Scheme}://{Request.Host}";
            var callbackUrl = $"{_callbackBaseUrl}/api/subscription/admin-callback?returnBase={Uri.EscapeDataString(returnBase)}";

            var request = new CreateCheckoutFormInitializeRequest
            {
                Locale         = Locale.TR.ToString(),
                ConversationId = $"sub_{customer.Id}_{plan}",
                Price          = price,
                PaidPrice      = price,
                Currency       = Currency.TRY.ToString(),
                BasketId       = $"basket_{customer.Id}_{DateTimeOffset.UtcNow.ToUnixTimeSeconds()}",
                PaymentGroup   = PaymentGroup.PRODUCT.ToString(),
                CallbackUrl    = callbackUrl,
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
                        Price     = price
                    }
                }
            };

            var result = await CheckoutFormInitialize.Create(request, _iyzicoOptions);

            if (result.Status != "success")
                return Json(new { success = false, message = result.ErrorMessage ?? "Ödeme formu oluşturulamadı." });

            // iyzico token → customerId_plan kaydı (public callback'te okunur)
            await Nop.Core.Infrastructure.EngineContext.Current
                .Resolve<Nop.Data.INopDataProvider>()
                .ExecuteNonQueryAsync(
                    $"INSERT INTO \"GenericAttribute\" (\"EntityId\", \"KeyGroup\", \"Key\", \"Value\", \"StoreId\", \"CreatedOrUpdatedDateUTC\") " +
                    $"VALUES (0, 'IyzicoCheckout', 'token_{result.Token.Replace("'", "''")}', '{customer.Id}_{plan}', 0, NOW()) " +
                    $"ON CONFLICT DO NOTHING");

            return Json(new { success = true, checkoutFormContent = result.CheckoutFormContent });
        }

        // Callback artık public SubscriptionController'da: /subscription/admin-callback
    }
}
