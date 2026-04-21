namespace Nop.Plugin.Widgets.ProjectStories.Models;

public record PublicInfoModel
{
    public string SectionTitle { get; init; } = string.Empty;
    public string SectionSubtitle { get; init; } = string.Empty;
    public List<StoryCard> Stories { get; init; } = new();
}

public record StoryCard
{
    public string Title { get; init; } = string.Empty;
    public string Tag { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public string ImageUrl { get; init; } = string.Empty;
    public string VideoUrl { get; init; } = string.Empty;
    public string LinkUrl { get; init; } = string.Empty;
    public bool HasVideo => !string.IsNullOrEmpty(VideoUrl);
    public bool HasImage => !string.IsNullOrEmpty(ImageUrl);
}
