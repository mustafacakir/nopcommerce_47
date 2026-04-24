using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Plugin.Widgets.CategoryShowcase.Models;
using Nop.Services.Configuration;
using Nop.Web.Framework.Components;

namespace Nop.Plugin.Widgets.CategoryShowcase.Components;

public class CategoryShowcaseViewComponent : NopViewComponent
{
    private readonly IStoreContext _storeContext;
    private readonly ISettingService _settingService;

    public CategoryShowcaseViewComponent(IStoreContext storeContext, ISettingService settingService)
    {
        _storeContext = storeContext;
        _settingService = settingService;
    }

    public async Task<IViewComponentResult> InvokeAsync(string widgetZone, object additionalData)
    {
        var store = await _storeContext.GetCurrentStoreAsync();
        var settings = await _settingService.LoadSettingAsync<CategoryShowcaseSettings>(store.Id);

        var model = new PublicInfoModel
        {
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

        return View("~/Plugins/Widgets.CategoryShowcase/Views/PublicInfo.cshtml", model);
    }
}
