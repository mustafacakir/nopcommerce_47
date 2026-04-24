using Nop.Plugin.Widgets.Logo.MinikhediyelCom.Components;
using Nop.Services.Cms;
using Nop.Services.Plugins;

namespace Nop.Plugin.Widgets.Logo.MinikhediyelCom;

/// <summary>
/// minikhediyen.com — text logo widget (voyage_theme_logo zone)
/// </summary>
public class LogoMinikhediyelComPlugin : BasePlugin, IWidgetPlugin
{
    public bool HideInWidgetList => false;

    public Type GetWidgetViewComponent(string widgetZone) => typeof(LogoMinikhediyelComViewComponent);

    public Task<IList<string>> GetWidgetZonesAsync()
    {
        return Task.FromResult<IList<string>>(new List<string> { "voyage_theme_logo" });
    }

    public override Task InstallAsync() => base.InstallAsync();

    public override Task UninstallAsync() => base.UninstallAsync();
}
