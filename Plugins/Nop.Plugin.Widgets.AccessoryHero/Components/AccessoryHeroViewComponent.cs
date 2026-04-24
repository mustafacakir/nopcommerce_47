using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Plugin.Widgets.AccessoryHero.Models;
using Nop.Services.Configuration;
using Nop.Web.Framework.Components;

namespace Nop.Plugin.Widgets.AccessoryHero.Components;

public class AccessoryHeroViewComponent : NopViewComponent
{
    private readonly IStoreContext _storeContext;
    private readonly ISettingService _settingService;

    public AccessoryHeroViewComponent(IStoreContext storeContext, ISettingService settingService)
    {
        _storeContext = storeContext;
        _settingService = settingService;
    }

    public async Task<IViewComponentResult> InvokeAsync(string widgetZone, object additionalData)
    {
        var store = await _storeContext.GetCurrentStoreAsync();
        var settings = await _settingService.LoadSettingAsync<AccessoryHeroSettings>(store.Id);

        var model = new PublicInfoModel
        {
            BadgeText = settings.BadgeText,
            TitleLine1 = settings.TitleLine1,
            TitleAccent = settings.TitleAccent,
            TitleLine2 = settings.TitleLine2,
            Description = settings.Description,
            Button1Text = settings.Button1Text,
            Button1Url = settings.Button1Url,
            Button2Text = settings.Button2Text,
            Button2Url = settings.Button2Url,
            Image1Url = settings.Image1Url,
            Image2Url = settings.Image2Url,
            Image3Url = settings.Image3Url,
            Image4Url = settings.Image4Url
        };

        return View("~/Plugins/Widgets.AccessoryHero/Views/PublicInfo.cshtml", model);
    }
}
