using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Plugin.Widgets.MarqueeBanner.Models;

public record ConfigurationModel : BaseNopModel
{
    [NopResourceDisplayName("Plugins.Widgets.MarqueeBanner.Text")]
    public string Text { get; set; }
    public bool Text_OverrideForStore { get; set; }

    [NopResourceDisplayName("Plugins.Widgets.MarqueeBanner.BackgroundColor")]
    public string BackgroundColor { get; set; }
    public bool BackgroundColor_OverrideForStore { get; set; }

    [NopResourceDisplayName("Plugins.Widgets.MarqueeBanner.TextColor")]
    public string TextColor { get; set; }
    public bool TextColor_OverrideForStore { get; set; }

    [NopResourceDisplayName("Plugins.Widgets.MarqueeBanner.Speed")]
    public int Speed { get; set; }
    public bool Speed_OverrideForStore { get; set; }

    public int ActiveStoreScopeConfiguration { get; set; }
}
