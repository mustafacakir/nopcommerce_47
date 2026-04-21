using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Plugin.Widgets.SocialShare.Models;
using Nop.Services.Configuration;
using Nop.Web.Framework.Components;

namespace Nop.Plugin.Widgets.SocialShare.Components;

public class SocialShareViewComponent : NopViewComponent
{
    private readonly ISettingService _settingService;
    private readonly IStoreContext _storeContext;

    public SocialShareViewComponent(ISettingService settingService, IStoreContext storeContext)
    {
        _settingService = settingService;
        _storeContext = storeContext;
    }

    public async Task<IViewComponentResult> InvokeAsync(string widgetZone, object? additionalData = null)
    {
        var store = await _storeContext.GetCurrentStoreAsync();
        var s = await _settingService.LoadSettingAsync<SocialShareSettings>(store.Id);

        var model = new PublicInfoModel
        {
            Title = s.Title,
            ShowWhatsApp = s.ShowWhatsApp,
            ShowFacebook = s.ShowFacebook,
            ShowX = s.ShowX,
            ShowCopyLink = s.ShowCopyLink
        };

        return View("~/Plugins/Widgets.SocialShare/Views/Components/SocialShare/Default.cshtml", model);
    }
}
