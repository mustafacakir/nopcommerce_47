using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Plugin.Widgets.RecurringDonation.Models;
using Nop.Services.Configuration;
using Nop.Web.Framework.Components;

namespace Nop.Plugin.Widgets.RecurringDonation.Components;

public class RecurringDonationViewComponent : NopViewComponent
{
    private readonly IStoreContext _storeContext;
    private readonly ISettingService _settingService;

    public RecurringDonationViewComponent(IStoreContext storeContext, ISettingService settingService)
    {
        _storeContext = storeContext;
        _settingService = settingService;
    }

    public async Task<IViewComponentResult> InvokeAsync(string widgetZone, object additionalData)
    {
        var store = await _storeContext.GetCurrentStoreAsync();
        var s = await _settingService.LoadSettingAsync<RecurringDonationSettings>(store.Id);

        var amounts = new List<AmountOption>
        {
            new() { Label = s.Amount1Label, Url = s.Amount1Url },
            new() { Label = s.Amount2Label, Url = s.Amount2Url },
            new() { Label = s.Amount3Label, Url = s.Amount3Url },
            new() { Label = s.Amount4Label, Url = s.Amount4Url },
        }.Where(a => !string.IsNullOrEmpty(a.Label)).ToList();

        var model = new PublicInfoModel
        {
            Title = s.Title,
            Subtitle = s.Subtitle,
            Amounts = amounts,
            CustomAmountUrl = s.CustomAmountUrl
        };

        return View("~/Plugins/Widgets.RecurringDonation/Views/PublicInfo.cshtml", model);
    }
}
