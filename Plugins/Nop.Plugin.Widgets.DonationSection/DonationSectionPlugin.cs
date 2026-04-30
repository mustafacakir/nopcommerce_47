using Nop.Plugin.Widgets.DonationSection.Components;
using Nop.Services.Cms;
using Nop.Services.Plugins;
using Nop.Web.Framework.Infrastructure;

namespace Nop.Plugin.Widgets.DonationSection;

public class DonationSectionPlugin : BasePlugin, IWidgetPlugin
{
    public bool HideInWidgetList => false;
    public Type GetWidgetViewComponent(string widgetZone) => typeof(DonationSectionViewComponent);

    public Task<IList<string>> GetWidgetZonesAsync() =>
        Task.FromResult<IList<string>>(new List<string> { PublicWidgetZones.HomepageTop });

    public override async Task InstallAsync() => await base.InstallAsync();
    public override async Task UninstallAsync() => await base.UninstallAsync();
}
