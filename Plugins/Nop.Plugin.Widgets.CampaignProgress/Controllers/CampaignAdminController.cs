using Microsoft.AspNetCore.Mvc;
using Nop.Plugin.Widgets.CampaignProgress.Domain;
using Nop.Plugin.Widgets.CampaignProgress.Models.Admin;
using Nop.Plugin.Widgets.CampaignProgress.Services;
using Nop.Services.Security;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Models.Extensions;
using Nop.Web.Framework.Mvc.Filters;

namespace Nop.Plugin.Widgets.CampaignProgress.Controllers;

[Area(AreaNames.ADMIN)]
[AuthorizeAdmin]
[AutoValidateAntiforgeryToken]
public class CampaignAdminController : BasePluginController
{
    private readonly ICampaignService _campaignService;
    private readonly IPermissionService _permissionService;

    public CampaignAdminController(ICampaignService campaignService, IPermissionService permissionService)
    {
        _campaignService = campaignService;
        _permissionService = permissionService;
    }

    public async Task<IActionResult> List()
    {
        if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageWidgets))
            return AccessDeniedView();

        var searchModel = new CampaignSearchModel();
        searchModel.SetGridPageSize();
        return View("~/Plugins/Widgets.CampaignProgress/Views/Admin/List.cshtml", searchModel);
    }

    [HttpPost]
    public async Task<IActionResult> List(CampaignSearchModel searchModel)
    {
        if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageWidgets))
            return await AccessDeniedDataTablesJson();

        var campaigns = await _campaignService.GetAllCampaignsPagedAsync(
            pageIndex: searchModel.Page - 1,
            pageSize: searchModel.PageSize);

        var model = await new CampaignListModel().PrepareToGridAsync(searchModel, campaigns, () =>
        {
            return campaigns.ToAsyncEnumerable().SelectAwait(async c =>
            {
                var currentAmount = await _campaignService.GetCurrentAmountAsync(c);
                var percent = c.GoalAmount > 0
                    ? (int)Math.Min(100, Math.Round(currentAmount / c.GoalAmount * 100))
                    : 0;
                return new CampaignModel
                {
                    Id = c.Id,
                    Title = c.Title,
                    Slug = c.Slug,
                    GoalAmount = c.GoalAmount,
                    CurrentAmount = currentAmount,
                    ProgressPercent = percent,
                    LinkedProductId = c.LinkedProductId,
                    IsActive = c.IsActive,
                    DisplayOrder = c.DisplayOrder
                };
            });
        });

        return Json(model);
    }

    public async Task<IActionResult> Create()
    {
        if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageWidgets))
            return AccessDeniedView();

        return View("~/Plugins/Widgets.CampaignProgress/Views/Admin/CreateOrEdit.cshtml", new CampaignModel
        {
            IsActive = true,
            DisplayOrder = 1
        });
    }

    [HttpPost]
    public async Task<IActionResult> Create(CampaignModel model)
    {
        if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageWidgets))
            return AccessDeniedView();

        if (!ModelState.IsValid)
            return View("~/Plugins/Widgets.CampaignProgress/Views/Admin/CreateOrEdit.cshtml", model);

        var campaign = new Campaign
        {
            Title = model.Title,
            Slug = model.Slug.Trim().ToLowerInvariant(),
            Description = model.Description,
            ImageUrl = model.ImageUrl,
            GoalAmount = model.GoalAmount,
            ManualBonus = model.ManualBonus,
            LinkedProductId = model.LinkedProductId,
            IsActive = model.IsActive,
            DisplayOrder = model.DisplayOrder
        };

        await _campaignService.InsertAsync(campaign);
        return RedirectToAction(nameof(List));
    }

    public async Task<IActionResult> Edit(int id)
    {
        if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageWidgets))
            return AccessDeniedView();

        var campaign = await _campaignService.GetByIdAsync(id);
        if (campaign == null)
            return RedirectToAction(nameof(List));

        var model = new CampaignModel
        {
            Id = campaign.Id,
            Title = campaign.Title,
            Slug = campaign.Slug,
            Description = campaign.Description,
            ImageUrl = campaign.ImageUrl,
            GoalAmount = campaign.GoalAmount,
            ManualBonus = campaign.ManualBonus,
            LinkedProductId = campaign.LinkedProductId,
            IsActive = campaign.IsActive,
            DisplayOrder = campaign.DisplayOrder
        };

        return View("~/Plugins/Widgets.CampaignProgress/Views/Admin/CreateOrEdit.cshtml", model);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(CampaignModel model)
    {
        if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageWidgets))
            return AccessDeniedView();

        if (!ModelState.IsValid)
            return View("~/Plugins/Widgets.CampaignProgress/Views/Admin/CreateOrEdit.cshtml", model);

        var campaign = await _campaignService.GetByIdAsync(model.Id);
        if (campaign == null)
            return RedirectToAction(nameof(List));

        campaign.Title = model.Title;
        campaign.Slug = model.Slug.Trim().ToLowerInvariant();
        campaign.Description = model.Description;
        campaign.ImageUrl = model.ImageUrl;
        campaign.GoalAmount = model.GoalAmount;
        campaign.ManualBonus = model.ManualBonus;
        campaign.LinkedProductId = model.LinkedProductId;
        campaign.IsActive = model.IsActive;
        campaign.DisplayOrder = model.DisplayOrder;

        await _campaignService.UpdateAsync(campaign);
        return RedirectToAction(nameof(List));
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageWidgets))
            return AccessDeniedView();

        var campaign = await _campaignService.GetByIdAsync(id);
        if (campaign == null)
            return Json(new { success = false });

        await _campaignService.DeleteAsync(campaign);
        return Json(new { success = true });
    }
}
