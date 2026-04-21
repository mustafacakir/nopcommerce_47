using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Plugin.Widgets.ImpactCounter.Models;
using Nop.Services.Configuration;
using Nop.Web.Framework.Components;

namespace Nop.Plugin.Widgets.ImpactCounter.Components;

public class ImpactCounterViewComponent : NopViewComponent
{
    private readonly IStoreContext _storeContext;
    private readonly ISettingService _settingService;

    public ImpactCounterViewComponent(IStoreContext storeContext, ISettingService settingService)
    {
        _storeContext = storeContext;
        _settingService = settingService;
    }

    public async Task<IViewComponentResult> InvokeAsync(string widgetZone, object additionalData)
    {
        var store = await _storeContext.GetCurrentStoreAsync();
        var s = await _settingService.LoadSettingAsync<ImpactCounterSettings>(store.Id);

        var model = new PublicInfoModel
        {
            Stats = new List<StatItem>
            {
                new() { Icon = s.Stat1Icon, Value = s.Stat1Value, Label = s.Stat1Label },
                new() { Icon = s.Stat2Icon, Value = s.Stat2Value, Label = s.Stat2Label },
                new() { Icon = s.Stat3Icon, Value = s.Stat3Value, Label = s.Stat3Label },
                new() { Icon = s.Stat4Icon, Value = s.Stat4Value, Label = s.Stat4Label },
            }
        };

        return View("~/Plugins/Widgets.ImpactCounter/Views/PublicInfo.cshtml", model);
    }
}
