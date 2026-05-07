using Microsoft.AspNetCore.Mvc;
using Nop.Plugin.Widgets.VolunteerCta.Domain;
using Nop.Plugin.Widgets.VolunteerCta.Models;
using Nop.Plugin.Widgets.VolunteerCta.Services;
using Nop.Web.Framework.Controllers;

namespace Nop.Plugin.Widgets.VolunteerCta.Controllers;

public class GonulluPublicController : BasePluginController
{
    private readonly GonulluService _gonulluService;

    public GonulluPublicController(GonulluService gonulluService)
    {
        _gonulluService = gonulluService;
    }

    [HttpGet]
    public IActionResult Form()
    {
        return View("~/Plugins/Widgets.VolunteerCta/Views/GonulluBasvuru/Form.cshtml", new GonulluBasvuruModel());
    }

    [HttpPost]
    public async Task<IActionResult> Form(GonulluBasvuruModel model)
    {
        if (!ModelState.IsValid)
            return View("~/Plugins/Widgets.VolunteerCta/Views/GonulluBasvuru/Form.cshtml", model);

        await _gonulluService.InsertAsync(new GonulluBasvuru
        {
            Ad = model.Ad,
            Soyad = model.Soyad,
            Telefon = model.Telefon,
            Email = model.Email,
            Not = model.Not,
            CreatedOnUtc = DateTime.UtcNow
        });

        return View("~/Plugins/Widgets.VolunteerCta/Views/GonulluBasvuru/Tesekkur.cshtml");
    }
}
