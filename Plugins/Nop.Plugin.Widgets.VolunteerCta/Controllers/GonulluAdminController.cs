using Microsoft.AspNetCore.Mvc;
using Nop.Plugin.Widgets.VolunteerCta.Models;
using Nop.Plugin.Widgets.VolunteerCta.Services;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc.Filters;

namespace Nop.Plugin.Widgets.VolunteerCta.Controllers;

[AuthorizeAdmin]
[Area(AreaNames.ADMIN)]
public class GonulluAdminController : BasePluginController
{
    private readonly GonulluService _gonulluService;

    public GonulluAdminController(GonulluService gonulluService)
    {
        _gonulluService = gonulluService;
    }

    public async Task<IActionResult> List()
    {
        var basvurular = await _gonulluService.GetAllAsync();
        var model = basvurular.Select(b => new GonulluBasvuruModel
        {
            Id = b.Id,
            Ad = b.Ad,
            Soyad = b.Soyad,
            Telefon = b.Telefon,
            Email = b.Email,
            Not = b.Not,
            WhatsAppPhone = FormatWhatsAppPhone(b.Telefon),
            CreatedOn = b.CreatedOnUtc.ToLocalTime().ToString("dd.MM.yyyy HH:mm")
        }).ToList();

        return View("~/Plugins/Widgets.VolunteerCta/Views/GonulluAdmin/List.cshtml", model);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var basvuru = await _gonulluService.GetByIdAsync(id);
        if (basvuru != null)
            await _gonulluService.DeleteAsync(basvuru);

        return RedirectToAction(nameof(List));
    }

    private static string FormatWhatsAppPhone(string phone)
    {
        if (string.IsNullOrWhiteSpace(phone)) return string.Empty;
        var digits = new string(phone.Where(char.IsDigit).ToArray());
        if (digits.StartsWith("90") && digits.Length == 12) return digits;
        if (digits.StartsWith("0") && digits.Length == 11) return "90" + digits[1..];
        if (digits.Length == 10) return "90" + digits;
        return digits;
    }
}
