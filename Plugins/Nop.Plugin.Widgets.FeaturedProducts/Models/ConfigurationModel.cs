using Nop.Web.Framework.Models;

namespace Nop.Plugin.Widgets.FeaturedProducts.Models;

public record ConfigurationModel : BaseNopModel
{
    public int ActiveStoreScopeConfiguration { get; set; }

    public string SectionBadge { get; set; }
    public bool SectionBadge_OverrideForStore { get; set; }

    public string SectionTitle { get; set; }
    public bool SectionTitle_OverrideForStore { get; set; }

    public string SectionSubtitle { get; set; }
    public bool SectionSubtitle_OverrideForStore { get; set; }

    public string ViewAllText { get; set; }
    public bool ViewAllText_OverrideForStore { get; set; }

    public string ViewAllUrl { get; set; }
    public bool ViewAllUrl_OverrideForStore { get; set; }

    public int ProductCount { get; set; }
    public bool ProductCount_OverrideForStore { get; set; }
}
