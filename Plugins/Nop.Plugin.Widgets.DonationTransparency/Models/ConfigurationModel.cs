using Nop.Web.Framework.Models;

namespace Nop.Plugin.Widgets.DonationTransparency.Models;

public record ConfigurationModel : BaseNopModel
{
    public int ActiveStoreScopeConfiguration { get; init; }
    public string SectionTitle { get; set; } = string.Empty;
    public string SectionSubtitle { get; set; } = string.Empty;
    public string ReportLinkText { get; set; } = string.Empty;
    public string ReportLinkUrl { get; set; } = string.Empty;
    public string Item1Label { get; set; } = string.Empty;
    public int Item1Percent { get; set; }
    public string Item1Color { get; set; } = string.Empty;
    public string Item1Icon { get; set; } = string.Empty;
    public string Item1Description { get; set; } = string.Empty;
    public string Item2Label { get; set; } = string.Empty;
    public int Item2Percent { get; set; }
    public string Item2Color { get; set; } = string.Empty;
    public string Item2Icon { get; set; } = string.Empty;
    public string Item2Description { get; set; } = string.Empty;
    public string Item3Label { get; set; } = string.Empty;
    public int Item3Percent { get; set; }
    public string Item3Color { get; set; } = string.Empty;
    public string Item3Icon { get; set; } = string.Empty;
    public string Item3Description { get; set; } = string.Empty;
    public string Item4Label { get; set; } = string.Empty;
    public int Item4Percent { get; set; }
    public string Item4Color { get; set; } = string.Empty;
    public string Item4Icon { get; set; } = string.Empty;
    public string Item4Description { get; set; } = string.Empty;
    public string Item5Label { get; set; } = string.Empty;
    public int Item5Percent { get; set; }
    public string Item5Color { get; set; } = string.Empty;
    public string Item5Icon { get; set; } = string.Empty;
    public string Item5Description { get; set; } = string.Empty;
}
