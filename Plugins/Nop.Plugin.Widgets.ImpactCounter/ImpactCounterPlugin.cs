using Nop.Core;
using Nop.Plugin.Widgets.ImpactCounter.Components;
using Nop.Services.Cms;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Plugins;
using Nop.Web.Framework.Infrastructure;

namespace Nop.Plugin.Widgets.ImpactCounter;

public class ImpactCounterPlugin : BasePlugin, IWidgetPlugin
{
    private readonly ILocalizationService _localizationService;
    private readonly ISettingService _settingService;
    private readonly IWebHelper _webHelper;

    public ImpactCounterPlugin(ILocalizationService localizationService, ISettingService settingService, IWebHelper webHelper)
    {
        _localizationService = localizationService;
        _settingService = settingService;
        _webHelper = webHelper;
    }

    public bool HideInWidgetList => false;
    public Type GetWidgetViewComponent(string widgetZone) => typeof(ImpactCounterViewComponent);

    public Task<IList<string>> GetWidgetZonesAsync()
    {
        return Task.FromResult<IList<string>>(new List<string> { PublicWidgetZones.HomepageTop });
    }

    public override string GetConfigurationPageUrl() =>
        $"{_webHelper.GetStoreLocation()}Admin/WidgetsImpactCounter/Configure";

    public override async Task InstallAsync()
    {
        await _settingService.SaveSettingAsync(new ImpactCounterSettings());
        await _localizationService.AddOrUpdateLocaleResourceAsync(new Dictionary<string, string>
        {
            ["Plugins.Widgets.ImpactCounter.Stat1Icon"] = "İkon 1",
            ["Plugins.Widgets.ImpactCounter.Stat1Value"] = "Değer 1",
            ["Plugins.Widgets.ImpactCounter.Stat1Label"] = "Etiket 1",
            ["Plugins.Widgets.ImpactCounter.Stat2Icon"] = "İkon 2",
            ["Plugins.Widgets.ImpactCounter.Stat2Value"] = "Değer 2",
            ["Plugins.Widgets.ImpactCounter.Stat2Label"] = "Etiket 2",
            ["Plugins.Widgets.ImpactCounter.Stat3Icon"] = "İkon 3",
            ["Plugins.Widgets.ImpactCounter.Stat3Value"] = "Değer 3",
            ["Plugins.Widgets.ImpactCounter.Stat3Label"] = "Etiket 3",
            ["Plugins.Widgets.ImpactCounter.Stat4Icon"] = "İkon 4",
            ["Plugins.Widgets.ImpactCounter.Stat4Value"] = "Değer 4",
            ["Plugins.Widgets.ImpactCounter.Stat4Label"] = "Etiket 4",
        });
        await base.InstallAsync();
    }

    public override async Task UninstallAsync()
    {
        await _settingService.DeleteSettingAsync<ImpactCounterSettings>();
        await _localizationService.DeleteLocaleResourcesAsync("Plugins.Widgets.ImpactCounter");
        await base.UninstallAsync();
    }
}

