using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Nop.Web.Framework.Mvc.Routing;

namespace Nop.Plugin.Widgets.CampaignProgress.Infrastructure;

public class RouteProvider : IRouteProvider
{
    public void RegisterRoutes(IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder.MapControllerRoute(
            "CampaignList",
            "bagis/proje",
            new { controller = "CampaignPublic", action = "List" });

        endpointRouteBuilder.MapControllerRoute(
            "CampaignDetail",
            "proje/{slug}",
            new { controller = "CampaignPublic", action = "Detail" });
    }

    public int Priority => 0;
}
