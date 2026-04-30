using Nop.Plugin.Widgets.ChildDonation.Components;
using Nop.Services.Cms;
using Nop.Services.Configuration;
using Nop.Services.Plugins;
using Nop.Web.Framework.Infrastructure;

namespace Nop.Plugin.Widgets.ChildDonation;

public class ChildDonationPlugin : BasePlugin, IWidgetPlugin
{
    private readonly ISettingService _settingService;

    public ChildDonationPlugin(ISettingService settingService)
    {
        _settingService = settingService;
    }

    public bool HideInWidgetList => false;
    public Type GetWidgetViewComponent(string widgetZone) => typeof(ChildDonationViewComponent);

    public Task<IList<string>> GetWidgetZonesAsync() =>
        Task.FromResult<IList<string>>(new List<string> { PublicWidgetZones.HomepageTop });

    public override string GetConfigurationPageUrl() => "/Admin/ChildDonationAdmin/Configure";

    public override async Task InstallAsync()
    {
        await _settingService.SaveSettingAsync(new ChildDonationSettings());
        await base.InstallAsync();
    }

    public override async Task UninstallAsync()
    {
        await _settingService.DeleteSettingAsync<ChildDonationSettings>();
        await base.UninstallAsync();
    }
}
