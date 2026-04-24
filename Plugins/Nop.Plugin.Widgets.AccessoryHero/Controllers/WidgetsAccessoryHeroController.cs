using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Plugin.Widgets.AccessoryHero.Models;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Messages;
using Nop.Services.Security;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;

namespace Nop.Plugin.Widgets.AccessoryHero.Controllers;

[Area(AreaNames.ADMIN)]
[AutoValidateAntiforgeryToken]
public class WidgetsAccessoryHeroController : BasePluginController
{
    private readonly ILocalizationService _localizationService;
    private readonly INotificationService _notificationService;
    private readonly IPermissionService _permissionService;
    private readonly ISettingService _settingService;
    private readonly IStoreContext _storeContext;

    public WidgetsAccessoryHeroController(
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
        var settings = await _settingService.LoadSettingAsync<AccessoryHeroSettings>(storeScope);

        var model = new ConfigurationModel
        {
            ActiveStoreScopeConfiguration = storeScope,
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

        if (storeScope > 0)
        {
            model.BadgeText_OverrideForStore = await _settingService.SettingExistsAsync(settings, x => x.BadgeText, storeScope);
            model.TitleLine1_OverrideForStore = await _settingService.SettingExistsAsync(settings, x => x.TitleLine1, storeScope);
            model.TitleAccent_OverrideForStore = await _settingService.SettingExistsAsync(settings, x => x.TitleAccent, storeScope);
            model.TitleLine2_OverrideForStore = await _settingService.SettingExistsAsync(settings, x => x.TitleLine2, storeScope);
            model.Description_OverrideForStore = await _settingService.SettingExistsAsync(settings, x => x.Description, storeScope);
            model.Button1Text_OverrideForStore = await _settingService.SettingExistsAsync(settings, x => x.Button1Text, storeScope);
            model.Button1Url_OverrideForStore = await _settingService.SettingExistsAsync(settings, x => x.Button1Url, storeScope);
            model.Button2Text_OverrideForStore = await _settingService.SettingExistsAsync(settings, x => x.Button2Text, storeScope);
            model.Button2Url_OverrideForStore = await _settingService.SettingExistsAsync(settings, x => x.Button2Url, storeScope);
            model.Image1Url_OverrideForStore = await _settingService.SettingExistsAsync(settings, x => x.Image1Url, storeScope);
            model.Image2Url_OverrideForStore = await _settingService.SettingExistsAsync(settings, x => x.Image2Url, storeScope);
            model.Image3Url_OverrideForStore = await _settingService.SettingExistsAsync(settings, x => x.Image3Url, storeScope);
            model.Image4Url_OverrideForStore = await _settingService.SettingExistsAsync(settings, x => x.Image4Url, storeScope);
        }

        return View("~/Plugins/Widgets.AccessoryHero/Views/Configure.cshtml", model);
    }

    [HttpPost]
    public async Task<IActionResult> Configure(ConfigurationModel model)
    {
        if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageWidgets))
            return AccessDeniedView();

        var storeScope = await _storeContext.GetActiveStoreScopeConfigurationAsync();
        var settings = await _settingService.LoadSettingAsync<AccessoryHeroSettings>(storeScope);

        settings.BadgeText = model.BadgeText;
        settings.TitleLine1 = model.TitleLine1;
        settings.TitleAccent = model.TitleAccent;
        settings.TitleLine2 = model.TitleLine2;
        settings.Description = model.Description;
        settings.Button1Text = model.Button1Text;
        settings.Button1Url = model.Button1Url;
        settings.Button2Text = model.Button2Text;
        settings.Button2Url = model.Button2Url;
        settings.Image1Url = model.Image1Url;
        settings.Image2Url = model.Image2Url;
        settings.Image3Url = model.Image3Url;
        settings.Image4Url = model.Image4Url;

        await _settingService.SaveSettingAsync(settings, storeScope);
        await _settingService.ClearCacheAsync();
        _notificationService.SuccessNotification(await _localizationService.GetResourceAsync("Admin.Plugins.Saved"));

        return await Configure();
    }
}
