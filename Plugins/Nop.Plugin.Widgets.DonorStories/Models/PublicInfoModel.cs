namespace Nop.Plugin.Widgets.DonorStories.Models;

public record PublicInfoModel
{
    public string SectionTitle { get; init; } = string.Empty;
    public List<StoryItem> Stories { get; init; } = new();
}

public record StoryItem
{
    public string Name { get; init; } = string.Empty;
    public string Location { get; init; } = string.Empty;
    public string Quote { get; init; } = string.Empty;
    public string Avatar { get; init; } = string.Empty;
    public string Initials { get; init; } = string.Empty;
}
