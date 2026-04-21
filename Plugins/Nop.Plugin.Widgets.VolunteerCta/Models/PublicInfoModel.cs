namespace Nop.Plugin.Widgets.VolunteerCta.Models;

public record PublicInfoModel
{
    public string Title { get; init; } = string.Empty;
    public string Subtitle { get; init; } = string.Empty;
    public string ButtonText { get; init; } = string.Empty;
    public string ButtonUrl { get; init; } = string.Empty;
    public string Stat1Value { get; init; } = string.Empty;
    public string Stat1Label { get; init; } = string.Empty;
    public string Stat2Value { get; init; } = string.Empty;
    public string Stat2Label { get; init; } = string.Empty;
    public string Stat3Value { get; init; } = string.Empty;
    public string Stat3Label { get; init; } = string.Empty;
}
