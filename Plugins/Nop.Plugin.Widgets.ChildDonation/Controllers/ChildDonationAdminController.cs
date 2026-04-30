using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Plugin.Widgets.ChildDonation.Models;
using Nop.Services.Configuration;
using Nop.Services.Security;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc.Filters;

namespace Nop.Plugin.Widgets.ChildDonation.Controllers;

[Area(AreaNames.ADMIN)]
[AuthorizeAdmin]
[AutoValidateAntiforgeryToken]
public class ChildDonationAdminController : BasePluginController
{
    private readonly ISettingService _settingService;
    private readonly IStoreContext _storeContext;
    private readonly IPermissionService _permissionService;

    public ChildDonationAdminController(
        ISettingService settingService,
        IStoreContext storeContext,
        IPermissionService permissionService)
    {
        _settingService = settingService;
        _storeContext = storeContext;
        _permissionService = permissionService;
    }

    public async Task<IActionResult> Configure()
    {
        if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageWidgets))
            return AccessDeniedView();

        var store = await _storeContext.GetCurrentStoreAsync();
        var s = await _settingService.LoadSettingAsync<ChildDonationSettings>(store.Id);

        var model = new ConfigurationModel
        {
            ChildName = s.ChildName,
            ChildImageUrl = s.ChildImageUrl,
            WidgetTitle = s.WidgetTitle,
            WidgetSubtitle = s.WidgetSubtitle,

            Cat1Name = s.Cat1Name, Cat1IconUrl = s.Cat1IconUrl, Cat1OverlayUrl = s.Cat1OverlayUrl,
            Cat1Price = s.Cat1Price, Cat1ProductId = s.Cat1ProductId,

            Cat2Name = s.Cat2Name, Cat2IconUrl = s.Cat2IconUrl, Cat2OverlayUrl = s.Cat2OverlayUrl,
            Cat2Price = s.Cat2Price, Cat2ProductId = s.Cat2ProductId,

            Cat3Name = s.Cat3Name, Cat3IconUrl = s.Cat3IconUrl, Cat3OverlayUrl = s.Cat3OverlayUrl,
            Cat3Price = s.Cat3Price, Cat3ProductId = s.Cat3ProductId,

            Cat4Name = s.Cat4Name, Cat4IconUrl = s.Cat4IconUrl, Cat4OverlayUrl = s.Cat4OverlayUrl,
            Cat4Price = s.Cat4Price, Cat4ProductId = s.Cat4ProductId,
        };

        return View("~/Plugins/Widgets.ChildDonation/Views/Configure.cshtml", model);
    }

    [HttpPost]
    public async Task<IActionResult> Configure(ConfigurationModel model)
    {
        if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageWidgets))
            return AccessDeniedView();

        var store = await _storeContext.GetCurrentStoreAsync();
        var s = await _settingService.LoadSettingAsync<ChildDonationSettings>(store.Id);

        s.ChildName = model.ChildName;
        s.ChildImageUrl = model.ChildImageUrl;
        s.WidgetTitle = model.WidgetTitle;
        s.WidgetSubtitle = model.WidgetSubtitle;

        s.Cat1Name = model.Cat1Name; s.Cat1IconUrl = model.Cat1IconUrl; s.Cat1OverlayUrl = model.Cat1OverlayUrl;
        s.Cat1Price = model.Cat1Price; s.Cat1ProductId = model.Cat1ProductId;

        s.Cat2Name = model.Cat2Name; s.Cat2IconUrl = model.Cat2IconUrl; s.Cat2OverlayUrl = model.Cat2OverlayUrl;
        s.Cat2Price = model.Cat2Price; s.Cat2ProductId = model.Cat2ProductId;

        s.Cat3Name = model.Cat3Name; s.Cat3IconUrl = model.Cat3IconUrl; s.Cat3OverlayUrl = model.Cat3OverlayUrl;
        s.Cat3Price = model.Cat3Price; s.Cat3ProductId = model.Cat3ProductId;

        s.Cat4Name = model.Cat4Name; s.Cat4IconUrl = model.Cat4IconUrl; s.Cat4OverlayUrl = model.Cat4OverlayUrl;
        s.Cat4Price = model.Cat4Price; s.Cat4ProductId = model.Cat4ProductId;

        await _settingService.SaveSettingAsync(s, store.Id);

        ViewBag.Saved = true;
        return await Configure();
    }
}
