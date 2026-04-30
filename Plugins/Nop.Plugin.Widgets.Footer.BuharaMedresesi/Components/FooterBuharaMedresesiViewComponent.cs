using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Plugin.Widgets.Footer.BuharaMedresesi.Models;
using Nop.Services.Configuration;
using Nop.Web.Framework.Components;

namespace Nop.Plugin.Widgets.Footer.BuharaMedresesi.Components;

public class FooterBuharaMedresesiViewComponent : NopViewComponent
{
    private readonly IStoreContext _storeContext;
    private readonly ISettingService _settingService;

    public FooterBuharaMedresesiViewComponent(IStoreContext storeContext, ISettingService settingService)
    {
        _storeContext = storeContext;
        _settingService = settingService;
    }

    public async Task<IViewComponentResult> InvokeAsync(string widgetZone, object additionalData)
    {
        var store = await _storeContext.GetCurrentStoreAsync();
        var s = await _settingService.LoadSettingAsync<FooterBuharaMedresesiSettings>(store.Id);

        var model = new PublicInfoModel
        {
            CtaTitle      = s.CtaTitle,
            CtaSubtitle   = s.CtaSubtitle,
            CtaButtonText = s.CtaButtonText,
            CtaButtonUrl  = s.CtaButtonUrl,
            LogoUrl       = s.LogoUrl,
            Description   = s.Description,
            FacebookUrl   = s.FacebookUrl,
            InstagramUrl  = s.InstagramUrl,
            TwitterUrl    = s.TwitterUrl,
            YoutubeUrl    = s.YoutubeUrl,
            LinkedinUrl   = s.LinkedinUrl,
            TiktokUrl     = s.TiktokUrl,
            WhatsappUrl   = s.WhatsappUrl,
            QuickLinksTitle = s.QuickLinksTitle,
            QuickLinks = new List<LinkItem>
            {
                new() { Text = s.QuickLink1Text, Url = s.QuickLink1Url },
                new() { Text = s.QuickLink2Text, Url = s.QuickLink2Url },
                new() { Text = s.QuickLink3Text, Url = s.QuickLink3Url },
                new() { Text = s.QuickLink4Text, Url = s.QuickLink4Url },
                new() { Text = s.QuickLink5Text, Url = s.QuickLink5Url },
                new() { Text = s.QuickLink6Text, Url = s.QuickLink6Url },
            }.Where(x => !string.IsNullOrEmpty(x.Text)).ToList(),
            CategoryLinksTitle = s.CategoryLinksTitle,
            CategoryLinks = new List<LinkItem>
            {
                new() { Text = s.CategoryLink1Text, Url = s.CategoryLink1Url },
                new() { Text = s.CategoryLink2Text, Url = s.CategoryLink2Url },
                new() { Text = s.CategoryLink3Text, Url = s.CategoryLink3Url },
                new() { Text = s.CategoryLink4Text, Url = s.CategoryLink4Url },
            }.Where(x => !string.IsNullOrEmpty(x.Text)).ToList(),
            ContactTitle  = s.ContactTitle,
            Address       = s.Address,
            Phone         = s.Phone,
            Email         = s.Email,
            CopyrightText = s.CopyrightText,
            KvkkUrl       = s.KvkkUrl,
            GizlilikUrl   = s.GizlilikUrl
        };

        return View("~/Plugins/Widgets.Footer.BuharaMedresesi/Views/PublicInfo.cshtml", model);
    }
}
