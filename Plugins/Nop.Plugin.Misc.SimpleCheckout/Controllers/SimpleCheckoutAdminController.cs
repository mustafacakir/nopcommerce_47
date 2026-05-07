using Microsoft.AspNetCore.Mvc;
using Nop.Services.Configuration;
using Nop.Services.Security;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc.Filters;

namespace Nop.Plugin.Misc.SimpleCheckout.Controllers;

[AuthorizeAdmin]
[Area(AreaNames.ADMIN)]
public class SimpleCheckoutAdminController : BasePluginController
{
    private readonly ISettingService _settingService;
    private readonly IPermissionService _permissionService;

    public SimpleCheckoutAdminController(ISettingService settingService, IPermissionService permissionService)
    {
        _settingService = settingService;
        _permissionService = permissionService;
    }

    public async Task<IActionResult> Configure()
    {
        if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManagePlugins))
            return AccessDeniedView();

        var settings = await _settingService.LoadSettingAsync<SimpleCheckoutSettings>();
        return View("~/Plugins/Misc.SimpleCheckout/Views/Configure.cshtml", settings);
    }

    [HttpPost]
    public async Task<IActionResult> Configure(SimpleCheckoutSettings model)
    {
        if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManagePlugins))
            return AccessDeniedView();

        await _settingService.SaveSettingAsync(model);
        ViewBag.Saved = true;
        return View("~/Plugins/Misc.SimpleCheckout/Views/Configure.cshtml", model);
    }
}
