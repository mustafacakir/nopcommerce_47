using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Plugin.Widgets.Header.BuharaMedresesi.Models;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Messages;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;

namespace Nop.Plugin.Widgets.Header.BuharaMedresesi.Controllers;

[Area(AreaNames.ADMIN)]
[AutoValidateAntiforgeryToken]
public class HeaderBuharaMedresesiAdminController : BasePluginController
{
    private readonly ILocalizationService _localizationService;
    private readonly INotificationService _notificationService;
    private readonly ISettingService _settingService;
    private readonly IStoreContext _storeContext;

    public HeaderBuharaMedresesiAdminController(
        ILocalizationService localizationService,
        INotificationService notificationService,
        ISettingService settingService,
        IStoreContext storeContext)
    {
        _localizationService = localizationService;
        _notificationService = notificationService;
        _settingService = settingService;
        _storeContext = storeContext;
    }

    public async Task<IActionResult> Configure()
    {
        var storeScope = await _storeContext.GetActiveStoreScopeConfigurationAsync();
        var s = await _settingService.LoadSettingAsync<HeaderBuharaMedresesiSettings>(storeScope);

        return View("~/Plugins/Widgets.Header.BuharaMedresesi/Views/Configure.cshtml", new ConfigurationModel
        {
            ActiveStoreScopeConfiguration = storeScope,
            Phone = s.Phone, Email = s.Email,
            InstagramUrl = s.InstagramUrl, TwitterUrl = s.TwitterUrl,
            YoutubeUrl = s.YoutubeUrl, FacebookUrl = s.FacebookUrl,
            LinkedinUrl = s.LinkedinUrl, TiktokUrl = s.TiktokUrl,
            DonateUrl = s.DonateUrl, DonateText = s.DonateText
        });
    }

    [HttpPost]
    public async Task<IActionResult> Configure(ConfigurationModel model)
    {
        var storeScope = await _storeContext.GetActiveStoreScopeConfigurationAsync();
        var s = await _settingService.LoadSettingAsync<HeaderBuharaMedresesiSettings>(storeScope);

        s.Phone = model.Phone; s.Email = model.Email;
        s.InstagramUrl = model.InstagramUrl; s.TwitterUrl = model.TwitterUrl;
        s.YoutubeUrl = model.YoutubeUrl; s.FacebookUrl = model.FacebookUrl;
        s.LinkedinUrl = model.LinkedinUrl; s.TiktokUrl = model.TiktokUrl;
        s.DonateUrl = model.DonateUrl; s.DonateText = model.DonateText;

        await _settingService.SaveSettingAsync(s, storeScope);
        await _settingService.ClearCacheAsync();
        _notificationService.SuccessNotification(await _localizationService.GetResourceAsync("Admin.Plugins.Saved"));

        return await Configure();
    }
}
