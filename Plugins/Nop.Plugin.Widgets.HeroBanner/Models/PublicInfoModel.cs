using Nop.Web.Framework.Models;

namespace Nop.Plugin.Widgets.HeroBanner.Models;

public record PublicInfoModel : BaseNopModel
{
    public string Title { get; set; }
    public string Subtitle { get; set; }
    public string ButtonText { get; set; }
    public string ButtonUrl { get; set; }
    public string BackgroundColor { get; set; }
    public string AccentColor { get; set; }
    public string BackgroundImageUrl { get; set; }
}
