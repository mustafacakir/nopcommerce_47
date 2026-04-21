namespace Nop.Plugin.Widgets.TrustBadges.Models;

public record PublicInfoModel
{
    public List<BadgeItem> Badges { get; init; } = new();
}

public record BadgeItem
{
    public string Icon { get; init; } = string.Empty;
    public string Title { get; init; } = string.Empty;
    public string Subtitle { get; init; } = string.Empty;
}
