using Nop.Web.Framework.Models;

namespace Nop.Plugin.Widgets.MarqueeBanner.Models;

public record PublicInfoModel : BaseNopModel
{
    public string Text { get; set; }
    public string Link { get; set; }
    public string BackgroundColor { get; set; }
    public string TextColor { get; set; }
    public int Speed { get; set; }
}
