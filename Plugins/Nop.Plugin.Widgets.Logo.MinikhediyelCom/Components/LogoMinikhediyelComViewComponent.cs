using Microsoft.AspNetCore.Mvc;
using Nop.Web.Framework.Components;

namespace Nop.Plugin.Widgets.Logo.MinikhediyelCom.Components;

public class LogoMinikhediyelComViewComponent : NopViewComponent
{
    public IViewComponentResult Invoke(string widgetZone, object additionalData)
    {
        return View("~/Plugins/Widgets.Logo.MinikhediyelCom/Views/PublicInfo.cshtml");
    }
}
