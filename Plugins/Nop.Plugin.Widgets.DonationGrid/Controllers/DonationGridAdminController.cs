using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Plugin.Widgets.DonationGrid.Models;
using Nop.Services.Configuration;
using Nop.Services.Security;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc.Filters;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Plugin.Widgets.DonationGrid.Controllers;

[AuthorizeAdmin]
[Area(AreaNames.ADMIN)]
public class DonationGridController : BasePluginController
{
    private readonly IPermissionService _permissionService;
    private readonly ISettingService _settingService;
    private readonly IStoreContext _storeContext;

    public DonationGridController(
        IPermissionService permissionService,
        ISettingService settingService,
        IStoreContext storeContext)
    {
        _permissionService = permissionService;
        _settingService = settingService;
        _storeContext = storeContext;
    }

    public async Task<IActionResult> Configure()
    {
        if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageWidgets))
            return AccessDeniedView();

        var storeScope = await _storeContext.GetActiveStoreScopeConfigurationAsync();
        var settings = await _settingService.LoadSettingAsync<DonationGridSettings>(storeScope);

        var model = new ConfigureModel
        {
            ActiveStoreScopeConfiguration = storeScope,
            RootCategoryId = settings.RootCategoryId,
        };

        if (storeScope > 0)
            model.RootCategoryId_OverrideForStore = await _settingService.SettingExistsAsync(settings, x => x.RootCategoryId, storeScope);

        return View("~/Plugins/Widgets.DonationGrid/Views/Configure.cshtml", model);
    }

    [HttpPost]
    public async Task<IActionResult> Configure(ConfigureModel model)
    {
        if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageWidgets))
            return AccessDeniedView();

        var storeScope = await _storeContext.GetActiveStoreScopeConfigurationAsync();
        var settings = await _settingService.LoadSettingAsync<DonationGridSettings>(storeScope);

        settings.RootCategoryId = model.RootCategoryId;

        await _settingService.SaveSettingOverridablePerStoreAsync(settings, x => x.RootCategoryId, model.RootCategoryId_OverrideForStore, storeScope, false);
        await _settingService.ClearCacheAsync();

        return await Configure();
    }
}
