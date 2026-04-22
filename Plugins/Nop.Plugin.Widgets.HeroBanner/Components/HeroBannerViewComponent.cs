using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Plugin.Widgets.HeroBanner.Models;
using Nop.Services.Configuration;
using Nop.Web.Framework.Components;

namespace Nop.Plugin.Widgets.HeroBanner.Components;

public class HeroBannerViewComponent : NopViewComponent
{
    private readonly IStoreContext _storeContext;
    private readonly ISettingService _settingService;

    public HeroBannerViewComponent(IStoreContext storeContext, ISettingService settingService)
    {
        _storeContext = storeContext;
        _settingService = settingService;
    }

    public async Task<IViewComponentResult> InvokeAsync(string widgetZone, object additionalData)
    {
        var store = await _storeContext.GetCurrentStoreAsync();
        var settings = await _settingService.LoadSettingAsync<HeroBannerSettings>(store.Id);

        var model = new PublicInfoModel
        {
            Title = settings.Title,
            Subtitle = settings.Subtitle,
            ButtonText = settings.ButtonText,
            ButtonUrl = settings.ButtonUrl,
            BackgroundColor = settings.BackgroundColor,
            AccentColor = settings.AccentColor,
            BackgroundImageUrl = settings.BackgroundImageUrl,
            Stat1Value = settings.Stat1Value,
            Stat1Label = settings.Stat1Label,
            Stat1Icon = settings.Stat1Icon,
            Stat2Value = settings.Stat2Value,
            Stat2Label = settings.Stat2Label,
            Stat2Icon = settings.Stat2Icon
        };

        return View("~/Plugins/Widgets.HeroBanner/Views/PublicInfo.cshtml", model);
    }
}
