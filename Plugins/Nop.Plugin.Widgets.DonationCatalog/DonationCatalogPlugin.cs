using Nop.Plugin.Widgets.DonationCatalog.Components;
using Nop.Services.Cms;
using Nop.Services.Plugins;
using Nop.Web.Framework.Infrastructure;

namespace Nop.Plugin.Widgets.DonationCatalog;

public class DonationCatalogPlugin : BasePlugin, IWidgetPlugin
{
    public bool HideInWidgetList => false;

    public Task<IList<string>> GetWidgetZonesAsync()
    {
        return Task.FromResult<IList<string>>(new List<string>
        {
            PublicWidgetZones.HomepageTop
        });
    }

    public Type GetWidgetViewComponent(string widgetZone)
    {
        return typeof(DonationCatalogViewComponent);
    }
}
