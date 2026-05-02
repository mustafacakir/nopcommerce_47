using Nop.Plugin.Widgets.DonationGrid.Components;
using Nop.Services.Cms;
using Nop.Services.Configuration;
using Nop.Services.Plugins;
using Nop.Web.Framework.Infrastructure;

namespace Nop.Plugin.Widgets.DonationGrid;

public class DonationGridPlugin : BasePlugin, IWidgetPlugin
{
    private readonly ISettingService _settingService;

    public DonationGridPlugin(ISettingService settingService)
    {
        _settingService = settingService;
    }

    public bool HideInWidgetList => false;

    public Type GetWidgetViewComponent(string widgetZone) => typeof(DonationGridViewComponent);

    public Task<IList<string>> GetWidgetZonesAsync() =>
        Task.FromResult<IList<string>>(new List<string> { PublicWidgetZones.CategoryDetailsTop });

    public override string GetConfigurationPageUrl() => "/Admin/DonationGrid/Configure";

    public override async Task InstallAsync()
    {
        await _settingService.SaveSettingAsync(new DonationGridSettings());
        await base.InstallAsync();
    }

    public override async Task UninstallAsync()
    {
        await _settingService.DeleteSettingAsync<DonationGridSettings>();
        await base.UninstallAsync();
    }
}
