using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Plugin.Widgets.MarqueeBanner.Models;
using Nop.Services.Configuration;
using Nop.Web.Framework.Components;

namespace Nop.Plugin.Widgets.MarqueeBanner.Components;

public class MarqueeBannerViewComponent : NopViewComponent
{
    private readonly IStoreContext _storeContext;
    private readonly ISettingService _settingService;

    public MarqueeBannerViewComponent(IStoreContext storeContext, ISettingService settingService)
    {
        _storeContext = storeContext;
        _settingService = settingService;
    }

    public async Task<IViewComponentResult> InvokeAsync(string widgetZone, object additionalData)
    {
        var store = await _storeContext.GetCurrentStoreAsync();
        var settings = await _settingService.LoadSettingAsync<MarqueeBannerSettings>(store.Id);

        if (string.IsNullOrWhiteSpace(settings.Text))
            return Content("");

        var model = new PublicInfoModel
        {
            Text = settings.Text,
            Link = settings.Link,
            BackgroundColor = settings.BackgroundColor,
            TextColor = settings.TextColor,
            Speed = settings.Speed > 0 ? settings.Speed : 40
        };

        return View("~/Plugins/Widgets.MarqueeBanner/Views/PublicInfo.cshtml", model);
    }
}
