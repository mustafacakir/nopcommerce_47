using Iyzipay;
using Iyzipay.Model;
using Iyzipay.Model.V2.Subscription;
using Iyzipay.Request.V2.Subscription;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Nop.Core;
using Nop.Services.Common;
using Nop.Services.Customers;
using Nop.Web.Framework.Controllers;
using System;
using System.Threading.Tasks;

// PEKIN_CUSTOM: iyzico abonelik yönetimi — checkout başlatma, callback, durum sorgulama
namespace Nop.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [IgnoreAntiforgeryToken]
    [EnableCors("AllowMarketingSite")]
    public class SubscriptionController : BasePublicController
    {
        private readonly IWorkContext _workContext;
        private readonly ICustomerService _customerService;
        private readonly IGenericAttributeService _genericAttributeService;
        private readonly Options _iyzicoOptions;
        private readonly string _callbackUrl;
        private readonly string _pricingPlanReferenceCode;
        private readonly string _frontendUrl;

        public SubscriptionController(
            IWorkContext workContext,
            ICustomerService customerService,
            IGenericAttributeService genericAttributeService,
            IConfiguration configuration)
        {
            _workContext = workContext;
            _customerService = customerService;
            _genericAttributeService = genericAttributeService;

            _iyzicoOptions = new Options
            {
                ApiKey = configuration["IyzicoConfig:ApiKey"],
                SecretKey = configuration["IyzicoConfig:SecretKey"],
                BaseUrl = configuration["IyzicoConfig:BaseUrl"]
            };
            _callbackUrl = configuration["IyzicoConfig:CallbackUrl"];
            _pricingPlanReferenceCode = configuration["IyzicoConfig:PricingPlanReferenceCode"] ?? "";
            _frontendUrl = configuration["IyzicoConfig:FrontendUrl"] ?? "https://pekinteknoloji.com";
        }

        // iyzico checkout formu başlatır; React bu endpoint'i çağırıp dönen HTML'i sayfaya embed eder
        [HttpPost("checkout")]
        public async Task<IActionResult> CreateCheckout()
        {
            var customer = await _workContext.GetCurrentCustomerAsync();
            if (!await _customerService.IsRegisteredAsync(customer))
                return Unauthorized(new { success = false, message = "Giriş yapmanız gerekiyor." });

            var subscriptionStatus = await _genericAttributeService.GetAttributeAsync<string>(customer, "SubscriptionStatus") ?? "trial";
            if (subscriptionStatus == "active")
                return BadRequest(new { success = false, message = "Zaten aktif aboneliğiniz var." });

            if (string.IsNullOrEmpty(_pricingPlanReferenceCode))
                return StatusCode(500, new { success = false, message = "Abonelik planı henüz yapılandırılmamış. Lütfen yöneticinizle iletişime geçin." });

            var request = new InitializeCheckoutFormRequest
            {
                Locale = Locale.TR.ToString(),
                ConversationId = customer.Id.ToString(),
                CallbackUrl = _callbackUrl,
                PricingPlanReferenceCode = _pricingPlanReferenceCode,
                Customer = new CheckoutFormCustomer
                {
                    Name = customer.FirstName ?? "",
                    Surname = customer.LastName ?? "",
                    Email = customer.Email,
                    GsmNumber = customer.Phone ?? "",
                    IdentityNumber = "11111111111", // iyzico zorunlu kılıyor; gerçek TC sandbox'ta önemli değil
                    BillingAddress = new Address
                    {
                        ContactName = $"{customer.FirstName} {customer.LastName}".Trim(),
                        City = "Istanbul",
                        Country = "Turkey",
                        Description = "Fatura Adresi"
                    }
                }
            };

            var result = Subscription.InitializeCheckoutForm(request, _iyzicoOptions);

            if (result.Status != "success")
                return BadRequest(new { success = false, message = result.ErrorMessage ?? "Ödeme formu oluşturulamadı." });

            return Ok(new
            {
                success = true,
                checkoutFormContent = result.CheckoutFormContent,
                token = result.Token
            });
        }

        // iyzico ödeme sonrası bu URL'e POST atar; aboneliği aktif eder
        [HttpPost("callback")]
        [HttpGet("callback")]
        public async Task<IActionResult> Callback([FromForm] string token, [FromForm] string conversationId, [FromQuery] string token2 = null)
        {
            var resolvedToken = token ?? token2;
            if (string.IsNullOrEmpty(resolvedToken) && string.IsNullOrEmpty(conversationId))
                return Redirect($"{_frontendUrl}/odeme?result=fail&reason=missing_token");

            // ConversationId'den müşteriyi bul (InitializeCheckoutForm'da customer.Id set edildi)
            if (!int.TryParse(conversationId, out var customerId))
                return Redirect($"{_frontendUrl}/odeme?result=fail&reason=invalid_conversation");

            var customer = await _customerService.GetCustomerByIdAsync(customerId);
            if (customer == null)
                return Redirect($"{_frontendUrl}/odeme?result=fail&reason=customer_not_found");

            // Aboneliği aktif et
            await _genericAttributeService.SaveAttributeAsync(customer, "SubscriptionStatus", "active");
            await _genericAttributeService.SaveAttributeAsync(customer, "SubscriptionStartDate", DateTime.UtcNow);

            if (!string.IsNullOrEmpty(resolvedToken))
                await _genericAttributeService.SaveAttributeAsync(customer, "IyzicoSubscriptionToken", resolvedToken);

            return Redirect($"{_frontendUrl}/hesabim?result=success");
        }

        // Mevcut abonelik durumunu döner
        [HttpGet("status")]
        public async Task<IActionResult> Status()
        {
            var customer = await _workContext.GetCurrentCustomerAsync();
            if (!await _customerService.IsRegisteredAsync(customer))
                return Unauthorized(new { success = false });

            var status = await _genericAttributeService.GetAttributeAsync<string>(customer, "SubscriptionStatus") ?? "trial";
            var trialEndDate = await _genericAttributeService.GetAttributeAsync<DateTime?>(customer, "TrialEndDate");

            int? daysRemaining = null;
            if (trialEndDate.HasValue)
                daysRemaining = Math.Max(0, (int)(trialEndDate.Value - DateTime.UtcNow).TotalDays);

            return Ok(new
            {
                success = true,
                status,
                trialEndDate = trialEndDate?.ToString("dd.MM.yyyy"),
                daysRemaining,
                isExpired = daysRemaining == 0 && status != "active"
            });
        }
    }
}
