using Microsoft.AspNetCore.Mvc;
using Nop.Plugin.Widgets.CampaignProgress.Models.Public;
using Nop.Plugin.Widgets.CampaignProgress.Services;
using Nop.Web.Framework.Controllers;

namespace Nop.Plugin.Widgets.CampaignProgress.Controllers;

public class CampaignPublicController : BasePluginController
{
    private readonly ICampaignService _campaignService;

    public CampaignPublicController(ICampaignService campaignService)
    {
        _campaignService = campaignService;
    }

    public async Task<IActionResult> Detail(string slug)
    {
        var campaign = await _campaignService.GetBySlugAsync(slug);
        if (campaign == null)
            return NotFound();

        var currentAmount = await _campaignService.GetCurrentAmountAsync(campaign);
        var percent = campaign.GoalAmount > 0
            ? (int)Math.Min(100, Math.Round(currentAmount / campaign.GoalAmount * 100))
            : 0;

        var model = new ProjectDetailModel
        {
            Id = campaign.Id,
            Title = campaign.Title,
            Slug = campaign.Slug,
            Description = campaign.Description,
            ImageUrl = campaign.ImageUrl,
            GoalAmount = campaign.GoalAmount,
            CurrentAmount = currentAmount,
            ProgressPercent = percent,
            LinkedProductId = campaign.LinkedProductId
        };

        return View("~/Plugins/Widgets.CampaignProgress/Views/Public/ProjectDetail.cshtml", model);
    }
}
