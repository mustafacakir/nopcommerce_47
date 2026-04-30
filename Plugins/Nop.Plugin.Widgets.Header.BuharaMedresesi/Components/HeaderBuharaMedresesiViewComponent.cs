using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Plugin.Widgets.Header.BuharaMedresesi.Models;
using Nop.Services.Configuration;
using Nop.Web.Framework.Components;

namespace Nop.Plugin.Widgets.Header.BuharaMedresesi.Components;

public class HeaderBuharaMedresesiViewComponent : NopViewComponent
{
    private readonly IStoreContext _storeContext;
    private readonly ISettingService _settingService;

    public HeaderBuharaMedresesiViewComponent(IStoreContext storeContext, ISettingService settingService)
    {
        _storeContext = storeContext;
        _settingService = settingService;
    }

    public async Task<IViewComponentResult> InvokeAsync(string widgetZone, object additionalData)
    {
        var store = await _storeContext.GetCurrentStoreAsync();
        var s = await _settingService.LoadSettingAsync<HeaderBuharaMedresesiSettings>(store.Id);

        var model = new PublicInfoModel
        {
            Phone        = s.Phone,
            Email        = s.Email,
            InstagramUrl = s.InstagramUrl,
            TwitterUrl   = s.TwitterUrl,
            YoutubeUrl   = s.YoutubeUrl,
            FacebookUrl  = s.FacebookUrl,
            LinkedinUrl  = s.LinkedinUrl,
            TiktokUrl    = s.TiktokUrl,
            DonateUrl    = s.DonateUrl,
            DonateText   = s.DonateText
        };

        return View("~/Plugins/Widgets.Header.BuharaMedresesi/Views/PublicInfo.cshtml", model);
    }
}
