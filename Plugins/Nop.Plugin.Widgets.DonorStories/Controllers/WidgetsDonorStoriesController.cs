using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Plugin.Widgets.DonorStories.Models;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Messages;
using Nop.Services.Security;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;

namespace Nop.Plugin.Widgets.DonorStories.Controllers;

[Area(AreaNames.ADMIN)]
[AutoValidateAntiforgeryToken]
public class WidgetsDonorStoriesController : BasePluginController
{
    private readonly ILocalizationService _localizationService;
    private readonly INotificationService _notificationService;
    private readonly IPermissionService _permissionService;
    private readonly ISettingService _settingService;
    private readonly IStoreContext _storeContext;

    public WidgetsDonorStoriesController(ILocalizationService localizationService, INotificationService notificationService,
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
        var s = await _settingService.LoadSettingAsync<DonorStoriesSettings>(storeScope);

        return View("~/Plugins/Widgets.DonorStories/Views/Configure.cshtml", new ConfigurationModel
        {
            SectionTitle = s.SectionTitle,
            Story1Name = s.Story1Name, Story1Location = s.Story1Location, Story1Quote = s.Story1Quote, Story1Avatar = s.Story1Avatar,
            Story2Name = s.Story2Name, Story2Location = s.Story2Location, Story2Quote = s.Story2Quote, Story2Avatar = s.Story2Avatar,
            Story3Name = s.Story3Name, Story3Location = s.Story3Location, Story3Quote = s.Story3Quote, Story3Avatar = s.Story3Avatar,
            Story4Name = s.Story4Name, Story4Location = s.Story4Location, Story4Quote = s.Story4Quote, Story4Avatar = s.Story4Avatar,
            ActiveStoreScopeConfiguration = storeScope
        });
    }

    [HttpPost]
    public async Task<IActionResult> Configure(ConfigurationModel model)
    {
        if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageWidgets))
            return AccessDeniedView();

        var storeScope = await _storeContext.GetActiveStoreScopeConfigurationAsync();
        var s = await _settingService.LoadSettingAsync<DonorStoriesSettings>(storeScope);

        s.SectionTitle = model.SectionTitle;
        s.Story1Name = model.Story1Name; s.Story1Location = model.Story1Location; s.Story1Quote = model.Story1Quote; s.Story1Avatar = model.Story1Avatar;
        s.Story2Name = model.Story2Name; s.Story2Location = model.Story2Location; s.Story2Quote = model.Story2Quote; s.Story2Avatar = model.Story2Avatar;
        s.Story3Name = model.Story3Name; s.Story3Location = model.Story3Location; s.Story3Quote = model.Story3Quote; s.Story3Avatar = model.Story3Avatar;
        s.Story4Name = model.Story4Name; s.Story4Location = model.Story4Location; s.Story4Quote = model.Story4Quote; s.Story4Avatar = model.Story4Avatar;

        await _settingService.SaveSettingAsync(s, storeScope);
        await _settingService.ClearCacheAsync();
        _notificationService.SuccessNotification(await _localizationService.GetResourceAsync("Admin.Plugins.Saved"));

        return await Configure();
    }
}
