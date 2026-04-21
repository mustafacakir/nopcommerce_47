using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Plugin.Widgets.VolunteerCta.Models;
using Nop.Services.Configuration;
using Nop.Web.Framework.Components;

namespace Nop.Plugin.Widgets.VolunteerCta.Components;

public class VolunteerCtaViewComponent : NopViewComponent
{
    private readonly ISettingService _settingService;
    private readonly IStoreContext _storeContext;

    public VolunteerCtaViewComponent(ISettingService settingService, IStoreContext storeContext)
    {
        _settingService = settingService;
        _storeContext = storeContext;
    }

    public async Task<IViewComponentResult> InvokeAsync(string widgetZone, object? additionalData = null)
    {
        var store = await _storeContext.GetCurrentStoreAsync();
        var s = await _settingService.LoadSettingAsync<VolunteerCtaSettings>(store.Id);

        return View("~/Plugins/Widgets.VolunteerCta/Views/Components/VolunteerCta/Default.cshtml", new PublicInfoModel
        {
            Title = s.Title,
            Subtitle = s.Subtitle,
            ButtonText = s.ButtonText,
            ButtonUrl = s.ButtonUrl,
            Stat1Value = s.Stat1Value, Stat1Label = s.Stat1Label,
            Stat2Value = s.Stat2Value, Stat2Label = s.Stat2Label,
            Stat3Value = s.Stat3Value, Stat3Label = s.Stat3Label,
        });
    }
}
