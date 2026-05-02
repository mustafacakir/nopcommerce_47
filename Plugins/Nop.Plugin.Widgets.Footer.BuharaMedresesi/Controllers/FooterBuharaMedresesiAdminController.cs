using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Plugin.Widgets.Footer.BuharaMedresesi.Models;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Messages;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;

namespace Nop.Plugin.Widgets.Footer.BuharaMedresesi.Controllers;

[Area(AreaNames.ADMIN)]
[AutoValidateAntiforgeryToken]
public class FooterBuharaMedresesiAdminController : BasePluginController
{
    private readonly ILocalizationService _localizationService;
    private readonly INotificationService _notificationService;
    private readonly ISettingService _settingService;
    private readonly IStoreContext _storeContext;

    public FooterBuharaMedresesiAdminController(
        ILocalizationService localizationService,
        INotificationService notificationService,
        ISettingService settingService,
        IStoreContext storeContext)
    {
        _localizationService = localizationService;
        _notificationService = notificationService;
        _settingService = settingService;
        _storeContext = storeContext;
    }

    public async Task<IActionResult> Configure()
    {
        var storeScope = await _storeContext.GetActiveStoreScopeConfigurationAsync();
        var s = await _settingService.LoadSettingAsync<FooterBuharaMedresesiSettings>(storeScope);

        var model = new ConfigurationModel
        {
            ActiveStoreScopeConfiguration = storeScope,
            CtaTitle      = s.CtaTitle,      CtaSubtitle   = s.CtaSubtitle,
            CtaButtonText = s.CtaButtonText,  CtaButtonUrl  = s.CtaButtonUrl,
            LogoUrl       = s.LogoUrl,        Description   = s.Description,
            FacebookUrl   = s.FacebookUrl,    InstagramUrl  = s.InstagramUrl,
            TwitterUrl    = s.TwitterUrl,     YoutubeUrl    = s.YoutubeUrl,
            LinkedinUrl   = s.LinkedinUrl,    TiktokUrl     = s.TiktokUrl,
            WhatsappUrl   = s.WhatsappUrl,
            QuickLinksTitle = s.QuickLinksTitle,
            QuickLink1Text = s.QuickLink1Text, QuickLink1Url = s.QuickLink1Url,
            QuickLink2Text = s.QuickLink2Text, QuickLink2Url = s.QuickLink2Url,
            QuickLink3Text = s.QuickLink3Text, QuickLink3Url = s.QuickLink3Url,
            QuickLink4Text = s.QuickLink4Text, QuickLink4Url = s.QuickLink4Url,
            QuickLink5Text = s.QuickLink5Text, QuickLink5Url = s.QuickLink5Url,
            QuickLink6Text = s.QuickLink6Text, QuickLink6Url = s.QuickLink6Url,
            CategoryLinksTitle = s.CategoryLinksTitle,
            CategoryLink1Text = s.CategoryLink1Text, CategoryLink1Url = s.CategoryLink1Url,
            CategoryLink2Text = s.CategoryLink2Text, CategoryLink2Url = s.CategoryLink2Url,
            CategoryLink3Text = s.CategoryLink3Text, CategoryLink3Url = s.CategoryLink3Url,
            CategoryLink4Text = s.CategoryLink4Text, CategoryLink4Url = s.CategoryLink4Url,
            ContactTitle  = s.ContactTitle,   Address       = s.Address,
            Phone         = s.Phone,          Email         = s.Email,
            CopyrightText = s.CopyrightText,  KvkkUrl       = s.KvkkUrl,
            GizlilikUrl   = s.GizlilikUrl
        };

        return View("~/Plugins/Widgets.Footer.BuharaMedresesi/Views/Configure.cshtml", model);
    }

    [HttpPost]
    public async Task<IActionResult> Configure(ConfigurationModel model)
    {
        var storeScope = await _storeContext.GetActiveStoreScopeConfigurationAsync();
        var s = await _settingService.LoadSettingAsync<FooterBuharaMedresesiSettings>(storeScope);

        s.CtaTitle      = model.CtaTitle;      s.CtaSubtitle   = model.CtaSubtitle;
        s.CtaButtonText = model.CtaButtonText;  s.CtaButtonUrl  = model.CtaButtonUrl;
        s.LogoUrl       = model.LogoUrl;        s.Description   = model.Description;
        s.FacebookUrl   = model.FacebookUrl;    s.InstagramUrl  = model.InstagramUrl;
        s.TwitterUrl    = model.TwitterUrl;     s.YoutubeUrl    = model.YoutubeUrl;
        s.LinkedinUrl   = model.LinkedinUrl;    s.TiktokUrl     = model.TiktokUrl;
        s.WhatsappUrl   = model.WhatsappUrl;
        s.QuickLinksTitle = model.QuickLinksTitle;
        s.QuickLink1Text = model.QuickLink1Text; s.QuickLink1Url = model.QuickLink1Url;
        s.QuickLink2Text = model.QuickLink2Text; s.QuickLink2Url = model.QuickLink2Url;
        s.QuickLink3Text = model.QuickLink3Text; s.QuickLink3Url = model.QuickLink3Url;
        s.QuickLink4Text = model.QuickLink4Text; s.QuickLink4Url = model.QuickLink4Url;
        s.QuickLink5Text = model.QuickLink5Text; s.QuickLink5Url = model.QuickLink5Url;
        s.QuickLink6Text = model.QuickLink6Text; s.QuickLink6Url = model.QuickLink6Url;
        s.CategoryLinksTitle = model.CategoryLinksTitle;
        s.CategoryLink1Text = model.CategoryLink1Text; s.CategoryLink1Url = model.CategoryLink1Url;
        s.CategoryLink2Text = model.CategoryLink2Text; s.CategoryLink2Url = model.CategoryLink2Url;
        s.CategoryLink3Text = model.CategoryLink3Text; s.CategoryLink3Url = model.CategoryLink3Url;
        s.CategoryLink4Text = model.CategoryLink4Text; s.CategoryLink4Url = model.CategoryLink4Url;
        s.ContactTitle  = model.ContactTitle;   s.Address       = model.Address;
        s.Phone         = model.Phone;          s.Email         = model.Email;
        s.CopyrightText = model.CopyrightText;  s.KvkkUrl       = model.KvkkUrl;
        s.GizlilikUrl   = model.GizlilikUrl;

        await _settingService.SaveSettingAsync(s, storeScope);
        await _settingService.ClearCacheAsync();
        _notificationService.SuccessNotification(await _localizationService.GetResourceAsync("Admin.Plugins.Saved"));

        return await Configure();
    }
}
