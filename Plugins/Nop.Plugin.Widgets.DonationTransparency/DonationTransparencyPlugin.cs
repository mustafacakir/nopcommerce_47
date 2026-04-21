using Nop.Core;
using Nop.Plugin.Widgets.DonationTransparency.Components;
using Nop.Services.Cms;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Plugins;
using Nop.Web.Framework.Infrastructure;

namespace Nop.Plugin.Widgets.DonationTransparency;

public class DonationTransparencyPlugin : BasePlugin, IWidgetPlugin
{
    private readonly ILocalizationService _localizationService;
    private readonly ISettingService _settingService;
    private readonly IWebHelper _webHelper;

    public DonationTransparencyPlugin(ILocalizationService localizationService, ISettingService settingService, IWebHelper webHelper)
    {
        _localizationService = localizationService;
        _settingService = settingService;
        _webHelper = webHelper;
    }

    public bool HideInWidgetList => false;
    public Type GetWidgetViewComponent(string widgetZone) => typeof(DonationTransparencyViewComponent);

    public Task<IList<string>> GetWidgetZonesAsync()
    {
        return Task.FromResult<IList<string>>(new List<string> { PublicWidgetZones.HomepageBottom });
    }

    public override string GetConfigurationPageUrl() =>
        $"{_webHelper.GetStoreLocation()}Admin/WidgetsDonationTransparency/Configure";

    public override async Task InstallAsync()
    {
        await _settingService.SaveSettingAsync(new DonationTransparencySettings());
        await base.InstallAsync();
    }

    public override async Task UninstallAsync()
    {
        await _settingService.DeleteSettingAsync<DonationTransparencySettings>();
        await base.UninstallAsync();
    }
}
