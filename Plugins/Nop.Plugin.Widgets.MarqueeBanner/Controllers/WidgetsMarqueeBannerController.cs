using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Plugin.Widgets.MarqueeBanner.Models;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Messages;
using Nop.Services.Security;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;

namespace Nop.Plugin.Widgets.MarqueeBanner.Controllers;

[Area(AreaNames.ADMIN)]
[AutoValidateAntiforgeryToken]
public class WidgetsMarqueeBannerController : BasePluginController
{
    private readonly ILocalizationService _localizationService;
    private readonly INotificationService _notificationService;
    private readonly IPermissionService _permissionService;
    private readonly ISettingService _settingService;
    private readonly IStoreContext _storeContext;

    public WidgetsMarqueeBannerController(
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
        var settings = await _settingService.LoadSettingAsync<MarqueeBannerSettings>(storeScope);

        var model = new ConfigurationModel
        {
            Text = settings.Text,
            BackgroundColor = settings.BackgroundColor,
            TextColor = settings.TextColor,
            Speed = settings.Speed,
            ActiveStoreScopeConfiguration = storeScope
        };

        if (storeScope > 0)
        {
            model.Text_OverrideForStore = await _settingService.SettingExistsAsync(settings, x => x.Text, storeScope);
            model.BackgroundColor_OverrideForStore = await _settingService.SettingExistsAsync(settings, x => x.BackgroundColor, storeScope);
            model.TextColor_OverrideForStore = await _settingService.SettingExistsAsync(settings, x => x.TextColor, storeScope);
            model.Speed_OverrideForStore = await _settingService.SettingExistsAsync(settings, x => x.Speed, storeScope);
        }

        return View("~/Plugins/Widgets.MarqueeBanner/Views/Configure.cshtml", model);
    }

    [HttpPost]
    public async Task<IActionResult> Configure(ConfigurationModel model)
    {
        if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageWidgets))
            return AccessDeniedView();

        var storeScope = await _storeContext.GetActiveStoreScopeConfigurationAsync();
        var settings = await _settingService.LoadSettingAsync<MarqueeBannerSettings>(storeScope);

        settings.Text = model.Text;
        settings.BackgroundColor = model.BackgroundColor;
        settings.TextColor = model.TextColor;
        settings.Speed = model.Speed;

        await _settingService.SaveSettingOverridablePerStoreAsync(settings, x => x.Text, model.Text_OverrideForStore, storeScope, false);
        await _settingService.SaveSettingOverridablePerStoreAsync(settings, x => x.BackgroundColor, model.BackgroundColor_OverrideForStore, storeScope, false);
        await _settingService.SaveSettingOverridablePerStoreAsync(settings, x => x.TextColor, model.TextColor_OverrideForStore, storeScope, false);
        await _settingService.SaveSettingOverridablePerStoreAsync(settings, x => x.Speed, model.Speed_OverrideForStore, storeScope, false);

        await _settingService.ClearCacheAsync();

        _notificationService.SuccessNotification(await _localizationService.GetResourceAsync("Admin.Plugins.Saved"));

        return await Configure();
    }
}
