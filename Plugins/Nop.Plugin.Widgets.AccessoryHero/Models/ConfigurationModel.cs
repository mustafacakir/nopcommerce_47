using Nop.Web.Framework.Models;

namespace Nop.Plugin.Widgets.AccessoryHero.Models;

public record ConfigurationModel : BaseNopModel
{
    public int ActiveStoreScopeConfiguration { get; set; }

    public string BadgeText { get; set; }
    public bool BadgeText_OverrideForStore { get; set; }

    public string TitleLine1 { get; set; }
    public bool TitleLine1_OverrideForStore { get; set; }

    public string TitleAccent { get; set; }
    public bool TitleAccent_OverrideForStore { get; set; }

    public string TitleLine2 { get; set; }
    public bool TitleLine2_OverrideForStore { get; set; }

    public string Description { get; set; }
    public bool Description_OverrideForStore { get; set; }

    public string Button1Text { get; set; }
    public bool Button1Text_OverrideForStore { get; set; }

    public string Button1Url { get; set; }
    public bool Button1Url_OverrideForStore { get; set; }

    public string Button2Text { get; set; }
    public bool Button2Text_OverrideForStore { get; set; }

    public string Button2Url { get; set; }
    public bool Button2Url_OverrideForStore { get; set; }

    public string Image1Url { get; set; }
    public bool Image1Url_OverrideForStore { get; set; }

    public string Image2Url { get; set; }
    public bool Image2Url_OverrideForStore { get; set; }

    public string Image3Url { get; set; }
    public bool Image3Url_OverrideForStore { get; set; }

    public string Image4Url { get; set; }
    public bool Image4Url_OverrideForStore { get; set; }
}
