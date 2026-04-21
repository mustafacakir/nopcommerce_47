using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Plugin.Widgets.RecurringDonation.Models;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Messages;
using Nop.Services.Security;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;

namespace Nop.Plugin.Widgets.RecurringDonation.Controllers;

[Area(AreaNames.ADMIN)]
[AutoValidateAntiforgeryToken]
public class WidgetsRecurringDonationController : BasePluginController
{
    private readonly ILocalizationService _localizationService;
    private readonly INotificationService _notificationService;
    private readonly IPermissionService _permissionService;
    private readonly ISettingService _settingService;
    private readonly IStoreContext _storeContext;

    public WidgetsRecurringDonationController(ILocalizationService localizationService, INotificationService notificationService,
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
        var s = await _settingService.LoadSettingAsync<RecurringDonationSettings>(storeScope);

        return View("~/Plugins/Widgets.RecurringDonation/Views/Configure.cshtml", new ConfigurationModel
        {
            Title = s.Title, Subtitle = s.Subtitle,
            Amount1Label = s.Amount1Label, Amount1Url = s.Amount1Url,
            Amount2Label = s.Amount2Label, Amount2Url = s.Amount2Url,
            Amount3Label = s.Amount3Label, Amount3Url = s.Amount3Url,
            Amount4Label = s.Amount4Label, Amount4Url = s.Amount4Url,
            CustomAmountUrl = s.CustomAmountUrl,
            ActiveStoreScopeConfiguration = storeScope
        });
    }

    [HttpPost]
    public async Task<IActionResult> Configure(ConfigurationModel model)
    {
        if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageWidgets))
            return AccessDeniedView();

        var storeScope = await _storeContext.GetActiveStoreScopeConfigurationAsync();
        var s = await _settingService.LoadSettingAsync<RecurringDonationSettings>(storeScope);

        s.Title = model.Title; s.Subtitle = model.Subtitle;
        s.Amount1Label = model.Amount1Label; s.Amount1Url = model.Amount1Url;
        s.Amount2Label = model.Amount2Label; s.Amount2Url = model.Amount2Url;
        s.Amount3Label = model.Amount3Label; s.Amount3Url = model.Amount3Url;
        s.Amount4Label = model.Amount4Label; s.Amount4Url = model.Amount4Url;
        s.CustomAmountUrl = model.CustomAmountUrl;

        await _settingService.SaveSettingAsync(s, storeScope);
        await _settingService.ClearCacheAsync();
        _notificationService.SuccessNotification(await _localizationService.GetResourceAsync("Admin.Plugins.Saved"));

        return await Configure();
    }
}
