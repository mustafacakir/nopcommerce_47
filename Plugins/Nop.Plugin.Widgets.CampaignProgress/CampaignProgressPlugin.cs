using Microsoft.AspNetCore.Routing;
using Nop.Plugin.Widgets.CampaignProgress.Components;
using Nop.Services.Cms;
using Nop.Services.Plugins;
using Nop.Services.Security;
using Nop.Web.Framework;
using Nop.Web.Framework.Infrastructure;
using Nop.Web.Framework.Menu;

namespace Nop.Plugin.Widgets.CampaignProgress;

public class CampaignProgressPlugin : BasePlugin, IWidgetPlugin, IAdminMenuPlugin
{
    private readonly IPermissionService _permissionService;

    public CampaignProgressPlugin(IPermissionService permissionService)
    {
        _permissionService = permissionService;
    }

    public bool HideInWidgetList => false;
    public Type GetWidgetViewComponent(string widgetZone) => typeof(CampaignProgressViewComponent);

    public Task<IList<string>> GetWidgetZonesAsync()
    {
        return Task.FromResult<IList<string>>(new List<string> { PublicWidgetZones.HomepageTop });
    }

    public override string GetConfigurationPageUrl() => "/Admin/CampaignAdmin/List";

    public async Task ManageSiteMapAsync(SiteMapNode rootNode)
    {
        if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageWidgets))
            return;

        var parent = rootNode.ChildNodes.FirstOrDefault(x => x.SystemName == "Content management")
                     ?? rootNode;

        parent.ChildNodes.Add(new SiteMapNode
        {
            SystemName = "Widgets.CampaignProgress.List",
            Title = "Kampanya Projeleri",
            ControllerName = "CampaignAdmin",
            ActionName = "List",
            IconClass = "far fa-dot-circle",
            Visible = true,
            RouteValues = new RouteValueDictionary { ["area"] = AreaNames.ADMIN }
        });
    }

    public override async Task InstallAsync()
    {
        await base.InstallAsync();
    }

    public override async Task UninstallAsync()
    {
        await base.UninstallAsync();
    }
}
