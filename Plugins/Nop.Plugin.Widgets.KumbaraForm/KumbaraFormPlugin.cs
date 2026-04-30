using Nop.Plugin.Widgets.KumbaraForm.Components;
using Nop.Services.Cms;
using Nop.Services.Plugins;
using Nop.Web.Framework.Infrastructure;
using Nop.Web.Framework.Menu;

namespace Nop.Plugin.Widgets.KumbaraForm;

public class KumbaraFormPlugin : BasePlugin, IWidgetPlugin, IAdminMenuPlugin
{
    public bool HideInWidgetList => false;

    public Task<IList<string>> GetWidgetZonesAsync()
    {
        return Task.FromResult<IList<string>>(new List<string>());
    }

    public Type GetWidgetViewComponent(string widgetZone)
    {
        return typeof(KumbaraFormViewComponent);
    }

    public override string GetConfigurationPageUrl()
    {
        return "/Admin/KumbaraAdmin/Configure";
    }

    public Task ManageSiteMapAsync(SiteMapNode rootNode)
    {
        var pluginNode = rootNode.ChildNodes.FirstOrDefault(n => n.SystemName == "Third party plugins")
            ?? rootNode.ChildNodes.FirstOrDefault();

        var kumbaraNode = new SiteMapNode
        {
            SystemName = "KumbaraForm",
            Title = "Kumbara Başvuruları",
            Url = "/Admin/KumbaraAdmin/Configure",
            Visible = true,
            IconClass = "far fa-dot-circle",
        };

        pluginNode?.ChildNodes.Add(kumbaraNode);

        return Task.CompletedTask;
    }
}
