using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Plugin.Widgets.ImpactCounter.Models;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Messages;
using Nop.Services.Security;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;

namespace Nop.Plugin.Widgets.ImpactCounter.Controllers;

[Area(AreaNames.ADMIN)]
[AutoValidateAntiforgeryToken]
public class WidgetsImpactCounterController : BasePluginController
{
    private readonly ILocalizationService _localizationService;
    private readonly INotificationService _notificationService;
    private readonly IPermissionService _permissionService;
    private readonly ISettingService _settingService;
    private readonly IStoreContext _storeContext;

    public WidgetsImpactCounterController(ILocalizationService localizationService, INotificationService notificationService,
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
        var s = await _settingService.LoadSettingAsync<ImpactCounterSettings>(storeScope);

        var model = new ConfigurationModel
        {
            Stat1Icon = s.Stat1Icon, Stat1Value = s.Stat1Value, Stat1Label = s.Stat1Label,
            Stat2Icon = s.Stat2Icon, Stat2Value = s.Stat2Value, Stat2Label = s.Stat2Label,
            Stat3Icon = s.Stat3Icon, Stat3Value = s.Stat3Value, Stat3Label = s.Stat3Label,
            Stat4Icon = s.Stat4Icon, Stat4Value = s.Stat4Value, Stat4Label = s.Stat4Label,
            ActiveStoreScopeConfiguration = storeScope
        };

        return View("~/Plugins/Widgets.ImpactCounter/Views/Configure.cshtml", model);
    }

    [HttpPost]
    public async Task<IActionResult> Configure(ConfigurationModel model)
    {
        if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageWidgets))
            return AccessDeniedView();

        var storeScope = await _storeContext.GetActiveStoreScopeConfigurationAsync();
        var s = await _settingService.LoadSettingAsync<ImpactCounterSettings>(storeScope);

        s.Stat1Icon = model.Stat1Icon; s.Stat1Value = model.Stat1Value; s.Stat1Label = model.Stat1Label;
        s.Stat2Icon = model.Stat2Icon; s.Stat2Value = model.Stat2Value; s.Stat2Label = model.Stat2Label;
        s.Stat3Icon = model.Stat3Icon; s.Stat3Value = model.Stat3Value; s.Stat3Label = model.Stat3Label;
        s.Stat4Icon = model.Stat4Icon; s.Stat4Value = model.Stat4Value; s.Stat4Label = model.Stat4Label;

        await _settingService.SaveSettingAsync(s, storeScope);
        await _settingService.ClearCacheAsync();
        _notificationService.SuccessNotification(await _localizationService.GetResourceAsync("Admin.Plugins.Saved"));

        return await Configure();
    }
}
