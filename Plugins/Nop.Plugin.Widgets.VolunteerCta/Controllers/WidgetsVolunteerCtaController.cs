using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Plugin.Widgets.VolunteerCta.Models;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Messages;
using Nop.Services.Security;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;

namespace Nop.Plugin.Widgets.VolunteerCta.Controllers;

[Area(AreaNames.ADMIN)]
[AutoValidateAntiforgeryToken]
public class WidgetsVolunteerCtaController : BasePluginController
{
    private readonly ILocalizationService _localizationService;
    private readonly INotificationService _notificationService;
    private readonly IPermissionService _permissionService;
    private readonly ISettingService _settingService;
    private readonly IStoreContext _storeContext;

    public WidgetsVolunteerCtaController(ILocalizationService localizationService, INotificationService notificationService,
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
        var s = await _settingService.LoadSettingAsync<VolunteerCtaSettings>(storeScope);

        return View("~/Plugins/Widgets.VolunteerCta/Views/Configure.cshtml", new ConfigurationModel
        {
            Title = s.Title, Subtitle = s.Subtitle,
            ButtonText = s.ButtonText, ButtonUrl = s.ButtonUrl,
            Stat1Value = s.Stat1Value, Stat1Label = s.Stat1Label,
            Stat2Value = s.Stat2Value, Stat2Label = s.Stat2Label,
            Stat3Value = s.Stat3Value, Stat3Label = s.Stat3Label,
            ActiveStoreScopeConfiguration = storeScope
        });
    }

    [HttpPost]
    public async Task<IActionResult> Configure(ConfigurationModel model)
    {
        if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageWidgets))
            return AccessDeniedView();

        var storeScope = await _storeContext.GetActiveStoreScopeConfigurationAsync();
        var s = await _settingService.LoadSettingAsync<VolunteerCtaSettings>(storeScope);

        s.Title = model.Title; s.Subtitle = model.Subtitle;
        s.ButtonText = model.ButtonText; s.ButtonUrl = model.ButtonUrl;
        s.Stat1Value = model.Stat1Value; s.Stat1Label = model.Stat1Label;
        s.Stat2Value = model.Stat2Value; s.Stat2Label = model.Stat2Label;
        s.Stat3Value = model.Stat3Value; s.Stat3Label = model.Stat3Label;

        await _settingService.SaveSettingAsync(s, storeScope);
        await _settingService.ClearCacheAsync();
        _notificationService.SuccessNotification(await _localizationService.GetResourceAsync("Admin.Plugins.Saved"));

        return await Configure();
    }
}
