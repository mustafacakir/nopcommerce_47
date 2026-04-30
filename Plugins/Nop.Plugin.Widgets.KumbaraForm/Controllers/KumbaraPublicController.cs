using Microsoft.AspNetCore.Mvc;
using Nop.Plugin.Widgets.KumbaraForm.Domain;
using Nop.Plugin.Widgets.KumbaraForm.Models;
using Nop.Plugin.Widgets.KumbaraForm.Services;
using Nop.Web.Framework.Controllers;

namespace Nop.Plugin.Widgets.KumbaraForm.Controllers;

public class KumbaraPublicController : BasePluginController
{
    private readonly IKumbaraService _kumbaraService;

    public KumbaraPublicController(IKumbaraService kumbaraService)
    {
        _kumbaraService = kumbaraService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View("~/Plugins/Widgets.KumbaraForm/Views/Public/KumbaraPage.cshtml");
    }

    [HttpPost]
    [IgnoreAntiforgeryToken]
    public async Task<IActionResult> Submit(KumbaraFormModel model)
    {
        if (!ModelState.IsValid)
            return Json(new { success = false, message = "Lütfen zorunlu alanları eksiksiz doldurun." });

        var entry = new KumbaraEntry
        {
            FirstName = (model.FirstName ?? "").Trim(),
            LastName = (model.LastName ?? "").Trim(),
            Email = (model.Email ?? "").Trim(),
            Phone = (model.Phone ?? "").Trim(),
            City = (model.City ?? "").Trim(),
            District = (model.District ?? "").Trim(),
            Address = (model.Address ?? "").Trim(),
            Quantity = model.Quantity < 1 ? 1 : model.Quantity,
            UsagePlace = model.UsagePlace?.Trim(),
            Message = model.Message?.Trim(),
            IsRead = false,
            CreatedOnUtc = DateTime.UtcNow
        };

        await _kumbaraService.InsertEntryAsync(entry);

        return Json(new { success = true });
    }
}
