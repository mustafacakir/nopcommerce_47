using Nop.Web.Framework.Models;

namespace Nop.Plugin.Widgets.CategoryShowcase.Models;

public record ConfigurationModel : BaseNopModel
{
    public int ActiveStoreScopeConfiguration { get; set; }

    public string SectionBadge { get; set; }
    public bool SectionBadge_OverrideForStore { get; set; }

    public string SectionTitle { get; set; }
    public bool SectionTitle_OverrideForStore { get; set; }

    public string SectionSubtitle { get; set; }
    public bool SectionSubtitle_OverrideForStore { get; set; }

    public string Card1Title { get; set; }
    public bool Card1Title_OverrideForStore { get; set; }

    public string Card1Description { get; set; }
    public bool Card1Description_OverrideForStore { get; set; }

    public string Card1Badge { get; set; }
    public bool Card1Badge_OverrideForStore { get; set; }

    public string Card1ImageUrl { get; set; }
    public bool Card1ImageUrl_OverrideForStore { get; set; }

    public string Card1Url { get; set; }
    public bool Card1Url_OverrideForStore { get; set; }

    public string Card2Title { get; set; }
    public bool Card2Title_OverrideForStore { get; set; }

    public string Card2Description { get; set; }
    public bool Card2Description_OverrideForStore { get; set; }

    public string Card2Badge { get; set; }
    public bool Card2Badge_OverrideForStore { get; set; }

    public string Card2ImageUrl { get; set; }
    public bool Card2ImageUrl_OverrideForStore { get; set; }

    public string Card2Url { get; set; }
    public bool Card2Url_OverrideForStore { get; set; }
}
