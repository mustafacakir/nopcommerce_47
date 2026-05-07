using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Nop.Web.Framework.Mvc.Routing;

namespace Nop.Plugin.Misc.SimpleCheckout.Infrastructure;

public class RouteProvider : IRouteProvider
{
    public void RegisterRoutes(IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder.MapControllerRoute(
            "SimpleCheckout",
            "checkout/bagis",
            new { controller = "SimpleCheckout", action = "Index" });

        endpointRouteBuilder.MapControllerRoute(
            "SimpleCheckoutAdmin",
            "Admin/SimpleCheckoutAdmin/Configure",
            new { controller = "SimpleCheckoutAdmin", action = "Configure", area = "Admin" });
    }

    public int Priority => 100;
}
