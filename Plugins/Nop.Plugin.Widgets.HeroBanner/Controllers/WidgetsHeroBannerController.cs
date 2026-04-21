using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Plugin.Widgets.HeroBanner.Models;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Messages;
using Nop.Services.Security;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;

namespace Nop.Plugin.Widgets.HeroBanner.Controllers;

[Area(AreaNames.ADMIN)]
[AutoValidateAntiforgeryToken]
public class WidgetsHeroBannerController : BasePluginController
{
    private readonly ILocalizationService _localizationService;
    private readonly INotificationService _notificationService;
    private readonly IPermissionService _permissionService;
    private readonly ISettingService _settingService;
    private readonly IStoreContext _storeContext;

    public WidgetsHeroBannerController(
        ILocalizationService localizationService,
        INotificationService notificationService,
        IPermissionService permissionService,
        ISettingService settingService,
        IStoreContext storeContext)
    {
        _localizationService = localizationService;
        _notificationService = notificationService;
        _permissionService = permissionService;
        _settingService = settingService;
        _storeContext = storeContext;
    }

    public async Task<IActionResult> Configure()
    {
        if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageWidgets))
            return AccessDeniedView();

        var storeScope = await _storeContext.GetActiveStoreScopeConfigurationAsync();
        var settings = await _settingService.LoadSettingAsync<HeroBannerSettings>(storeScope);

        var model = new ConfigurationModel
        {
            Title = settings.Title,
            Subtitle = settings.Subtitle,
            ButtonText = settings.ButtonText,
            ButtonUrl = settings.ButtonUrl,
            BackgroundColor = settings.BackgroundColor,
            AccentColor = settings.AccentColor,
            BackgroundImageUrl = settings.BackgroundImageUrl,
            ActiveStoreScopeConfiguration = storeScope
        };

        if (storeScope > 0)
        {
            model.Title_OverrideForStore = await _settingService.SettingExistsAsync(settings, x => x.Title, storeScope);
            model.Subtitle_OverrideForStore = await _settingService.SettingExistsAsync(settings, x => x.Subtitle, storeScope);
            model.ButtonText_OverrideForStore = await _settingService.SettingExistsAsync(settings, x => x.ButtonText, storeScope);
            model.ButtonUrl_OverrideForStore = await _settingService.SettingExistsAsync(settings, x => x.ButtonUrl, storeScope);
            model.BackgroundColor_OverrideForStore = await _settingService.SettingExistsAsync(settings, x => x.BackgroundColor, storeScope);
            model.AccentColor_OverrideForStore = await _settingService.SettingExistsAsync(settings, x => x.AccentColor, storeScope);
            model.BackgroundImageUrl_OverrideForStore = await _settingService.SettingExistsAsync(settings, x => x.BackgroundImageUrl, storeScope);
        }

        return View("~/Plugins/Widgets.HeroBanner/Views/Configure.cshtml", model);
    }

    [HttpPost]
    public async Task<IActionResult> Configure(ConfigurationModel model)
    {
        if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageWidgets))
            return AccessDeniedView();

        var storeScope = await _storeContext.GetActiveStoreScopeConfigurationAsync();
        var settings = await _settingService.LoadSettingAsync<HeroBannerSettings>(storeScope);

        settings.Title = model.Title;
        settings.Subtitle = model.Subtitle;
        settings.ButtonText = model.ButtonText;
        settings.ButtonUrl = model.ButtonUrl;
        settings.BackgroundColor = model.BackgroundColor;
        settings.AccentColor = model.AccentColor;
        settings.BackgroundImageUrl = model.BackgroundImageUrl;

        await _settingService.SaveSettingOverridablePerStoreAsync(settings, x => x.Title, model.Title_OverrideForStore, storeScope, false);
        await _settingService.SaveSettingOverridablePerStoreAsync(settings, x => x.Subtitle, model.Subtitle_OverrideForStore, storeScope, false);
        await _settingService.SaveSettingOverridablePerStoreAsync(settings, x => x.ButtonText, model.ButtonText_OverrideForStore, storeScope, false);
        await _settingService.SaveSettingOverridablePerStoreAsync(settings, x => x.ButtonUrl, model.ButtonUrl_OverrideForStore, storeScope, false);
        await _settingService.SaveSettingOverridablePerStoreAsync(settings, x => x.BackgroundColor, model.BackgroundColor_OverrideForStore, storeScope, false);
        await _settingService.SaveSettingOverridablePerStoreAsync(settings, x => x.AccentColor, model.AccentColor_OverrideForStore, storeScope, false);
        await _settingService.SaveSettingOverridablePerStoreAsync(settings, x => x.BackgroundImageUrl, model.BackgroundImageUrl_OverrideForStore, storeScope, false);

        await _settingService.ClearCacheAsync();
        _notificationService.SuccessNotification(await _localizationService.GetResourceAsync("Admin.Plugins.Saved"));

        return await Configure();
    }
}
