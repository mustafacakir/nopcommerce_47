using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Plugin.Widgets.ChildDonation.Models;
using Nop.Services.Configuration;
using Nop.Web.Framework.Components;

namespace Nop.Plugin.Widgets.ChildDonation.Components;

public class ChildDonationViewComponent : NopViewComponent
{
    private readonly IStoreContext _storeContext;
    private readonly ISettingService _settingService;

    public ChildDonationViewComponent(IStoreContext storeContext, ISettingService settingService)
    {
        _storeContext = storeContext;
        _settingService = settingService;
    }

    public async Task<IViewComponentResult> InvokeAsync(string widgetZone, object additionalData)
    {
        var store = await _storeContext.GetCurrentStoreAsync();
        var s = await _settingService.LoadSettingAsync<ChildDonationSettings>(store.Id);

        var title = (s.WidgetTitle ?? "Merhaba, benim adım {name}!")
            .Replace("{name}", s.ChildName ?? "");

        var model = new ChildDonationPublicModel
        {
            ChildName = s.ChildName,
            ChildImageUrl = s.ChildImageUrl,
            Title = title,
            Subtitle = s.WidgetSubtitle,
            Categories = new List<CategoryItemModel>
            {
                new() { Name = s.Cat1Name, IconUrl = s.Cat1IconUrl, OverlayUrl = s.Cat1OverlayUrl, Price = s.Cat1Price, ProductId = s.Cat1ProductId },
                new() { Name = s.Cat2Name, IconUrl = s.Cat2IconUrl, OverlayUrl = s.Cat2OverlayUrl, Price = s.Cat2Price, ProductId = s.Cat2ProductId },
                new() { Name = s.Cat3Name, IconUrl = s.Cat3IconUrl, OverlayUrl = s.Cat3OverlayUrl, Price = s.Cat3Price, ProductId = s.Cat3ProductId },
                new() { Name = s.Cat4Name, IconUrl = s.Cat4IconUrl, OverlayUrl = s.Cat4OverlayUrl, Price = s.Cat4Price, ProductId = s.Cat4ProductId },
            }
        };

        return View("~/Plugins/Widgets.ChildDonation/Views/Components/ChildDonation/Default.cshtml", model);
    }
}
