using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Plugin.Widgets.ProjectStories.Models;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Messages;
using Nop.Services.Security;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;

namespace Nop.Plugin.Widgets.ProjectStories.Controllers;

[Area(AreaNames.ADMIN)]
[AutoValidateAntiforgeryToken]
public class WidgetsProjectStoriesController : BasePluginController
{
    private readonly ILocalizationService _localizationService;
    private readonly INotificationService _notificationService;
    private readonly IPermissionService _permissionService;
    private readonly ISettingService _settingService;
    private readonly IStoreContext _storeContext;

    public WidgetsProjectStoriesController(ILocalizationService localizationService, INotificationService notificationService,
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
        var s = await _settingService.LoadSettingAsync<ProjectStoriesSettings>(storeScope);

        return View("~/Plugins/Widgets.ProjectStories/Views/Configure.cshtml", new ConfigurationModel
        {
            SectionTitle = s.SectionTitle, SectionSubtitle = s.SectionSubtitle,
            Story1Title = s.Story1Title, Story1Tag = s.Story1Tag, Story1Description = s.Story1Description,
            Story1ImageUrl = s.Story1ImageUrl, Story1VideoUrl = s.Story1VideoUrl, Story1LinkUrl = s.Story1LinkUrl,
            Story2Title = s.Story2Title, Story2Tag = s.Story2Tag, Story2Description = s.Story2Description,
            Story2ImageUrl = s.Story2ImageUrl, Story2VideoUrl = s.Story2VideoUrl, Story2LinkUrl = s.Story2LinkUrl,
            Story3Title = s.Story3Title, Story3Tag = s.Story3Tag, Story3Description = s.Story3Description,
            Story3ImageUrl = s.Story3ImageUrl, Story3VideoUrl = s.Story3VideoUrl, Story3LinkUrl = s.Story3LinkUrl,
            Story4Title = s.Story4Title, Story4Tag = s.Story4Tag, Story4Description = s.Story4Description,
            Story4ImageUrl = s.Story4ImageUrl, Story4VideoUrl = s.Story4VideoUrl, Story4LinkUrl = s.Story4LinkUrl,
            ActiveStoreScopeConfiguration = storeScope
        });
    }

    [HttpPost]
    public async Task<IActionResult> Configure(ConfigurationModel model)
    {
        if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageWidgets))
            return AccessDeniedView();

        var storeScope = await _storeContext.GetActiveStoreScopeConfigurationAsync();
        var s = await _settingService.LoadSettingAsync<ProjectStoriesSettings>(storeScope);

        s.SectionTitle = model.SectionTitle; s.SectionSubtitle = model.SectionSubtitle;
        s.Story1Title = model.Story1Title; s.Story1Tag = model.Story1Tag; s.Story1Description = model.Story1Description;
        s.Story1ImageUrl = model.Story1ImageUrl; s.Story1VideoUrl = model.Story1VideoUrl; s.Story1LinkUrl = model.Story1LinkUrl;
        s.Story2Title = model.Story2Title; s.Story2Tag = model.Story2Tag; s.Story2Description = model.Story2Description;
        s.Story2ImageUrl = model.Story2ImageUrl; s.Story2VideoUrl = model.Story2VideoUrl; s.Story2LinkUrl = model.Story2LinkUrl;
        s.Story3Title = model.Story3Title; s.Story3Tag = model.Story3Tag; s.Story3Description = model.Story3Description;
        s.Story3ImageUrl = model.Story3ImageUrl; s.Story3VideoUrl = model.Story3VideoUrl; s.Story3LinkUrl = model.Story3LinkUrl;
        s.Story4Title = model.Story4Title; s.Story4Tag = model.Story4Tag; s.Story4Description = model.Story4Description;
        s.Story4ImageUrl = model.Story4ImageUrl; s.Story4VideoUrl = model.Story4VideoUrl; s.Story4LinkUrl = model.Story4LinkUrl;

        await _settingService.SaveSettingAsync(s, storeScope);
        await _settingService.ClearCacheAsync();
        _notificationService.SuccessNotification(await _localizationService.GetResourceAsync("Admin.Plugins.Saved"));

        return await Configure();
    }
}
