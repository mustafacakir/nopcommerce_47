using Nop.Plugin.Widgets.CampaignProgress.Components;
using Nop.Services.Cms;
using Nop.Services.Plugins;
using Nop.Web.Framework.Infrastructure;

namespace Nop.Plugin.Widgets.CampaignProgress;

public class CampaignProgressPlugin : BasePlugin, IWidgetPlugin
{
    public bool HideInWidgetList => false;
    public Type GetWidgetViewComponent(string widgetZone) => typeof(CampaignProgressViewComponent);

    public Task<IList<string>> GetWidgetZonesAsync()
    {
        return Task.FromResult<IList<string>>(new List<string> { PublicWidgetZones.HomepageTop });
    }

    public override string GetConfigurationPageUrl() => "/Admin/CampaignAdmin/List";

    public override async Task InstallAsync()
    {
        await base.InstallAsync();
    }

    public override async Task UninstallAsync()
    {
        await base.UninstallAsync();
    }
}
