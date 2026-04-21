using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace Nop.Plugin.Widgets.Whatsapp.Models;

public record ConfigurationModel : BaseNopModel
{
    [NopResourceDisplayName("Plugins.Widgets.Whatsapp.PhoneNumber")]
    public string PhoneNumber { get; set; }
    public bool PhoneNumber_OverrideForStore { get; set; }

    [NopResourceDisplayName("Plugins.Widgets.Whatsapp.IconWidthAndHeight")]
    public int IconWidthAndHeight { get; set; }
    public bool IconWidthAndHeight_OverrideForStore { get; set; }

    public int ActiveStoreScopeConfiguration { get; set; }
}
