using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Plugin.Widgets.CampaignProgress.Models;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Messages;
using Nop.Services.Security;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;

namespace Nop.Plugin.Widgets.CampaignProgress.Controllers;

[Area(AreaNames.ADMIN)]
[AutoValidateAntiforgeryToken]
public class WidgetsCampaignProgressController : BasePluginController
{
    private readonly ILocalizationService _localizationService;
    private readonly INotificationService _notificationService;
    private readonly IPermissionService _permissionService;
    private readonly ISettingService _settingService;
    private readonly IStoreContext _storeContext;

    public WidgetsCampaignProgressController(ILocalizationService localizationService, INotificationService notificationService,
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
        var s = await _settingService.LoadSettingAsync<CampaignProgressSettings>(storeScope);

        return View("~/Plugins/Widgets.CampaignProgress/Views/Configure.cshtml", new ConfigurationModel
        {
            Title = s.Title, Description = s.Description,
            GoalAmount = s.GoalAmount, CurrentAmount = s.CurrentAmount,
            Currency = s.Currency, EndDate = s.EndDate,
            ButtonText = s.ButtonText, ButtonUrl = s.ButtonUrl,
            ActiveStoreScopeConfiguration = storeScope
        });
    }

    [HttpPost]
    public async Task<IActionResult> Configure(ConfigurationModel model)
    {
        if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageWidgets))
            return AccessDeniedView();

        var storeScope = await _storeContext.GetActiveStoreScopeConfigurationAsync();
        var s = await _settingService.LoadSettingAsync<CampaignProgressSettings>(storeScope);

        s.Title = model.Title; s.Description = model.Description;
        s.GoalAmount = model.GoalAmount; s.CurrentAmount = model.CurrentAmount;
        s.Currency = model.Currency; s.EndDate = model.EndDate;
        s.ButtonText = model.ButtonText; s.ButtonUrl = model.ButtonUrl;

        await _settingService.SaveSettingAsync(s, storeScope);
        await _settingService.ClearCacheAsync();
        _notificationService.SuccessNotification(await _localizationService.GetResourceAsync("Admin.Plugins.Saved"));

        return await Configure();
    }
}
