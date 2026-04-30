using Microsoft.AspNetCore.Mvc;
using Nop.Plugin.Widgets.KumbaraForm.Models;
using Nop.Plugin.Widgets.KumbaraForm.Services;
using Nop.Services.Security;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Models.Extensions;
using Nop.Web.Framework.Mvc.Filters;

namespace Nop.Plugin.Widgets.KumbaraForm.Controllers;

[Area(AreaNames.ADMIN)]
[AuthorizeAdmin]
[AutoValidateAntiforgeryToken]
public class KumbaraAdminController : BasePluginController
{
    private readonly IKumbaraService _kumbaraService;
    private readonly IPermissionService _permissionService;

    public KumbaraAdminController(IKumbaraService kumbaraService, IPermissionService permissionService)
    {
        _kumbaraService = kumbaraService;
        _permissionService = permissionService;
    }

    public async Task<IActionResult> Configure()
    {
        if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageWidgets))
            return AccessDeniedView();

        var searchModel = new KumbaraEntrySearchModel();
        searchModel.SetGridPageSize();
        return View("~/Plugins/Widgets.KumbaraForm/Views/Admin/List.cshtml", searchModel);
    }

    [HttpPost]
    public async Task<IActionResult> List(KumbaraEntrySearchModel searchModel)
    {
        if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageWidgets))
            return await AccessDeniedDataTablesJson();

        var entries = await _kumbaraService.GetAllEntriesAsync(
            pageIndex: searchModel.Page - 1,
            pageSize: searchModel.PageSize);

        var usagePlaceLabel = new Dictionary<string, string>
        {
            { "home", "Ev" },
            { "work", "İşyeri" },
            { "school", "Okul/Üniversite" },
            { "mosque", "Cami/Dini Mekan" },
            { "other", "Diğer" }
        };

        var model = await new KumbaraEntryListModel().PrepareToGridAsync(searchModel, entries, () =>
        {
            return entries.ToAsyncEnumerable().SelectAwait(async e =>
            {
                await Task.CompletedTask;
                return new KumbaraEntryModel
                {
                    Id = e.Id,
                    FullName = $"{e.FirstName} {e.LastName}",
                    Email = e.Email,
                    Phone = e.Phone,
                    City = e.City,
                    District = e.District,
                    Address = e.Address,
                    Quantity = e.Quantity,
                    UsagePlace = e.UsagePlace != null && usagePlaceLabel.TryGetValue(e.UsagePlace, out var label) ? label : e.UsagePlace,
                    Message = e.Message,
                    IsRead = e.IsRead,
                    CreatedOn = e.CreatedOnUtc.ToLocalTime().ToString("dd.MM.yyyy HH:mm")
                };
            });
        });

        return Json(model);
    }

    [HttpPost]
    public async Task<IActionResult> MarkRead(int id)
    {
        if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageWidgets))
            return AccessDeniedView();

        var entry = await _kumbaraService.GetEntryByIdAsync(id);
        if (entry == null)
            return NotFound();

        await _kumbaraService.MarkAsReadAsync(entry);
        return Json(new { success = true });
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageWidgets))
            return AccessDeniedView();

        var entry = await _kumbaraService.GetEntryByIdAsync(id);
        if (entry == null)
            return NotFound();

        await _kumbaraService.DeleteEntryAsync(entry);
        return Json(new { success = true });
    }
}
