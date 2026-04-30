using Microsoft.AspNetCore.Routing;
using Nop.Plugin.Widgets.HeroSlider.Components;
using Nop.Services.Cms;
using Nop.Services.Plugins;
using Nop.Services.Security;
using Nop.Web.Framework;
using Nop.Web.Framework.Infrastructure;
using Nop.Web.Framework.Menu;

namespace Nop.Plugin.Widgets.HeroSlider;

public class HeroSliderPlugin : BasePlugin, IWidgetPlugin, IAdminMenuPlugin
{
    private readonly IPermissionService _permissionService;

    public HeroSliderPlugin(IPermissionService permissionService)
        => _permissionService = permissionService;

    public bool HideInWidgetList => false;
    public Type GetWidgetViewComponent(string widgetZone) => typeof(HeroSliderViewComponent);

    public Task<IList<string>> GetWidgetZonesAsync() =>
        Task.FromResult<IList<string>>(new List<string> { PublicWidgetZones.HomepageTop });

    public override string GetConfigurationPageUrl() => "/Admin/HeroSliderAdmin/List";

    public async Task ManageSiteMapAsync(SiteMapNode rootNode)
    {
        if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageWidgets))
            return;

        var parent = rootNode.ChildNodes.FirstOrDefault(x => x.SystemName == "Content management") ?? rootNode;
        parent.ChildNodes.Add(new SiteMapNode
        {
            SystemName = "Widgets.HeroSlider.List",
            Title = "Hero Slider",
            ControllerName = "HeroSliderAdmin",
            ActionName = "List",
            IconClass = "far fa-images",
            Visible = true,
            RouteValues = new RouteValueDictionary { ["area"] = AreaNames.ADMIN }
        });
    }

    public override async Task InstallAsync() => await base.InstallAsync();
    public override async Task UninstallAsync() => await base.UninstallAsync();
}
