using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Plugin.Widgets.DonationTransparency.Models;
using Nop.Services.Configuration;
using Nop.Web.Framework.Components;

namespace Nop.Plugin.Widgets.DonationTransparency.Components;

public class DonationTransparencyViewComponent : NopViewComponent
{
    private readonly ISettingService _settingService;
    private readonly IStoreContext _storeContext;

    public DonationTransparencyViewComponent(ISettingService settingService, IStoreContext storeContext)
    {
        _settingService = settingService;
        _storeContext = storeContext;
    }

    public async Task<IViewComponentResult> InvokeAsync(string widgetZone, object? additionalData = null)
    {
        var store = await _storeContext.GetCurrentStoreAsync();
        var s = await _settingService.LoadSettingAsync<DonationTransparencySettings>(store.Id);

        var raw = new[]
        {
            (s.Item1Label, s.Item1Percent, s.Item1Color, s.Item1Icon, s.Item1Description),
            (s.Item2Label, s.Item2Percent, s.Item2Color, s.Item2Icon, s.Item2Description),
            (s.Item3Label, s.Item3Percent, s.Item3Color, s.Item3Icon, s.Item3Description),
            (s.Item4Label, s.Item4Percent, s.Item4Color, s.Item4Icon, s.Item4Description),
            (s.Item5Label, s.Item5Percent, s.Item5Color, s.Item5Icon, s.Item5Description),
        };

        var items = raw
            .Where(x => !string.IsNullOrWhiteSpace(x.Item1) && x.Item2 > 0)
            .Select(x => new TransparencyItem
            {
                Label = x.Item1,
                Percent = x.Item2,
                Color = x.Item3,
                Icon = x.Item4,
                Description = x.Item5
            })
            .ToList();

        return View("~/Plugins/Widgets.DonationTransparency/Views/Components/DonationTransparency/Default.cshtml",
            new PublicInfoModel
            {
                SectionTitle = s.SectionTitle,
                SectionSubtitle = s.SectionSubtitle,
                ReportLinkText = s.ReportLinkText,
                ReportLinkUrl = s.ReportLinkUrl,
                Items = items
            });
    }
}
