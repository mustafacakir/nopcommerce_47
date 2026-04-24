using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Plugin.Widgets.Footer.MinikhediyelCom.Models;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Messages;
using Nop.Services.Security;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;

namespace Nop.Plugin.Widgets.Footer.MinikhediyelCom.Controllers;

[Area(AreaNames.ADMIN)]
[AutoValidateAntiforgeryToken]
public class WidgetsFooterMinikhediyelComController : BasePluginController
{
    private readonly ILocalizationService _localizationService;
    private readonly INotificationService _notificationService;
    private readonly IPermissionService _permissionService;
    private readonly ISettingService _settingService;
    private readonly IStoreContext _storeContext;

    public WidgetsFooterMinikhediyelComController(
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
        var s = await _settingService.LoadSettingAsync<FooterMinikhediyelComSettings>(storeScope);

        var model = new ConfigurationModel
        {
            ActiveStoreScopeConfiguration = storeScope,
            StoreName = s.StoreName,
            LogoLetter = s.LogoLetter,
            Tagline = s.Tagline,
            InstagramUrl = s.InstagramUrl,
            FacebookUrl = s.FacebookUrl,
            TwitterUrl = s.TwitterUrl,
            QuickLinksTitle = s.QuickLinksTitle,
            QuickLink1Text = s.QuickLink1Text, QuickLink1Url = s.QuickLink1Url,
            QuickLink2Text = s.QuickLink2Text, QuickLink2Url = s.QuickLink2Url,
            QuickLink3Text = s.QuickLink3Text, QuickLink3Url = s.QuickLink3Url,
            QuickLink4Text = s.QuickLink4Text, QuickLink4Url = s.QuickLink4Url,
            SupportTitle = s.SupportTitle,
            SupportLink1Text = s.SupportLink1Text, SupportLink1Url = s.SupportLink1Url,
            SupportLink2Text = s.SupportLink2Text, SupportLink2Url = s.SupportLink2Url,
            SupportLink3Text = s.SupportLink3Text, SupportLink3Url = s.SupportLink3Url,
            SupportLink4Text = s.SupportLink4Text, SupportLink4Url = s.SupportLink4Url,
            ContactTitle = s.ContactTitle,
            Address = s.Address,
            Phone = s.Phone,
            Email = s.Email,
            CopyrightText = s.CopyrightText
        };

        return View("~/Plugins/Widgets.Footer.MinikhediyelCom/Views/Configure.cshtml", model);
    }

    [HttpPost]
    public async Task<IActionResult> Configure(ConfigurationModel model)
    {
        if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageWidgets))
            return AccessDeniedView();

        var storeScope = await _storeContext.GetActiveStoreScopeConfigurationAsync();
        var s = await _settingService.LoadSettingAsync<FooterMinikhediyelComSettings>(storeScope);

        s.StoreName = model.StoreName;
        s.LogoLetter = model.LogoLetter;
        s.Tagline = model.Tagline;
        s.InstagramUrl = model.InstagramUrl;
        s.FacebookUrl = model.FacebookUrl;
        s.TwitterUrl = model.TwitterUrl;
        s.QuickLinksTitle = model.QuickLinksTitle;
        s.QuickLink1Text = model.QuickLink1Text; s.QuickLink1Url = model.QuickLink1Url;
        s.QuickLink2Text = model.QuickLink2Text; s.QuickLink2Url = model.QuickLink2Url;
        s.QuickLink3Text = model.QuickLink3Text; s.QuickLink3Url = model.QuickLink3Url;
        s.QuickLink4Text = model.QuickLink4Text; s.QuickLink4Url = model.QuickLink4Url;
        s.SupportTitle = model.SupportTitle;
        s.SupportLink1Text = model.SupportLink1Text; s.SupportLink1Url = model.SupportLink1Url;
        s.SupportLink2Text = model.SupportLink2Text; s.SupportLink2Url = model.SupportLink2Url;
        s.SupportLink3Text = model.SupportLink3Text; s.SupportLink3Url = model.SupportLink3Url;
        s.SupportLink4Text = model.SupportLink4Text; s.SupportLink4Url = model.SupportLink4Url;
        s.ContactTitle = model.ContactTitle;
        s.Address = model.Address;
        s.Phone = model.Phone;
        s.Email = model.Email;
        s.CopyrightText = model.CopyrightText;

        await _settingService.SaveSettingAsync(s, storeScope);
        await _settingService.ClearCacheAsync();
        _notificationService.SuccessNotification(await _localizationService.GetResourceAsync("Admin.Plugins.Saved"));

        return await Configure();
    }
}
