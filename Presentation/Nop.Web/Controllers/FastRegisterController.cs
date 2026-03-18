using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Nop.Core;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Security;
using Nop.Core.Domain.Stores;
using Nop.Services.Common;
using Nop.Services.Customers;
using Nop.Core.Domain.Messages;
using Nop.Services.Messages;
using Nop.Services.Configuration;
using Nop.Services.Security;
using Nop.Services.Stores;
using Nop.Web.Framework.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Nop.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [IgnoreAntiforgeryToken]
    [EnableCors("AllowMarketingSite")]
    public class FastRegisterController : BasePublicController
    {
        private const string StoreOwnerRoleSystemName = "StoreOwner";

        // StoreOwner'ın sahip olacağı izinler
        private static readonly string[] StoreOwnerPermissions = new[]
        {
            "AccessAdminPanel",
            "ManageProducts",
            "ManageCategories",
            "ManageManufacturers",
            "ManageProductReviews",
            "ManageProductTags",
            "ManageAttributes",
            "ManageOrders",
            "SalesSummaryReport",
            "ManageReturnRequests",
            "ManageGiftCards",
            "ManageCurrentCarts",
            "ManageDiscounts",
            "ManageNewsletterSubscribers",
            "ManageNews",
            "ManageBlog",
            "ManageWidgets",
            "ManageTopics",
            "ManagePolls",
            "ManageMessageTemplates",
            "HtmlEditor.ManagePictures",
            "ManageSettings",
            "ManagePaymentMethods",
            "ManageShippingSettings",
            "ManagePlugins",
        };

        private readonly ICustomerService _customerService;
        private readonly ICustomerRegistrationService _customerRegistrationService;
        private readonly IWorkContext _workContext;
        private readonly IStoreContext _storeContext;
        private readonly IStoreService _storeService;
        private readonly IPermissionService _permissionService;
        private readonly IGenericAttributeService _genericAttributeService;
        private readonly CustomerSettings _customerSettings;
        private readonly IEmailAccountService _emailAccountService;
        private readonly IQueuedEmailService _queuedEmailService;
        private readonly ISettingService _settingService;
        private readonly string _storeDomain;
        private readonly bool _sslEnabled;
        private readonly int _trialDays;

        public FastRegisterController(
            ICustomerService customerService,
            ICustomerRegistrationService customerRegistrationService,
            IWorkContext workContext,
            IStoreContext storeContext,
            IStoreService storeService,
            IPermissionService permissionService,
            IGenericAttributeService genericAttributeService,
            CustomerSettings customerSettings,
            IEmailAccountService emailAccountService,
            IQueuedEmailService queuedEmailService,
            ISettingService settingService,
            IConfiguration configuration)
        {
            _customerService = customerService;
            _customerRegistrationService = customerRegistrationService;
            _workContext = workContext;
            _storeContext = storeContext;
            _storeService = storeService;
            _permissionService = permissionService;
            _genericAttributeService = genericAttributeService;
            _customerSettings = customerSettings;
            _emailAccountService = emailAccountService;
            _queuedEmailService = queuedEmailService;
            _settingService = settingService;
            _storeDomain = configuration["ProvisioningConfig:StoreDomain"] ?? "localhost";
            _sslEnabled = bool.TryParse(configuration["ProvisioningConfig:SslEnabled"], out var ssl) && ssl;
            _trialDays = int.TryParse(configuration["ProvisioningConfig:TrialDays"], out var td) ? td : 14;
        }

        [HttpGet("check-slug")]
        public async Task<IActionResult> CheckSlug([FromQuery] string slug)
        {
            if (string.IsNullOrWhiteSpace(slug))
                return Ok(new { available = false, message = "Slug boş olamaz." });

            var cleanSlug = Regex.Replace(slug.ToLowerInvariant().Trim(), @"[^a-z0-9\-]", "");
            if (string.IsNullOrEmpty(cleanSlug))
                return Ok(new { available = false, message = "Geçersiz slug." });

            var host = $"{cleanSlug}.{_storeDomain}";
            var stores = await _storeService.GetAllStoresAsync();
            var exists = stores.Any(s => s.Hosts != null &&
                s.Hosts.Split(',').Any(h => h.Trim().Equals(host, StringComparison.OrdinalIgnoreCase)));

            return Ok(new { available = !exists, slug = cleanSlug });
        }

        [HttpGet("my-store")]
        public async Task<IActionResult> MyStore()
        {
            var customer = await _workContext.GetCurrentCustomerAsync();
            if (!await _customerService.IsRegisteredAsync(customer))
                return Unauthorized(new { success = false, message = "Giriş yapmanız gerekiyor." });

            var storeId = await _genericAttributeService.GetAttributeAsync<int>(customer, "OwnedStoreId");
            if (storeId == 0)
                return NotFound(new { success = false, message = "Mağaza bulunamadı." });

            var store = await _storeService.GetStoreByIdAsync(storeId);
            if (store == null)
                return NotFound(new { success = false, message = "Mağaza bulunamadı." });

            var trialEndDate = await _genericAttributeService.GetAttributeAsync<DateTime?>(customer, "TrialEndDate");
            var subscriptionStatus = await _genericAttributeService.GetAttributeAsync<string>(customer, "SubscriptionStatus") ?? "trial";

            int? daysRemaining = null;
            if (trialEndDate.HasValue)
                daysRemaining = Math.Max(0, (int)(trialEndDate.Value - DateTime.UtcNow).TotalDays);

            return Ok(new
            {
                success = true,
                store = new
                {
                    id = store.Id,
                    name = store.Name,
                    url = store.Url,
                    adminUrl = $"{store.Url}admin/"
                },
                subscription = new
                {
                    status = subscriptionStatus,
                    trialEndDate = trialEndDate?.ToString("dd.MM.yyyy"),
                    daysRemaining,
                    isExpired = daysRemaining == 0 && subscriptionStatus != "active"
                }
            });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] FastRegisterModel model)
        {
            if (model == null || string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Password))
                return BadRequest(new { success = false, message = "Eksik bilgi." });

            if (string.IsNullOrEmpty(model.StoreName) || string.IsNullOrEmpty(model.StoreSlug))
                return BadRequest(new { success = false, message = "Mağaza adı ve slug gereklidir." });

            var slug = Regex.Replace(model.StoreSlug.ToLowerInvariant().Trim(), @"[^a-z0-9\-]", "");
            if (string.IsNullOrEmpty(slug))
                return BadRequest(new { success = false, message = "Geçersiz mağaza slug'ı." });

            // Slug müsaitlik kontrolü
            var host = $"{slug}.{_storeDomain}";
            var allStores = await _storeService.GetAllStoresAsync();
            var slugExists = allStores.Any(s => s.Hosts != null &&
                s.Hosts.Split(',').Any(h => h.Trim().Equals(host, StringComparison.OrdinalIgnoreCase)));
            if (slugExists)
                return BadRequest(new { success = false, message = "Bu mağaza adresi zaten kullanılıyor. Lütfen farklı bir isim deneyin." });

            var customer = await _workContext.GetCurrentCustomerAsync();
            if (await _customerService.IsRegisteredAsync(customer))
                return BadRequest(new { success = false, message = "Zaten üyesiniz." });

            var currentStore = await _storeContext.GetCurrentStoreAsync();

            // Müşteri kaydı
            var registrationRequest = new CustomerRegistrationRequest(
                customer,
                model.Email,
                model.Email,
                model.Password,
                _customerSettings.DefaultPasswordFormat,
                currentStore.Id,
                true);

            var result = await _customerRegistrationService.RegisterCustomerAsync(registrationRequest);
            if (!result.Success)
                return BadRequest(new { success = false, errors = result.Errors });

            customer.FirstName = model.FirstName;
            customer.LastName = model.LastName;
            customer.Phone = model.Phone;
            await _customerService.UpdateCustomerAsync(customer);

            // StoreOwner rolü oluştur (yoksa) ve müşteriye ata
            var storeOwnerRole = await GetOrCreateStoreOwnerRoleAsync();
            await _customerService.AddCustomerRoleMappingAsync(new CustomerCustomerRoleMapping
            {
                CustomerId = customer.Id,
                CustomerRoleId = storeOwnerRole.Id
            });

            // Yeni store oluştur
            var scheme = _sslEnabled ? "https" : "http";
            var storeUrl = $"{scheme}://{slug}.{_storeDomain}/";
            var newStore = new Store
            {
                Name = model.StoreName,
                Url = storeUrl,
                SslEnabled = _sslEnabled,
                Hosts = $"{slug}.{_storeDomain}",
                DefaultLanguageId = currentStore.DefaultLanguageId,
                DisplayOrder = 1,
                CompanyName = model.StoreName
            };
            await _storeService.InsertStoreAsync(newStore);

            // Voyage temasını bu mağaza için varsayılan yap
            await _settingService.SetSettingAsync("storeinformationsettings.defaultstoretheme", "Voyage", newStore.Id);

            // Trial bilgilerini kaydet
            var trialEndDate = DateTime.UtcNow.AddDays(_trialDays);
            await _genericAttributeService.SaveAttributeAsync(customer, "OwnedStoreId", newStore.Id);
            await _genericAttributeService.SaveAttributeAsync(customer, "TrialEndDate", trialEndDate);
            await _genericAttributeService.SaveAttributeAsync(customer, "SubscriptionStatus", "trial");

            // Yeni mağaza bildirimi gönder (bize)
            await SendNewStoreNotificationAsync(customer, newStore);

            // Müşteriye hoş geldin maili gönder
            await SendWelcomeEmailAsync(customer, newStore);

            // One-time auto-login token üret (5 dk geçerli)
            var autoLoginToken = Guid.NewGuid().ToString("N");
            await _genericAttributeService.SaveAttributeAsync(customer, "AutoLoginToken", autoLoginToken);
            await _genericAttributeService.SaveAttributeAsync(customer, "AutoLoginTokenExpiry", DateTime.UtcNow.AddMinutes(30));

            return Ok(new
            {
                success = true,
                message = "Mağaza oluşturuldu.",
                storeUrl,
                autoLoginUrl = $"{storeUrl}quickstart?token={autoLoginToken}"
            });
        }

        private async Task SendWelcomeEmailAsync(Customer customer, Store store)
        {
            try
            {
                var accounts = await _emailAccountService.GetAllEmailAccountsAsync();
                var emailAccount = accounts.Count > 0 ? accounts[0] : null;
                if (emailAccount == null) return;

                var body = $@"
                    <p>Merhaba {customer.FirstName},</p>
                    <p>Mağazanız başarıyla oluşturuldu! Aşağıdaki bilgilerle yönetim panelinize erişebilirsiniz.</p>
                    <table cellpadding='8' style='font-family:sans-serif;font-size:14px;border-collapse:collapse'>
                        <tr><td><b>Mağaza Adı:</b></td><td>{store.Name}</td></tr>
                        <tr><td><b>Mağaza URL:</b></td><td><a href='{store.Url}'>{store.Url}</a></td></tr>
                        <tr><td><b>Yönetim Paneli:</b></td><td><a href='{store.Url}admin/'>{store.Url}admin/</a></td></tr>
                        <tr><td><b>E-posta:</b></td><td>{customer.Email}</td></tr>
                    </table>
                    <br>
                    <p>Herhangi bir sorunuz için <a href='mailto:bilgi@pekinteknoloji.com'>bilgi@pekinteknoloji.com</a> adresinden bize ulaşabilirsiniz.</p>
                    <p>İyi satışlar!<br><b>Pekin Teknoloji</b></p>";

                await _queuedEmailService.InsertQueuedEmailAsync(new QueuedEmail
                {
                    Priority = QueuedEmailPriority.High,
                    From = emailAccount.Email,
                    FromName = emailAccount.DisplayName,
                    To = customer.Email,
                    ToName = $"{customer.FirstName} {customer.LastName}",
                    Subject = $"Mağazanız Hazır: {store.Name}",
                    Body = body,
                    CreatedOnUtc = DateTime.UtcNow,
                    EmailAccountId = emailAccount.Id
                });
            }
            catch
            {
                // Mail gönderilemese bile kayıt işlemi başarılı sayılır
            }
        }

        private async Task SendNewStoreNotificationAsync(Customer customer, Store store)
        {
            try
            {
                var accounts = await _emailAccountService.GetAllEmailAccountsAsync();
                var emailAccount = accounts.Count > 0 ? accounts[0] : null;
                if (emailAccount == null) return;

                var body = $@"
                    <h2>Yeni Mağaza Oluşturuldu</h2>
                    <table cellpadding='6' style='font-family:sans-serif;font-size:14px'>
                        <tr><td><b>Mağaza Adı:</b></td><td>{store.Name}</td></tr>
                        <tr><td><b>URL:</b></td><td><a href='{store.Url}'>{store.Url}</a></td></tr>
                        <tr><td><b>Admin:</b></td><td><a href='{store.Url}admin/'>{store.Url}admin/</a></td></tr>
                        <tr><td><b>Ad Soyad:</b></td><td>{customer.FirstName} {customer.LastName}</td></tr>
                        <tr><td><b>E-posta:</b></td><td>{customer.Email}</td></tr>
                        <tr><td><b>Telefon:</b></td><td>{customer.Phone}</td></tr>
                        <tr><td><b>Tarih:</b></td><td>{DateTime.UtcNow:dd.MM.yyyy HH:mm} UTC</td></tr>
                    </table>";

                await _queuedEmailService.InsertQueuedEmailAsync(new QueuedEmail
                {
                    Priority = QueuedEmailPriority.High,
                    From = emailAccount.Email,
                    FromName = emailAccount.DisplayName,
                    To = "bilgi@pekinteknoloji.com",
                    ToName = "Pekin Teknoloji",
                    Subject = $"Yeni Mağaza: {store.Name}",
                    Body = body,
                    CreatedOnUtc = DateTime.UtcNow,
                    EmailAccountId = emailAccount.Id
                });
            }
            catch
            {
                // Mail gönderilemese bile kayıt işlemi başarılı sayılır
            }
        }

        private async Task<CustomerRole> GetOrCreateStoreOwnerRoleAsync()
        {
            var role = await _customerService.GetCustomerRoleBySystemNameAsync(StoreOwnerRoleSystemName);

            if (role == null)
            {
                role = new CustomerRole
                {
                    Name = "Store Owner",
                    SystemName = StoreOwnerRoleSystemName,
                    Active = true,
                    IsSystemRole = false
                };
                await _customerService.InsertCustomerRoleAsync(role);

                // İzinleri ata
                var allPermissions = await _permissionService.GetAllPermissionRecordsAsync();
                foreach (var permission in allPermissions.Where(p => StoreOwnerPermissions.Contains(p.SystemName)))
                {
                    await _permissionService.InsertPermissionRecordCustomerRoleMappingAsync(
                        new PermissionRecordCustomerRoleMapping
                        {
                            CustomerRoleId = role.Id,
                            PermissionRecordId = permission.Id
                        });
                }
            }

            return role;
        }

        [HttpPost("consultation")]
        public async Task<IActionResult> Consultation([FromBody] ConsultationModel model)
        {
            if (model == null || string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Name))
                return BadRequest(new { success = false, message = "Eksik bilgi." });

            var accounts = await _emailAccountService.GetAllEmailAccountsAsync();
            var emailAccount = accounts.Count > 0 ? accounts[0] : null;

            if (emailAccount == null)
                return StatusCode(500, new { success = false, message = "E-posta hesabı bulunamadı." });

            var body = $@"
                <h2>Yeni Danışmanlık Talebi</h2>
                <table cellpadding='6' style='font-family:sans-serif;font-size:14px'>
                    <tr><td><b>Ad Soyad:</b></td><td>{model.Name}</td></tr>
                    <tr><td><b>E-posta:</b></td><td>{model.Email}</td></tr>
                    <tr><td><b>Telefon:</b></td><td>{model.Phone}</td></tr>
                    <tr><td><b>Konu:</b></td><td>{model.Subject}</td></tr>
                    <tr><td><b>Mesaj:</b></td><td>{model.Message}</td></tr>
                </table>";

            await _queuedEmailService.InsertQueuedEmailAsync(new QueuedEmail
            {
                Priority = QueuedEmailPriority.High,
                From = emailAccount.Email,
                FromName = emailAccount.DisplayName,
                To = "bilgi@pekinteknoloji.com",
                ToName = "Pekin Teknoloji",
                ReplyTo = model.Email,
                ReplyToName = model.Name,
                Subject = $"Danışmanlık Talebi: {model.Subject} — {model.Name}",
                Body = body,
                CreatedOnUtc = DateTime.UtcNow,
                EmailAccountId = emailAccount.Id
            });

            return Ok(new { success = true });
        }

        public class FastRegisterModel
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string Phone { get; set; }
            public string Password { get; set; }
            public string StoreName { get; set; }
            public string StoreSlug { get; set; }
        }

        public class ConsultationModel
        {
            public string Name { get; set; }
            public string Email { get; set; }
            public string Phone { get; set; }
            public string Subject { get; set; }
            public string Message { get; set; }
        }
    }
}
