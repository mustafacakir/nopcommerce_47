using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Plugin.Widgets.FeaturedProducts.Models;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Messages;
using Nop.Services.Security;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;

namespace Nop.Plugin.Widgets.FeaturedProducts.Controllers;

[Area(AreaNames.ADMIN)]
[AutoValidateAntiforgeryToken]
public class WidgetsFeaturedProductsController : BasePluginController
{
    private readonly ILocalizationService _localizationService;
    private readonly INotificationService _notificationService;
    private readonly IPermissionService _permissionService;
    private readonly ISettingService _settingService;
    private readonly IStoreContext _storeContext;

    public WidgetsFeaturedProductsController(
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
        var settings = await _settingService.LoadSettingAsync<FeaturedProductsSettings>(storeScope);

        var model = new ConfigurationModel
        {
            ActiveStoreScopeConfiguration = storeScope,
            SectionBadge = settings.SectionBadge,
            SectionTitle = settings.SectionTitle,
            SectionSubtitle = settings.SectionSubtitle,
            ViewAllText = settings.ViewAllText,
            ViewAllUrl = settings.ViewAllUrl,
            ProductCount = settings.ProductCount
        };

        if (storeScope > 0)
        {
            model.SectionBadge_OverrideForStore = await _settingService.SettingExistsAsync(settings, x => x.SectionBadge, storeScope);
            model.SectionTitle_OverrideForStore = await _settingService.SettingExistsAsync(settings, x => x.SectionTitle, storeScope);
            model.SectionSubtitle_OverrideForStore = await _settingService.SettingExistsAsync(settings, x => x.SectionSubtitle, storeScope);
            model.ViewAllText_OverrideForStore = await _settingService.SettingExistsAsync(settings, x => x.ViewAllText, storeScope);
            model.ViewAllUrl_OverrideForStore = await _settingService.SettingExistsAsync(settings, x => x.ViewAllUrl, storeScope);
            model.ProductCount_OverrideForStore = await _settingService.SettingExistsAsync(settings, x => x.ProductCount, storeScope);
        }

        return View("~/Plugins/Widgets.FeaturedProducts/Views/Configure.cshtml", model);
    }

    [HttpPost]
    public async Task<IActionResult> Configure(ConfigurationModel model)
    {
        if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageWidgets))
            return AccessDeniedView();

        var storeScope = await _storeContext.GetActiveStoreScopeConfigurationAsync();
        var settings = await _settingService.LoadSettingAsync<FeaturedProductsSettings>(storeScope);

        settings.SectionBadge = model.SectionBadge;
        settings.SectionTitle = model.SectionTitle;
        settings.SectionSubtitle = model.SectionSubtitle;
        settings.ViewAllText = model.ViewAllText;
        settings.ViewAllUrl = model.ViewAllUrl;
        settings.ProductCount = model.ProductCount > 0 ? model.ProductCount : 8;

        await _settingService.SaveSettingAsync(settings, storeScope);
        await _settingService.ClearCacheAsync();
        _notificationService.SuccessNotification(await _localizationService.GetResourceAsync("Admin.Plugins.Saved"));

        return await Configure();
    }
}
