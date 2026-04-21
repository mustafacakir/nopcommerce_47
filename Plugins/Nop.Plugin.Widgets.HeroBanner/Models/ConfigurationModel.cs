using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Plugin.Widgets.HeroBanner.Models;

public record ConfigurationModel : BaseNopModel
{
    [NopResourceDisplayName("Plugins.Widgets.HeroBanner.Title")]
    public string Title { get; set; }
    public bool Title_OverrideForStore { get; set; }

    [NopResourceDisplayName("Plugins.Widgets.HeroBanner.Subtitle")]
    public string Subtitle { get; set; }
    public bool Subtitle_OverrideForStore { get; set; }

    [NopResourceDisplayName("Plugins.Widgets.HeroBanner.ButtonText")]
    public string ButtonText { get; set; }
    public bool ButtonText_OverrideForStore { get; set; }

    [NopResourceDisplayName("Plugins.Widgets.HeroBanner.ButtonUrl")]
    public string ButtonUrl { get; set; }
    public bool ButtonUrl_OverrideForStore { get; set; }

    [NopResourceDisplayName("Plugins.Widgets.HeroBanner.BackgroundColor")]
    public string BackgroundColor { get; set; }
    public bool BackgroundColor_OverrideForStore { get; set; }

    [NopResourceDisplayName("Plugins.Widgets.HeroBanner.AccentColor")]
    public string AccentColor { get; set; }
    public bool AccentColor_OverrideForStore { get; set; }

    [NopResourceDisplayName("Plugins.Widgets.HeroBanner.BackgroundImageUrl")]
    public string BackgroundImageUrl { get; set; }
    public bool BackgroundImageUrl_OverrideForStore { get; set; }

    public int ActiveStoreScopeConfiguration { get; set; }
}
