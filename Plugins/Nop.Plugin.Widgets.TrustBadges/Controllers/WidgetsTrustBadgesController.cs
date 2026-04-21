using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Plugin.Widgets.TrustBadges.Models;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Messages;
using Nop.Services.Security;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;

namespace Nop.Plugin.Widgets.TrustBadges.Controllers;

[Area(AreaNames.ADMIN)]
[AutoValidateAntiforgeryToken]
public class WidgetsTrustBadgesController : BasePluginController
{
    private readonly ILocalizationService _localizationService;
    private readonly INotificationService _notificationService;
    private readonly IPermissionService _permissionService;
    private readonly ISettingService _settingService;
    private readonly IStoreContext _storeContext;

    public WidgetsTrustBadgesController(ILocalizationService localizationService, INotificationService notificationService,
        IPermissionService permissionService, ISettingService settingService, IStoreContext storeContext)
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
        var s = await _settingService.LoadSettingAsync<TrustBadgesSettings>(storeScope);

        return View("~/Plugins/Widgets.TrustBadges/Views/Configure.cshtml", new ConfigurationModel
        {
            Badge1Icon = s.Badge1Icon, Badge1Title = s.Badge1Title, Badge1Subtitle = s.Badge1Subtitle,
            Badge2Icon = s.Badge2Icon, Badge2Title = s.Badge2Title, Badge2Subtitle = s.Badge2Subtitle,
            Badge3Icon = s.Badge3Icon, Badge3Title = s.Badge3Title, Badge3Subtitle = s.Badge3Subtitle,
            Badge4Icon = s.Badge4Icon, Badge4Title = s.Badge4Title, Badge4Subtitle = s.Badge4Subtitle,
            Badge5Icon = s.Badge5Icon, Badge5Title = s.Badge5Title, Badge5Subtitle = s.Badge5Subtitle,
            ActiveStoreScopeConfiguration = storeScope
        });
    }

    [HttpPost]
    public async Task<IActionResult> Configure(ConfigurationModel model)
    {
        if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageWidgets))
            return AccessDeniedView();

        var storeScope = await _storeContext.GetActiveStoreScopeConfigurationAsync();
        var s = await _settingService.LoadSettingAsync<TrustBadgesSettings>(storeScope);

        s.Badge1Icon = model.Badge1Icon; s.Badge1Title = model.Badge1Title; s.Badge1Subtitle = model.Badge1Subtitle;
        s.Badge2Icon = model.Badge2Icon; s.Badge2Title = model.Badge2Title; s.Badge2Subtitle = model.Badge2Subtitle;
        s.Badge3Icon = model.Badge3Icon; s.Badge3Title = model.Badge3Title; s.Badge3Subtitle = model.Badge3Subtitle;
        s.Badge4Icon = model.Badge4Icon; s.Badge4Title = model.Badge4Title; s.Badge4Subtitle = model.Badge4Subtitle;
        s.Badge5Icon = model.Badge5Icon; s.Badge5Title = model.Badge5Title; s.Badge5Subtitle = model.Badge5Subtitle;

        await _settingService.SaveSettingAsync(s, storeScope);
        await _settingService.ClearCacheAsync();
        _notificationService.SuccessNotification(await _localizationService.GetResourceAsync("Admin.Plugins.Saved"));

        return await Configure();
    }
}
