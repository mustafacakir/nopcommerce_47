using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Plugin.Widgets.CategoryShowcase.Models;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Messages;
using Nop.Services.Security;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;

namespace Nop.Plugin.Widgets.CategoryShowcase.Controllers;

[Area(AreaNames.ADMIN)]
[AutoValidateAntiforgeryToken]
public class WidgetsCategoryShowcaseController : BasePluginController
{
    private readonly ILocalizationService _localizationService;
    private readonly INotificationService _notificationService;
    private readonly IPermissionService _permissionService;
    private readonly ISettingService _settingService;
    private readonly IStoreContext _storeContext;

    public WidgetsCategoryShowcaseController(
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
        var settings = await _settingService.LoadSettingAsync<CategoryShowcaseSettings>(storeScope);

        var model = new ConfigurationModel
        {
            ActiveStoreScopeConfiguration = storeScope,
            SectionBadge = settings.SectionBadge,
            SectionTitle = settings.SectionTitle,
            SectionSubtitle = settings.SectionSubtitle,
            Card1Title = settings.Card1Title,
            Card1Description = settings.Card1Description,
            Card1Badge = settings.Card1Badge,
            Card1ImageUrl = settings.Card1ImageUrl,
            Card1Url = settings.Card1Url,
            Card2Title = settings.Card2Title,
            Card2Description = settings.Card2Description,
            Card2Badge = settings.Card2Badge,
            Card2ImageUrl = settings.Card2ImageUrl,
            Card2Url = settings.Card2Url
        };

        if (storeScope > 0)
        {
            model.SectionBadge_OverrideForStore = await _settingService.SettingExistsAsync(settings, x => x.SectionBadge, storeScope);
            model.SectionTitle_OverrideForStore = await _settingService.SettingExistsAsync(settings, x => x.SectionTitle, storeScope);
            model.SectionSubtitle_OverrideForStore = await _settingService.SettingExistsAsync(settings, x => x.SectionSubtitle, storeScope);
            model.Card1Title_OverrideForStore = await _settingService.SettingExistsAsync(settings, x => x.Card1Title, storeScope);
            model.Card1Description_OverrideForStore = await _settingService.SettingExistsAsync(settings, x => x.Card1Description, storeScope);
            model.Card1Badge_OverrideForStore = await _settingService.SettingExistsAsync(settings, x => x.Card1Badge, storeScope);
            model.Card1ImageUrl_OverrideForStore = await _settingService.SettingExistsAsync(settings, x => x.Card1ImageUrl, storeScope);
            model.Card1Url_OverrideForStore = await _settingService.SettingExistsAsync(settings, x => x.Card1Url, storeScope);
            model.Card2Title_OverrideForStore = await _settingService.SettingExistsAsync(settings, x => x.Card2Title, storeScope);
            model.Card2Description_OverrideForStore = await _settingService.SettingExistsAsync(settings, x => x.Card2Description, storeScope);
            model.Card2Badge_OverrideForStore = await _settingService.SettingExistsAsync(settings, x => x.Card2Badge, storeScope);
            model.Card2ImageUrl_OverrideForStore = await _settingService.SettingExistsAsync(settings, x => x.Card2ImageUrl, storeScope);
            model.Card2Url_OverrideForStore = await _settingService.SettingExistsAsync(settings, x => x.Card2Url, storeScope);
        }

        return View("~/Plugins/Widgets.CategoryShowcase/Views/Configure.cshtml", model);
    }

    [HttpPost]
    public async Task<IActionResult> Configure(ConfigurationModel model)
    {
        if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageWidgets))
            return AccessDeniedView();

        var storeScope = await _storeContext.GetActiveStoreScopeConfigurationAsync();
        var settings = await _settingService.LoadSettingAsync<CategoryShowcaseSettings>(storeScope);

        settings.SectionBadge = model.SectionBadge;
        settings.SectionTitle = model.SectionTitle;
        settings.SectionSubtitle = model.SectionSubtitle;
        settings.Card1Title = model.Card1Title;
        settings.Card1Description = model.Card1Description;
        settings.Card1Badge = model.Card1Badge;
        settings.Card1ImageUrl = model.Card1ImageUrl;
        settings.Card1Url = model.Card1Url;
        settings.Card2Title = model.Card2Title;
        settings.Card2Description = model.Card2Description;
        settings.Card2Badge = model.Card2Badge;
        settings.Card2ImageUrl = model.Card2ImageUrl;
        settings.Card2Url = model.Card2Url;

        await _settingService.SaveSettingAsync(settings, storeScope);
        await _settingService.ClearCacheAsync();
        _notificationService.SuccessNotification(await _localizationService.GetResourceAsync("Admin.Plugins.Saved"));

        return await Configure();
    }
}
