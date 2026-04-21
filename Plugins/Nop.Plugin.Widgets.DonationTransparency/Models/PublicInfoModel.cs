namespace Nop.Plugin.Widgets.DonationTransparency.Models;

public record PublicInfoModel
{
    public string SectionTitle { get; init; } = string.Empty;
    public string SectionSubtitle { get; init; } = string.Empty;
    public string ReportLinkText { get; init; } = string.Empty;
    public string ReportLinkUrl { get; init; } = string.Empty;
    public List<TransparencyItem> Items { get; init; } = new();
}

public record TransparencyItem
{
    public string Label { get; init; } = string.Empty;
    public int Percent { get; init; }
    public string Color { get; init; } = string.Empty;
    public string Icon { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
}
