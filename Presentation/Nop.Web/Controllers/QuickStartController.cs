using System;
using System.Linq;
using System.Threading.Tasks;
using LinqToDB;
using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Core.Domain.Common;
using Nop.Data;
using Nop.Services.Authentication;
using Nop.Services.Common;
using Nop.Services.Customers;
using Nop.Services.Stores;
using Nop.Web.Framework.Controllers;

namespace Nop.Web.Controllers
{
    public class QuickStartController : BasePublicController
    {
        private readonly ICustomerService _customerService;
        private readonly IGenericAttributeService _genericAttributeService;
        private readonly IAuthenticationService _authenticationService;
        private readonly IStoreService _storeService;
        private readonly IRepository<GenericAttribute> _gaRepository;
        private readonly IWorkContext _workContext;

        public QuickStartController(
            ICustomerService customerService,
            IGenericAttributeService genericAttributeService,
            IAuthenticationService authenticationService,
            IStoreService storeService,
            IRepository<GenericAttribute> gaRepository,
            IWorkContext workContext)
        {
            _customerService = customerService;
            _genericAttributeService = genericAttributeService;
            _authenticationService = authenticationService;
            _storeService = storeService;
            _gaRepository = gaRepository;
            _workContext = workContext;
        }

        [HttpGet("/quickstart")]
        public async Task<IActionResult> Index([FromQuery] string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                var currentCustomer = await _workContext.GetCurrentCustomerAsync();
                if (await _customerService.IsRegisteredAsync(currentCustomer))
                {
                    var currentStoreId = await _genericAttributeService.GetAttributeAsync<int>(currentCustomer, "OwnedStoreId");
                    var currentStore = currentStoreId > 0 ? await _storeService.GetStoreByIdAsync(currentStoreId) : null;
                    return Content(BuildWelcomePage(currentCustomer.FirstName, currentStore?.Name, currentStore?.Url), "text/html", System.Text.Encoding.UTF8);
                }
                return Redirect("/admin");
            }

            var ga = await _gaRepository.Table
                .FirstOrDefaultAsync(x =>
                    x.KeyGroup == "Customer" &&
                    x.Key == "AutoLoginToken" &&
                    x.Value == token);

            if (ga == null)
                return Redirect("/admin");

            var customer = await _customerService.GetCustomerByIdAsync(ga.EntityId);
            if (customer == null)
                return Redirect("/admin");

            var expiry = await _genericAttributeService.GetAttributeAsync<DateTime?>(customer, "AutoLoginTokenExpiry");
            if (expiry == null || DateTime.UtcNow > expiry.Value)
                return Redirect("/admin");

            await _authenticationService.SignInAsync(customer, isPersistent: true);
            await _genericAttributeService.SaveAttributeAsync(customer, "AutoLoginToken", (string)null);
            await _genericAttributeService.SaveAttributeAsync(customer, "AutoLoginTokenExpiry", (DateTime?)null);

            var storeId = await _genericAttributeService.GetAttributeAsync<int>(customer, "OwnedStoreId");
            var store = storeId > 0 ? await _storeService.GetStoreByIdAsync(storeId) : null;

            return Content(BuildWelcomePage(customer.FirstName, store?.Name, store?.Url), "text/html", System.Text.Encoding.UTF8);
        }

        private static string BuildWelcomePage(string firstName, string storeName, string storeUrl)
        {
            var adminUrl = storeUrl?.TrimEnd('/') + "/admin";
            var fn = System.Web.HttpUtility.HtmlEncode(firstName ?? "");
            var sn = System.Web.HttpUtility.HtmlEncode(storeName ?? "Mağazanız");
            var su = System.Web.HttpUtility.HtmlEncode(storeUrl ?? "");
            var au = System.Web.HttpUtility.HtmlEncode(adminUrl);
            return $@"<!DOCTYPE html>
<html lang=""tr"">
<head>
<meta charset=""utf-8"">
<meta name=""viewport"" content=""width=device-width, initial-scale=1"">
<title>Mağazanız Hazır — {sn}</title>
<style>
*, *::before, *::after {{ box-sizing: border-box; margin: 0; padding: 0; }}
body {{
  font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, 'Helvetica Neue', sans-serif;
  background: #f0f2f5;
  min-height: 100vh;
  display: flex;
  flex-direction: column;
}}
.topbar {{
  background: #fff;
  border-bottom: 1px solid #e5e7eb;
  padding: 0 40px;
  height: 60px;
  display: flex;
  align-items: center;
  justify-content: space-between;
}}
.topbar-brand {{ font-size: 1rem; font-weight: 700; color: #111827; letter-spacing: -0.02em; }}
.topbar-brand span {{ color: #6366f1; }}
.topbar-user {{ font-size: 0.85rem; color: #6b7280; }}
.main {{
  flex: 1;
  display: grid;
  grid-template-columns: 1fr 380px;
  gap: 24px;
  max-width: 1100px;
  width: 100%;
  margin: 40px auto;
  padding: 0 24px;
}}
.hero {{ background: #fff; border-radius: 16px; border: 1px solid #e5e7eb; overflow: hidden; }}
.hero-top {{
  background: linear-gradient(135deg, #6366f1 0%, #8b5cf6 100%);
  padding: 40px 40px 48px;
  position: relative;
}}
.hero-top::after {{
  content: '';
  position: absolute;
  bottom: -1px; left: 0; right: 0;
  height: 32px;
  background: #fff;
  border-radius: 16px 16px 0 0;
}}
.badge {{
  display: inline-flex; align-items: center; gap: 6px;
  background: rgba(255,255,255,0.2); color: #fff;
  font-size: 0.75rem; font-weight: 600;
  padding: 5px 12px; border-radius: 100px;
  margin-bottom: 20px; letter-spacing: 0.03em; text-transform: uppercase;
}}
.badge-dot {{ width: 6px; height: 6px; background: #4ade80; border-radius: 50%; }}
.hero-top h1 {{ font-size: 1.9rem; font-weight: 800; color: #fff; line-height: 1.2; margin-bottom: 8px; letter-spacing: -0.03em; }}
.hero-top p {{ color: rgba(255,255,255,0.8); font-size: 1rem; }}
.hero-body {{ padding: 32px 40px 36px; }}
.store-info {{
  display: flex; align-items: center; gap: 16px;
  background: #f9fafb; border: 1px solid #e5e7eb;
  border-radius: 12px; padding: 16px 20px; margin-bottom: 32px;
}}
.store-avatar {{
  width: 48px; height: 48px;
  background: linear-gradient(135deg, #6366f1, #8b5cf6);
  border-radius: 12px; display: flex; align-items: center; justify-content: center;
  font-size: 1.4rem; font-weight: 800; color: #fff; flex-shrink: 0;
}}
.store-meta {{ flex: 1; min-width: 0; }}
.store-name {{ font-size: 1rem; font-weight: 700; color: #111827; margin-bottom: 2px; }}
.store-url {{ font-size: 0.8rem; color: #6b7280; white-space: nowrap; overflow: hidden; text-overflow: ellipsis; }}
.store-badge {{ background: #d1fae5; color: #065f46; font-size: 0.72rem; font-weight: 700; padding: 3px 10px; border-radius: 100px; flex-shrink: 0; }}
.steps-title {{ font-size: 0.75rem; font-weight: 700; color: #9ca3af; text-transform: uppercase; letter-spacing: 0.07em; margin-bottom: 16px; }}
.steps {{ display: flex; flex-direction: column; gap: 12px; }}
.step {{
  display: flex; align-items: flex-start; gap: 14px;
  padding: 14px 16px; border-radius: 10px; border: 1px solid #e5e7eb;
  text-decoration: none; background: #fff;
  transition: border-color 0.15s, box-shadow 0.15s;
}}
.step:hover {{ border-color: #6366f1; box-shadow: 0 0 0 3px rgba(99,102,241,0.08); }}
.step-icon {{ width: 36px; height: 36px; border-radius: 9px; display: flex; align-items: center; justify-content: center; font-size: 1rem; flex-shrink: 0; }}
.step-icon-1 {{ background: #ede9fe; }}
.step-icon-2 {{ background: #fef3c7; }}
.step-text {{ flex: 1; }}
.step-label {{ font-size: 0.9rem; font-weight: 600; color: #111827; margin-bottom: 2px; }}
.step-desc {{ font-size: 0.8rem; color: #6b7280; }}
.step-arrow {{ color: #d1d5db; font-size: 1rem; align-self: center; transition: color 0.15s; }}
.step:hover .step-arrow {{ color: #6366f1; }}
.sidebar {{ display: flex; flex-direction: column; gap: 16px; }}
.panel {{ background: #fff; border-radius: 16px; border: 1px solid #e5e7eb; padding: 24px; }}
.panel-title {{ font-size: 0.75rem; font-weight: 700; color: #9ca3af; text-transform: uppercase; letter-spacing: 0.07em; margin-bottom: 16px; }}
.cta {{
  display: flex; align-items: center; justify-content: center; gap: 8px;
  background: #6366f1; color: #fff; text-decoration: none;
  font-weight: 700; font-size: 0.95rem; padding: 14px 20px;
  border-radius: 10px; width: 100%; transition: background 0.15s, transform 0.1s;
}}
.cta:hover {{ background: #4f46e5; transform: translateY(-1px); }}
.cta-sub {{
  display: flex; align-items: center; justify-content: center; gap: 6px;
  background: #f9fafb; color: #374151; text-decoration: none;
  font-weight: 600; font-size: 0.85rem; padding: 11px 16px;
  border-radius: 8px; border: 1px solid #e5e7eb; width: 100%; margin-top: 8px;
  transition: background 0.15s;
}}
.cta-sub:hover {{ background: #f3f4f6; }}
.trial-bar {{
  background: #f9fafb; border-radius: 8px; padding: 12px 14px;
  display: flex; align-items: center; gap: 10px; font-size: 0.85rem;
}}
.trial-bar-icon {{ font-size: 1.1rem; }}
.trial-bar-text {{ color: #374151; line-height: 1.4; }}
.trial-bar-text strong {{ color: #111827; }}
.cred {{ margin-bottom: 12px; }}
.cred-label {{ font-size: 0.72rem; color: #9ca3af; font-weight: 600; text-transform: uppercase; letter-spacing: 0.05em; margin-bottom: 4px; }}
.cred-value {{
  font-size: 0.85rem; color: #374151;
  background: #f9fafb; border: 1px solid #e5e7eb;
  border-radius: 6px; padding: 8px 10px;
  font-family: monospace; word-break: break-all;
}}
/* Prewarm modal */
.pw-overlay {{
  display: none; position: fixed; inset: 0;
  background: rgba(0,0,0,0.45); backdrop-filter: blur(4px);
  z-index: 1000; align-items: center; justify-content: center;
}}
.pw-overlay.active {{ display: flex; }}
.pw-box {{
  background: #fff; border-radius: 20px; padding: 40px 44px;
  max-width: 380px; width: 90%; text-align: center;
  box-shadow: 0 24px 64px rgba(0,0,0,0.18);
}}
.pw-spinner {{
  width: 56px; height: 56px;
  border: 4px solid #e5e7eb; border-top-color: #6366f1;
  border-radius: 50%; margin: 0 auto 24px;
  animation: spin 0.9s linear infinite;
}}
@keyframes spin {{ to {{ transform: rotate(360deg); }} }}
.pw-title {{ font-size: 1.15rem; font-weight: 700; color: #111827; margin-bottom: 8px; }}
.pw-step {{ font-size: 0.85rem; color: #6b7280; min-height: 20px; }}
.pw-bar-wrap {{ background: #f3f4f6; border-radius: 100px; height: 6px; margin-top: 20px; overflow: hidden; }}
.pw-bar {{ height: 100%; background: linear-gradient(90deg, #6366f1, #8b5cf6); border-radius: 100px; width: 0%; transition: width 0.6s ease; }}
@media (max-width: 768px) {{
  .main {{ grid-template-columns: 1fr; }}
  .hero-top {{ padding: 28px 24px 40px; }}
  .hero-body {{ padding: 24px; }}
  .topbar {{ padding: 0 20px; }}
}}
</style>
</head>
<body>

<div class=""pw-overlay"" id=""pw-overlay"">
  <div class=""pw-box"">
    <div class=""pw-spinner""></div>
    <div class=""pw-title"">Mağazanız hazırlanıyor…</div>
    <div class=""pw-step"" id=""pw-step"">Bağlantı kuruluyor…</div>
    <div class=""pw-bar-wrap""><div class=""pw-bar"" id=""pw-bar""></div></div>
  </div>
</div>

<div class=""topbar"">
  <div class=""topbar-brand"">Pekin<span>Teknoloji</span></div>
  <div class=""topbar-user"">Hoş geldiniz, {fn}</div>
</div>

<div class=""main"">
  <div class=""hero"">
    <div class=""hero-top"">
      <div class=""badge""><span class=""badge-dot""></span> Mağaza Aktif</div>
      <h1>Mağazanız hazır,<br>{fn}!</h1>
      <p>{sn} başarıyla oluşturuldu.</p>
    </div>
    <div class=""hero-body"">
      <div class=""store-info"">
        <div class=""store-avatar"">{(storeName?.Length > 0 ? System.Web.HttpUtility.HtmlEncode(storeName[0].ToString().ToUpper()) : "M")}</div>
        <div class=""store-meta"">
          <div class=""store-name"">{sn}</div>
          <div class=""store-url"">{su}</div>
        </div>
        <div class=""store-badge"">Trial · 14 gün</div>
      </div>
      <div class=""steps-title"">Başlamak için yapman gerekenler</div>
      <div class=""steps"">
        <a href=""{au}/product/create"" class=""step"" onclick=""goPrewarm(event, '{au}/product/create')"">
          <div class=""step-icon step-icon-1"">📦</div>
          <div class=""step-text"">
            <div class=""step-label"">İlk ürününü ekle</div>
            <div class=""step-desc"">Ürün adı, fiyat, stok ve görsel ekle</div>
          </div>
          <span class=""step-arrow"">›</span>
        </a>
        <a href=""{au}/category/create"" class=""step"" onclick=""goPrewarm(event, '{au}/category/create')"">
          <div class=""step-icon step-icon-2"">🗂️</div>
          <div class=""step-text"">
            <div class=""step-label"">Kategori oluştur</div>
            <div class=""step-desc"">Ürünlerini kategorilere ayır, gezinmeyi kolaylaştır</div>
          </div>
          <span class=""step-arrow"">›</span>
        </a>
      </div>
    </div>
  </div>

  <div class=""sidebar"">
    <div class=""panel"">
      <div class=""panel-title"">Yönetim Paneli</div>
      <a href=""{au}"" class=""cta"" onclick=""goPrewarm(event, '{au}')"">
        Admin Paneline Git &nbsp;→
      </a>
      <a href=""{su}"" class=""cta-sub"" onclick=""goPrewarm(event, '{su}')"">
        🏪 &nbsp;Mağazamı Görüntüle
      </a>
    </div>
    <div class=""panel"">
      <div class=""panel-title"">Trial Süresi</div>
      <div class=""trial-bar"">
        <div class=""trial-bar-icon"">⏳</div>
        <div class=""trial-bar-text"">
          Mağazanız <strong>14 gün</strong> boyunca ücretsiz kullanıma açık. Süre dolmadan abonelik planı seçin.
        </div>
      </div>
    </div>
    <div class=""panel"">
      <div class=""panel-title"">Giriş Bilgileri</div>
      <div class=""cred"">
        <div class=""cred-label"">Admin Paneli</div>
        <div class=""cred-value"">{au}</div>
      </div>
      <div class=""cred"">
        <div class=""cred-label"">Mağaza Adresi</div>
        <div class=""cred-value"">{su}</div>
      </div>
    </div>
  </div>
</div>

<script>
var _adminUrl = '{au}';
var _storeUrl = '{su}';
var _steps = [
  'Bağlantı kuruluyor\u2026',
  'Veritabanı hazırlanıyor\u2026',
  'Önbellek dolduruluyor\u2026',
  'Neredeyse hazır\u2026'
];
function goPrewarm(event, targetUrl) {{
  event.preventDefault();
  var overlay = document.getElementById('pw-overlay');
  var stepEl  = document.getElementById('pw-step');
  var barEl   = document.getElementById('pw-bar');
  overlay.classList.add('active');
  var i = 0;
  stepEl.textContent = _steps[0];
  barEl.style.width  = '15%';
  var interval = setInterval(function() {{
    i++;
    if (i < _steps.length) {{
      stepEl.textContent = _steps[i];
      barEl.style.width  = (15 + i * 22) + '%';
    }}
  }}, 1100);
  var warm1 = fetch(_adminUrl, {{ credentials: 'include' }}).catch(function(){{}});
  var warm2 = fetch(_storeUrl, {{ credentials: 'include' }}).catch(function(){{}});
  var minWait = new Promise(function(r) {{ setTimeout(r, 4000); }});
  Promise.all([warm1, warm2, minWait]).then(function() {{
    clearInterval(interval);
    stepEl.textContent = 'Hazır! Yönlendiriliyor\u2026';
    barEl.style.width  = '100%';
    setTimeout(function() {{ window.location.href = targetUrl; }}, 400);
  }});
}}
</script>
</body>
</html>";
        }
    }
}
