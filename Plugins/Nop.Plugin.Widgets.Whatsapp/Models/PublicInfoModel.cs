using Nop.Web.Framework.Models;

namespace Nop.Plugin.Widgets.Whatsapp.Models;

public record PublicInfoModel : BaseNopModel
{
    public string PhoneNumber { get; set; }
    public int IconWidthAndHeight { get; set; }
}
