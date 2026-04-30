using Microsoft.AspNetCore.Mvc;
using Nop.Plugin.Widgets.KumbaraForm.Models;
using Nop.Web.Framework.Components;

namespace Nop.Plugin.Widgets.KumbaraForm.Components;

[ViewComponent(Name = "KumbaraForm")]
public class KumbaraFormViewComponent : NopViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View("~/Plugins/Widgets.KumbaraForm/Views/Components/KumbaraForm/Default.cshtml", new KumbaraFormModel());
    }
}
