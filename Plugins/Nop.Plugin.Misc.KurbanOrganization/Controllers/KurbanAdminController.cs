using Microsoft.AspNetCore.Mvc;
using Nop.Plugin.Misc.KurbanOrganization.Models;
using Nop.Plugin.Misc.KurbanOrganization.Services;
using Nop.Services.Configuration;
using Nop.Services.Stores;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc.Filters;

namespace Nop.Plugin.Misc.KurbanOrganization.Controllers;

[AuthorizeAdmin]
[Area(AreaNames.ADMIN)]
public class KurbanAdminController : BasePluginController
{
    private readonly KurbanService _kurbanService;
    private readonly ISettingService _settingService;
    private readonly IStoreService _storeService;

    public KurbanAdminController(
        KurbanService kurbanService,
        ISettingService settingService,
        IStoreService storeService)
    {
        _kurbanService = kurbanService;
        _settingService = settingService;
        _storeService = storeService;
    }

    public async Task<IActionResult> List(bool? kesildi)
    {
        var hisseler = await _kurbanService.GetAllHisselerAsync(kesildi);
        var model = hisseler.Select(h => new KurbanHisseModel
        {
            Id = h.Id,
            OrderId = h.OrderId,
            HisseKodu = h.HisseKodu,
            KurbanTuru = h.KurbanTuru,
            HisseSayisi = h.HisseSayisi,
            MusteriAd = h.MusteriAd,
            MusteriTelefon = h.MusteriTelefon,
            Kesildi = h.Kesildi,
            KesimTarihi = h.KesimTarihi?.ToLocalTime().ToString("dd.MM.yyyy HH:mm"),
            BildirimGonderildi = h.BildirimGonderildi,
            CreatedOn = h.CreatedOnUtc.ToLocalTime().ToString("dd.MM.yyyy HH:mm")
        }).ToList();

        ViewBag.FilterKesildi = kesildi;
        return View("~/Plugins/Misc.KurbanOrganization/Views/KurbanAdmin/List.cshtml", model);
    }

    public async Task<IActionResult> Detail(int id)
    {
        var hisse = await _kurbanService.GetHisseByIdAsync(id);
        if (hisse == null)
            return RedirectToAction(nameof(List));

        var model = new KurbanHisseModel
        {
            Id = hisse.Id,
            OrderId = hisse.OrderId,
            HisseKodu = hisse.HisseKodu,
            KurbanTuru = hisse.KurbanTuru,
            HisseSayisi = hisse.HisseSayisi,
            MusteriAd = hisse.MusteriAd,
            MusteriTelefon = hisse.MusteriTelefon,
            Kesildi = hisse.Kesildi,
            KesimTarihi = hisse.KesimTarihi?.ToLocalTime().ToString("dd.MM.yyyy HH:mm"),
            BildirimGonderildi = hisse.BildirimGonderildi,
            CreatedOn = hisse.CreatedOnUtc.ToLocalTime().ToString("dd.MM.yyyy HH:mm"),
            WhatsAppPhone = FormatWhatsAppPhone(hisse.MusteriTelefon)
        };

        return View("~/Plugins/Misc.KurbanOrganization/Views/KurbanAdmin/Detail.cshtml", model);
    }

    [HttpPost]
    public async Task<IActionResult> MarkAsKesildi(int id, bool sendWhatsApp)
    {
        var hisse = await _kurbanService.GetHisseByIdAsync(id);
        if (hisse == null)
            return RedirectToAction(nameof(List));

        await _kurbanService.MarkAsKesildiAsync(hisse);

        if (sendWhatsApp)
        {
            var settings = await _settingService.LoadSettingAsync<KurbanOrganizationSettings>();
            await _kurbanService.SendKesimBildirimiAsync(settings, hisse);
        }

        return RedirectToAction(nameof(Detail), new { id });
    }

    [HttpPost]
    public async Task<IActionResult> SendBildirim(int id)
    {
        var hisse = await _kurbanService.GetHisseByIdAsync(id);
        if (hisse == null)
            return RedirectToAction(nameof(List));

        var settings = await _settingService.LoadSettingAsync<KurbanOrganizationSettings>();
        await _kurbanService.SendKesimBildirimiAsync(settings, hisse);

        return RedirectToAction(nameof(Detail), new { id });
    }

    public async Task<IActionResult> Configure()
    {
        var settings = await _settingService.LoadSettingAsync<KurbanOrganizationSettings>();
        var model = new KurbanConfigureModel
        {
            KurbanCategoryId = settings.KurbanCategoryId,
            WhatsAppAccessToken = settings.WhatsAppAccessToken,
            WhatsAppPhoneNumberId = settings.WhatsAppPhoneNumberId,
            KesimTemplateName = settings.KesimTemplateName,
            TemplateLanguage = settings.TemplateLanguage,
            Enabled = settings.Enabled
        };
        return View("~/Plugins/Misc.KurbanOrganization/Views/KurbanAdmin/Configure.cshtml", model);
    }

    [HttpPost]
    public async Task<IActionResult> Configure(KurbanConfigureModel model)
    {
        var settings = await _settingService.LoadSettingAsync<KurbanOrganizationSettings>();
        settings.KurbanCategoryId = model.KurbanCategoryId;
        settings.WhatsAppAccessToken = model.WhatsAppAccessToken;
        settings.WhatsAppPhoneNumberId = model.WhatsAppPhoneNumberId;
        settings.KesimTemplateName = model.KesimTemplateName;
        settings.TemplateLanguage = model.TemplateLanguage;
        settings.Enabled = model.Enabled;
        await _settingService.SaveSettingAsync(settings);

        ViewBag.Saved = true;
        return View("~/Plugins/Misc.KurbanOrganization/Views/KurbanAdmin/Configure.cshtml", model);
    }

    private static string FormatWhatsAppPhone(string phone)
    {
        if (string.IsNullOrWhiteSpace(phone))
            return string.Empty;

        var digits = new string(phone.Where(char.IsDigit).ToArray());

        if (digits.StartsWith("90") && digits.Length == 12)
            return digits;
        if (digits.StartsWith("0") && digits.Length == 11)
            return "90" + digits[1..];
        if (digits.Length == 10)
            return "90" + digits;

        return digits;
    }
}
