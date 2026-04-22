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
            Stat1Value = settings.Stat1Value,
            Stat1Label = settings.Stat1Label,
            Stat1Icon = settings.Stat1Icon,
            Stat2Value = settings.Stat2Value,
            Stat2Label = settings.Stat2Label,
            Stat2Icon = settings.Stat2Icon,
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
            model.Stat1Value_OverrideForStore = await _settingService.SettingExistsAsync(settings, x => x.Stat1Value, storeScope);
            model.Stat1Label_OverrideForStore = await _settingService.SettingExistsAsync(settings, x => x.Stat1Label, storeScope);
            model.Stat1Icon_OverrideForStore = await _settingService.SettingExistsAsync(settings, x => x.Stat1Icon, storeScope);
            model.Stat2Value_OverrideForStore = await _settingService.SettingExistsAsync(settings, x => x.Stat2Value, storeScope);
            model.Stat2Label_OverrideForStore = await _settingService.SettingExistsAsync(settings, x => x.Stat2Label, storeScope);
            model.Stat2Icon_OverrideForStore = await _settingService.SettingExistsAsync(settings, x => x.Stat2Icon, storeScope);
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
        settings.Stat1Value = model.Stat1Value;
        settings.Stat1Label = model.Stat1Label;
        settings.Stat1Icon = model.Stat1Icon;
        settings.Stat2Value = model.Stat2Value;
        settings.Stat2Label = model.Stat2Label;
        settings.Stat2Icon = model.Stat2Icon;

        await _settingService.SaveSettingAsync(settings, storeScope);

        await _settingService.ClearCacheAsync();
        _notificationService.SuccessNotification(await _localizationService.GetResourceAsync("Admin.Plugins.Saved"));

        return await Configure();
    }
}
