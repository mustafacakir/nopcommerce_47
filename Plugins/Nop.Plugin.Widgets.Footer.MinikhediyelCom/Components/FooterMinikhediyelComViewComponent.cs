using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Plugin.Widgets.Footer.MinikhediyelCom.Models;
using Nop.Services.Configuration;
using Nop.Web.Framework.Components;

namespace Nop.Plugin.Widgets.Footer.MinikhediyelCom.Components;

public class FooterMinikhediyelComViewComponent : NopViewComponent
{
    private readonly IStoreContext _storeContext;
    private readonly ISettingService _settingService;

    public FooterMinikhediyelComViewComponent(IStoreContext storeContext, ISettingService settingService)
    {
        _storeContext = storeContext;
        _settingService = settingService;
    }

    public async Task<IViewComponentResult> InvokeAsync(string widgetZone, object additionalData)
    {
        var store = await _storeContext.GetCurrentStoreAsync();
        var s = await _settingService.LoadSettingAsync<FooterMinikhediyelComSettings>(store.Id);

        var model = new PublicInfoModel
        {
            StoreName = s.StoreName,
            LogoLetter = s.LogoLetter,
            Tagline = s.Tagline,
            InstagramUrl = s.InstagramUrl,
            FacebookUrl = s.FacebookUrl,
            TwitterUrl = s.TwitterUrl,
            QuickLinksTitle = s.QuickLinksTitle,
            QuickLinks = new List<(string, string)>
            {
                (s.QuickLink1Text, s.QuickLink1Url),
                (s.QuickLink2Text, s.QuickLink2Url),
                (s.QuickLink3Text, s.QuickLink3Url),
                (s.QuickLink4Text, s.QuickLink4Url),
            }.Where(x => !string.IsNullOrEmpty(x.Item1)).ToList(),
            SupportTitle = s.SupportTitle,
            SupportLinks = new List<(string, string)>
            {
                (s.SupportLink1Text, s.SupportLink1Url),
                (s.SupportLink2Text, s.SupportLink2Url),
                (s.SupportLink3Text, s.SupportLink3Url),
                (s.SupportLink4Text, s.SupportLink4Url),
            }.Where(x => !string.IsNullOrEmpty(x.Item1)).ToList(),
            ContactTitle = s.ContactTitle,
            Address = s.Address,
            Phone = s.Phone,
            Email = s.Email,
            CopyrightText = s.CopyrightText
        };

        return View("~/Plugins/Widgets.Footer.MinikhediyelCom/Views/PublicInfo.cshtml", model);
    }
}
