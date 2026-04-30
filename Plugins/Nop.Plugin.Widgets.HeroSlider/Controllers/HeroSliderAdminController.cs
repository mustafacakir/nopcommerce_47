using Microsoft.AspNetCore.Mvc;
using Nop.Plugin.Widgets.HeroSlider.Domain;
using Nop.Plugin.Widgets.HeroSlider.Models.Admin;
using Nop.Plugin.Widgets.HeroSlider.Services;
using Nop.Services.Security;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Models.Extensions;
using Nop.Web.Framework.Mvc.Filters;

namespace Nop.Plugin.Widgets.HeroSlider.Controllers;

[Area(AreaNames.ADMIN)]
[AuthorizeAdmin]
[AutoValidateAntiforgeryToken]
public class HeroSliderAdminController : BasePluginController
{
    private readonly IHeroSliderService _service;
    private readonly IPermissionService _permission;

    public HeroSliderAdminController(IHeroSliderService service, IPermissionService permission)
    {
        _service = service;
        _permission = permission;
    }

    public async Task<IActionResult> List()
    {
        if (!await _permission.AuthorizeAsync(StandardPermissionProvider.ManageWidgets))
            return AccessDeniedView();
        var model = new HeroSlideSearchModel();
        model.SetGridPageSize();
        return View("~/Plugins/Widgets.HeroSlider/Views/Admin/List.cshtml", model);
    }

    [HttpPost]
    public async Task<IActionResult> List(HeroSlideSearchModel searchModel)
    {
        if (!await _permission.AuthorizeAsync(StandardPermissionProvider.ManageWidgets))
            return await AccessDeniedDataTablesJson();

        var slides = await _service.GetAllPagedAsync(searchModel.Page - 1, searchModel.PageSize);
        var model = await new HeroSlideListModel().PrepareToGridAsync(searchModel, slides, () =>
            slides.ToAsyncEnumerable().Select(s => new HeroSlideModel
            {
                Id = s.Id, Title = s.Title, CategoryName = s.CategoryName,
                DisplayOrder = s.DisplayOrder, IsActive = s.IsActive
            }));
        return Json(model);
    }

    public IActionResult Create() =>
        View("~/Plugins/Widgets.HeroSlider/Views/Admin/CreateOrEdit.cshtml",
             new HeroSlideModel { IsActive = true, DisplayOrder = 1 });

    [HttpPost]
    public async Task<IActionResult> Create(HeroSlideModel model)
    {
        if (!ModelState.IsValid)
            return View("~/Plugins/Widgets.HeroSlider/Views/Admin/CreateOrEdit.cshtml", model);

        await _service.InsertAsync(new HeroSlide
        {
            Title = model.Title, Subtitle = model.Subtitle,
            BadgeLabel = model.BadgeLabel, PriceBadge = model.PriceBadge,
            PrimaryButtonText = model.PrimaryButtonText, PrimaryButtonUrl = model.PrimaryButtonUrl,
            SecondaryButtonText = model.SecondaryButtonText, SecondaryButtonUrl = model.SecondaryButtonUrl,
            ImageUrl = model.ImageUrl, CategoryName = model.CategoryName, CategoryIcon = model.CategoryIcon,
            DisplayOrder = model.DisplayOrder, IsActive = model.IsActive
        });
        return RedirectToAction(nameof(List));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var slide = await _service.GetByIdAsync(id);
        if (slide == null) return RedirectToAction(nameof(List));
        return View("~/Plugins/Widgets.HeroSlider/Views/Admin/CreateOrEdit.cshtml", new HeroSlideModel
        {
            Id = slide.Id, Title = slide.Title, Subtitle = slide.Subtitle,
            BadgeLabel = slide.BadgeLabel, PriceBadge = slide.PriceBadge,
            PrimaryButtonText = slide.PrimaryButtonText, PrimaryButtonUrl = slide.PrimaryButtonUrl,
            SecondaryButtonText = slide.SecondaryButtonText, SecondaryButtonUrl = slide.SecondaryButtonUrl,
            ImageUrl = slide.ImageUrl, CategoryName = slide.CategoryName, CategoryIcon = slide.CategoryIcon,
            DisplayOrder = slide.DisplayOrder, IsActive = slide.IsActive
        });
    }

    [HttpPost]
    public async Task<IActionResult> Edit(HeroSlideModel model)
    {
        if (!ModelState.IsValid)
            return View("~/Plugins/Widgets.HeroSlider/Views/Admin/CreateOrEdit.cshtml", model);

        var slide = await _service.GetByIdAsync(model.Id);
        if (slide == null) return RedirectToAction(nameof(List));

        slide.Title = model.Title; slide.Subtitle = model.Subtitle;
        slide.BadgeLabel = model.BadgeLabel; slide.PriceBadge = model.PriceBadge;
        slide.PrimaryButtonText = model.PrimaryButtonText; slide.PrimaryButtonUrl = model.PrimaryButtonUrl;
        slide.SecondaryButtonText = model.SecondaryButtonText; slide.SecondaryButtonUrl = model.SecondaryButtonUrl;
        slide.ImageUrl = model.ImageUrl; slide.CategoryName = model.CategoryName; slide.CategoryIcon = model.CategoryIcon;
        slide.DisplayOrder = model.DisplayOrder; slide.IsActive = model.IsActive;
        await _service.UpdateAsync(slide);
        return RedirectToAction(nameof(List));
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var slide = await _service.GetByIdAsync(id);
        if (slide != null) await _service.DeleteAsync(slide);
        return Json(new { success = true });
    }
}
