using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Plugin.Widgets.CampaignProgress.Models;
using Nop.Services.Configuration;
using Nop.Web.Framework.Components;

namespace Nop.Plugin.Widgets.CampaignProgress.Components;

public class CampaignProgressViewComponent : NopViewComponent
{
    private readonly IStoreContext _storeContext;
    private readonly ISettingService _settingService;

    public CampaignProgressViewComponent(IStoreContext storeContext, ISettingService settingService)
    {
        _storeContext = storeContext;
        _settingService = settingService;
    }

    public async Task<IViewComponentResult> InvokeAsync(string widgetZone, object additionalData)
    {
        var store = await _storeContext.GetCurrentStoreAsync();
        var s = await _settingService.LoadSettingAsync<CampaignProgressSettings>(store.Id);

        var percent = s.GoalAmount > 0
            ? (int)Math.Min(100, Math.Round(s.CurrentAmount / s.GoalAmount * 100))
            : 0;

        var model = new PublicInfoModel
        {
            Title = s.Title,
            Description = s.Description,
            GoalAmount = s.GoalAmount,
            CurrentAmount = s.CurrentAmount,
            Currency = s.Currency,
            ProgressPercent = percent,
            EndDate = s.EndDate,
            ButtonText = s.ButtonText,
            ButtonUrl = s.ButtonUrl
        };

        return View("~/Plugins/Widgets.CampaignProgress/Views/PublicInfo.cshtml", model);
    }
}
