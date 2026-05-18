using Nop.Plugin.Widgets.HeroSlider.Components;
using Nop.Services.Cms;
using Nop.Services.Plugins;
using Nop.Web.Framework.Infrastructure;

namespace Nop.Plugin.Widgets.HeroSlider;

public class HeroSliderPlugin : BasePlugin, IWidgetPlugin
{
    public bool HideInWidgetList => false;
    public Type GetWidgetViewComponent(string widgetZone) => typeof(HeroSliderViewComponent);

    public Task<IList<string>> GetWidgetZonesAsync() =>
        Task.FromResult<IList<string>>(new List<string> { PublicWidgetZones.HomepageTop });

    public override string GetConfigurationPageUrl() => "/Admin/HeroSliderAdmin/List";

    public override async Task InstallAsync() => await base.InstallAsync();
    public override async Task UninstallAsync() => await base.UninstallAsync();
}
