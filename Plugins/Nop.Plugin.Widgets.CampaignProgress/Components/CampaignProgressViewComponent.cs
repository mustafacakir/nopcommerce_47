using Microsoft.AspNetCore.Mvc;
using Nop.Plugin.Widgets.CampaignProgress.Models.Public;
using Nop.Plugin.Widgets.CampaignProgress.Services;
using Nop.Web.Framework.Components;

namespace Nop.Plugin.Widgets.CampaignProgress.Components;

public class CampaignProgressViewComponent : NopViewComponent
{
    private readonly ICampaignService _campaignService;

    public CampaignProgressViewComponent(ICampaignService campaignService)
    {
        _campaignService = campaignService;
    }

    public async Task<IViewComponentResult> InvokeAsync(string widgetZone, object additionalData)
    {
        var campaigns = await _campaignService.GetActiveCampaignsAsync();
        if (!campaigns.Any())
            return Content(string.Empty);

        var items = new List<CampaignWidgetItemModel>();
        foreach (var c in campaigns)
        {
            var current = await _campaignService.GetCurrentAmountAsync(c);
            var percent = c.GoalAmount > 0
                ? (int)Math.Min(100, Math.Round(current / c.GoalAmount * 100))
                : 0;

            items.Add(new CampaignWidgetItemModel
            {
                Title = c.Title,
                Slug = c.Slug,
                ImageUrl = c.ImageUrl,
                GoalAmount = c.GoalAmount,
                CurrentAmount = current,
                ProgressPercent = percent
            });
        }

        return View("~/Plugins/Widgets.CampaignProgress/Views/Components/CampaignProgress/Default.cshtml", items);
    }
}
