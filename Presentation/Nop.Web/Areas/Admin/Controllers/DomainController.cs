using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Nop.Core;
using Nop.Services.Common;
using Nop.Services.Customers;
using Nop.Services.Stores;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc.Filters;
using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

// PEKIN_CUSTOM: Müşteri custom domain yönetimi — domain ekle/sil, nginx config yaz
namespace Nop.Web.Areas.Admin.Controllers
{
    [AuthorizeAdmin]
    [Area(AreaNames.ADMIN)]
    public class DomainController : BaseAdminController
    {
        private readonly IWorkContext _workContext;
        private readonly ICustomerService _customerService;
        private readonly IGenericAttributeService _genericAttributeService;
        private readonly IStoreService _storeService;
        private readonly string _provisionPath;
        private readonly string _nopcommerceUpstream;

        public DomainController(
            IWorkContext workContext,
            ICustomerService customerService,
            IGenericAttributeService genericAttributeService,
            IStoreService storeService,
            IConfiguration configuration)
        {
            _workContext = workContext;
            _customerService = customerService;
            _genericAttributeService = genericAttributeService;
            _storeService = storeService;
            _provisionPath = configuration["DomainProvisioning:ProvisionPath"] ?? "/provision";
            _nopcommerceUpstream = configuration["DomainProvisioning:NopCommerceUpstream"] ?? "pekin_nopcommerce:80";
        }

        // GET /Admin/Domain
        public async Task<IActionResult> Index()
        {
            var customer = await _workContext.GetCurrentCustomerAsync();
            var isAdmin = await _customerService.IsAdminAsync(customer);

            var allStores = await _storeService.GetAllStoresAsync();

            // StoreOwner kendi mağazasını görür, admin hepsini görür
            if (!isAdmin)
            {
                var ownedStoreId = await _genericAttributeService.GetAttributeAsync<int>(customer, "OwnedStoreId");
                allStores = allStores.Where(s => s.Id == ownedStoreId).ToList();
            }

            var storeInfos = allStores.Select(s => new StoredomainInfo
            {
                StoreId   = s.Id,
                StoreName = s.Name,
                Hosts     = s.Hosts ?? "",
                Status    = GetDomainStatus(s.Hosts)
            }).ToList();

            ViewBag.IsAdmin = isAdmin;
            ViewBag.Stores  = storeInfos;

            return View();
        }

        // POST /Admin/Domain/Add
        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> Add([FromBody] AddDomainModel model)
        {
            if (model == null || string.IsNullOrWhiteSpace(model.Domain))
                return Json(new { success = false, message = "Domain boş olamaz." });

            var domain = model.Domain.Trim().ToLowerInvariant()
                .TrimStart('h').Replace("ttps://", "").Replace("ttp://", "")
                .TrimStart('w').Replace("ww.", "").TrimEnd('/');

            // Basit domain format kontrolü
            if (!Regex.IsMatch(domain, @"^[a-z0-9][a-z0-9\-\.]{1,61}[a-z0-9]\.[a-z]{2,}$"))
                return Json(new { success = false, message = "Geçersiz domain formatı." });

            var customer = await _workContext.GetCurrentCustomerAsync();
            var isAdmin  = await _customerService.IsAdminAsync(customer);

            var store = await _storeService.GetStoreByIdAsync(model.StoreId);
            if (store == null)
                return Json(new { success = false, message = "Mağaza bulunamadı." });

            // StoreOwner sadece kendi mağazasına ekleyebilir
            if (!isAdmin)
            {
                var ownedStoreId = await _genericAttributeService.GetAttributeAsync<int>(customer, "OwnedStoreId");
                if (ownedStoreId != model.StoreId)
                    return Json(new { success = false, message = "Bu mağazayı yönetme yetkiniz yok." });
            }

            // Domain başka bir store'da kullanılıyor mu?
            var allStores = await _storeService.GetAllStoresAsync();
            var conflict = allStores.FirstOrDefault(s =>
                s.Id != model.StoreId &&
                (s.Hosts ?? "").Split(',').Any(h => h.Trim().Equals(domain, StringComparison.OrdinalIgnoreCase)));

            if (conflict != null)
                return Json(new { success = false, message = "Bu domain başka bir mağazada kullanılıyor." });

            // Store.Hosts güncelle
            var existingHosts = (store.Hosts ?? "").Split(',')
                .Select(h => h.Trim())
                .Where(h => !string.IsNullOrEmpty(h))
                .ToList();

            if (!existingHosts.Any(h => h.Equals(domain, StringComparison.OrdinalIgnoreCase)))
            {
                existingHosts.Add(domain);
                existingHosts.Add($"www.{domain}");
                store.Hosts = string.Join(",", existingHosts);
                await _storeService.UpdateStoreAsync(store);
            }

            // Nginx config yaz
            try
            {
                WriteNginxConfig(domain);
            }
            catch (Exception ex)
            {
                // Config yazılamazsa uyar ama işlemi iptal etme
                return Json(new
                {
                    success = true,
                    warning = $"Domain kaydedildi fakat nginx config yazılamadı: {ex.Message}. Sunucuda manuel provision gerekiyor.",
                    domain
                });
            }

            return Json(new { success = true, domain, message = "Domain eklendi. Sunucuda 'provision.sh' çalıştırın." });
        }

        // POST /Admin/Domain/Remove
        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> Remove([FromBody] RemoveDomainModel model)
        {
            if (model == null || string.IsNullOrWhiteSpace(model.Domain))
                return Json(new { success = false, message = "Domain boş olamaz." });

            var domain = model.Domain.Trim().ToLowerInvariant();

            var customer = await _workContext.GetCurrentCustomerAsync();
            var isAdmin  = await _customerService.IsAdminAsync(customer);

            var store = await _storeService.GetStoreByIdAsync(model.StoreId);
            if (store == null)
                return Json(new { success = false, message = "Mağaza bulunamadı." });

            if (!isAdmin)
            {
                var ownedStoreId = await _genericAttributeService.GetAttributeAsync<int>(customer, "OwnedStoreId");
                if (ownedStoreId != model.StoreId)
                    return Json(new { success = false, message = "Bu mağazayı yönetme yetkiniz yok." });
            }

            // Store.Hosts'tan kaldır
            var hosts = (store.Hosts ?? "").Split(',')
                .Select(h => h.Trim())
                .Where(h => !string.IsNullOrEmpty(h) &&
                            !h.Equals(domain, StringComparison.OrdinalIgnoreCase) &&
                            !h.Equals($"www.{domain}", StringComparison.OrdinalIgnoreCase))
                .ToList();

            store.Hosts = string.Join(",", hosts);
            await _storeService.UpdateStoreAsync(store);

            // Provision dosyasını sil (varsa)
            var pendingFile = Path.Combine(_provisionPath, "pending", $"{domain}.conf");
            var activeFile  = Path.Combine(_provisionPath, "active",  $"{domain}.conf");
            if (System.IO.File.Exists(pendingFile)) System.IO.File.Delete(pendingFile);
            if (System.IO.File.Exists(activeFile))  System.IO.File.Delete(activeFile);

            return Json(new { success = true, message = "Domain kaldırıldı. Sunucuda nginx reload gerekiyor." });
        }

        private void WriteNginxConfig(string domain)
        {
            var pendingDir = Path.Combine(_provisionPath, "pending");
            Directory.CreateDirectory(pendingDir);

            var config = $@"# Auto-generated by Pekin Teknoloji — {DateTime.UtcNow:yyyy-MM-dd HH:mm} UTC
server {{
    listen 80;
    server_name {domain} www.{domain};

    location / {{
        proxy_pass         http://{_nopcommerceUpstream};
        proxy_set_header   Host              $host;
        proxy_set_header   X-Real-IP         $remote_addr;
        proxy_set_header   X-Forwarded-For   $proxy_add_x_forwarded_for;
        proxy_set_header   X-Forwarded-Proto $scheme;
        proxy_read_timeout 300s;
        client_max_body_size 64m;
    }}
}}
";
            System.IO.File.WriteAllText(Path.Combine(pendingDir, $"{domain}.conf"), config);
        }

        private string GetDomainStatus(string hosts)
        {
            if (string.IsNullOrEmpty(hosts)) return "yok";

            var activeDir = Path.Combine(_provisionPath, "active");
            var hasCustom = hosts.Split(',')
                .Select(h => h.Trim())
                .Any(h => !string.IsNullOrEmpty(h) && !h.Contains(".pekinteknoloji.com") && !h.Contains(".localhost"));

            if (!hasCustom) return "yok";

            var domain = hosts.Split(',')
                .Select(h => h.Trim())
                .FirstOrDefault(h => !h.StartsWith("www.") && !h.Contains(".pekinteknoloji.com") && !h.Contains(".localhost"));

            if (domain == null) return "yok";

            var activeFile  = Path.Combine(_provisionPath, "active",  $"{domain}.conf");
            var pendingFile = Path.Combine(_provisionPath, "pending", $"{domain}.conf");

            if (System.IO.File.Exists(activeFile))  return "aktif";
            if (System.IO.File.Exists(pendingFile)) return "bekliyor";
            return "manuel";
        }

        public class StoredomainInfo
        {
            public int    StoreId   { get; set; }
            public string StoreName { get; set; }
            public string Hosts     { get; set; }
            public string Status    { get; set; }
        }

        public class AddDomainModel
        {
            public int    StoreId { get; set; }
            public string Domain  { get; set; }
        }

        public class RemoveDomainModel
        {
            public int    StoreId { get; set; }
            public string Domain  { get; set; }
        }
    }
}
