using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Plugin.Widgets.DonationSection.Domain;
using Nop.Plugin.Widgets.DonationSection.Models.Admin;
using Nop.Plugin.Widgets.DonationSection.Services;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Models.Extensions;
using Nop.Web.Framework.Mvc.Filters;

namespace Nop.Plugin.Widgets.DonationSection.Controllers;

[Area(AreaNames.ADMIN)]
[AuthorizeAdmin]
[AutoValidateAntiforgeryToken]
public class DonationAdminController : BasePluginController
{
    private readonly IDonationSectionService _service;

    public DonationAdminController(IDonationSectionService service)
    {
        _service = service;
    }

    // ─── Sections ────────────────────────────────────────────────────────────

    public IActionResult Sections()
    {
        var m = new DonSectionSearchModel(); m.SetGridPageSize();
        return View("~/Plugins/Widgets.DonationSection/Views/Admin/SectionList.cshtml", m);
    }

    [HttpPost]
    public async Task<IActionResult> Sections(DonSectionSearchModel searchModel)
    {
        var list = await _service.GetAllSectionsPagedAsync(searchModel.Page - 1, searchModel.PageSize);
        var model = await new DonSectionListModel().PrepareToGridAsync(searchModel, list, () =>
            list.ToAsyncEnumerable().Select(s => new DonSectionModel
            {
                Id = s.Id, Name = s.Name, Color = s.Color,
                DisplayOrder = s.DisplayOrder, IsActive = s.IsActive
            }));
        return Json(model);
    }

    public IActionResult CreateSection() =>
        View("~/Plugins/Widgets.DonationSection/Views/Admin/SectionCreateOrEdit.cshtml",
             new DonSectionModel { IsActive = true, DisplayOrder = 1 });

    [HttpPost]
    public async Task<IActionResult> CreateSection(DonSectionModel model)
    {
        if (!ModelState.IsValid)
            return View("~/Plugins/Widgets.DonationSection/Views/Admin/SectionCreateOrEdit.cshtml", model);
        await _service.InsertSectionAsync(new DonSection
        {
            Name = model.Name, IconSvg = model.IconSvg, Color = model.Color,
            DisplayOrder = model.DisplayOrder, IsActive = model.IsActive
        });
        return RedirectToAction(nameof(Sections));
    }

    public async Task<IActionResult> EditSection(int id)
    {
        var s = await _service.GetSectionByIdAsync(id);
        if (s == null) return RedirectToAction(nameof(Sections));
        return View("~/Plugins/Widgets.DonationSection/Views/Admin/SectionCreateOrEdit.cshtml",
            new DonSectionModel { Id = s.Id, Name = s.Name, IconSvg = s.IconSvg, Color = s.Color,
                                  DisplayOrder = s.DisplayOrder, IsActive = s.IsActive });
    }

    [HttpPost]
    public async Task<IActionResult> EditSection(DonSectionModel model)
    {
        if (!ModelState.IsValid)
            return View("~/Plugins/Widgets.DonationSection/Views/Admin/SectionCreateOrEdit.cshtml", model);
        var s = await _service.GetSectionByIdAsync(model.Id);
        if (s == null) return RedirectToAction(nameof(Sections));
        s.Name = model.Name; s.IconSvg = model.IconSvg; s.Color = model.Color;
        s.DisplayOrder = model.DisplayOrder; s.IsActive = model.IsActive;
        await _service.UpdateSectionAsync(s);
        return RedirectToAction(nameof(Sections));
    }

    [HttpPost]
    public async Task<IActionResult> DeleteSection(int id)
    {
        var s = await _service.GetSectionByIdAsync(id);
        if (s != null) await _service.DeleteSectionAsync(s);
        return Json(new { success = true });
    }

    // ─── Items ────────────────────────────────────────────────────────────────

    public IActionResult Items(int sectionId = 0)
    {
        var m = new DonItemSearchModel { SectionId = sectionId }; m.SetGridPageSize();
        return View("~/Plugins/Widgets.DonationSection/Views/Admin/ItemList.cshtml", m);
    }

    [HttpPost]
    public async Task<IActionResult> Items(DonItemSearchModel searchModel)
    {
        var list = await _service.GetAllItemsPagedAsync(searchModel.SectionId, searchModel.Page - 1, searchModel.PageSize);
        var model = await new DonItemListModel().PrepareToGridAsync(searchModel, list, () =>
            list.ToAsyncEnumerable().Select(i => new DonItemModel
            {
                Id = i.Id, SectionId = i.SectionId, Name = i.Name,
                Price = i.Price, ProductId = i.ProductId,
                DisplayOrder = i.DisplayOrder, IsActive = i.IsActive
            }));
        return Json(model);
    }

    private async Task<DonItemModel> PopulateSectionsAsync(DonItemModel model)
    {
        var sections = await _service.GetAllSectionsPagedAsync();
        model.AvailableSections = sections.Select(s => new SelectListItem { Value = s.Id.ToString(), Text = s.Name }).ToList();
        return model;
    }

    public async Task<IActionResult> CreateItem()
    {
        var m = await PopulateSectionsAsync(new DonItemModel { IsActive = true, DisplayOrder = 1 });
        return View("~/Plugins/Widgets.DonationSection/Views/Admin/ItemCreateOrEdit.cshtml", m);
    }

    [HttpPost]
    public async Task<IActionResult> CreateItem(DonItemModel model)
    {
        if (!ModelState.IsValid)
            return View("~/Plugins/Widgets.DonationSection/Views/Admin/ItemCreateOrEdit.cshtml", await PopulateSectionsAsync(model));
        await _service.InsertItemAsync(new DonItem
        {
            SectionId = model.SectionId, Name = model.Name, Description = model.Description,
            ImageUrl = model.ImageUrl, Price = model.Price, ProductId = model.ProductId,
            DisplayOrder = model.DisplayOrder, IsActive = model.IsActive
        });
        return RedirectToAction(nameof(Items));
    }

    public async Task<IActionResult> EditItem(int id)
    {
        var item = await _service.GetItemByIdAsync(id);
        if (item == null) return RedirectToAction(nameof(Items));
        var m = await PopulateSectionsAsync(new DonItemModel
        {
            Id = item.Id, SectionId = item.SectionId, Name = item.Name, Description = item.Description,
            ImageUrl = item.ImageUrl, Price = item.Price, ProductId = item.ProductId,
            DisplayOrder = item.DisplayOrder, IsActive = item.IsActive
        });
        return View("~/Plugins/Widgets.DonationSection/Views/Admin/ItemCreateOrEdit.cshtml", m);
    }

    [HttpPost]
    public async Task<IActionResult> EditItem(DonItemModel model)
    {
        if (!ModelState.IsValid)
            return View("~/Plugins/Widgets.DonationSection/Views/Admin/ItemCreateOrEdit.cshtml", await PopulateSectionsAsync(model));
        var item = await _service.GetItemByIdAsync(model.Id);
        if (item == null) return RedirectToAction(nameof(Items));
        item.SectionId = model.SectionId; item.Name = model.Name; item.Description = model.Description;
        item.ImageUrl = model.ImageUrl; item.Price = model.Price; item.ProductId = model.ProductId;
        item.DisplayOrder = model.DisplayOrder; item.IsActive = model.IsActive;
        await _service.UpdateItemAsync(item);
        return RedirectToAction(nameof(Items));
    }

    [HttpPost]
    public async Task<IActionResult> DeleteItem(int id)
    {
        var item = await _service.GetItemByIdAsync(id);
        if (item != null) await _service.DeleteItemAsync(item);
        return Json(new { success = true });
    }
}
