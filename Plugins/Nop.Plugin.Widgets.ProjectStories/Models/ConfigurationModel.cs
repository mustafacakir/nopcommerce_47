using Nop.Web.Framework.Models;

namespace Nop.Plugin.Widgets.ProjectStories.Models;

public record ConfigurationModel : BaseNopModel
{
    public int ActiveStoreScopeConfiguration { get; init; }
    public string SectionTitle { get; set; } = string.Empty;
    public string SectionSubtitle { get; set; } = string.Empty;
    public string Story1Title { get; set; } = string.Empty;
    public string Story1Tag { get; set; } = string.Empty;
    public string Story1Description { get; set; } = string.Empty;
    public string Story1ImageUrl { get; set; } = string.Empty;
    public string Story1VideoUrl { get; set; } = string.Empty;
    public string Story1LinkUrl { get; set; } = string.Empty;
    public string Story2Title { get; set; } = string.Empty;
    public string Story2Tag { get; set; } = string.Empty;
    public string Story2Description { get; set; } = string.Empty;
    public string Story2ImageUrl { get; set; } = string.Empty;
    public string Story2VideoUrl { get; set; } = string.Empty;
    public string Story2LinkUrl { get; set; } = string.Empty;
    public string Story3Title { get; set; } = string.Empty;
    public string Story3Tag { get; set; } = string.Empty;
    public string Story3Description { get; set; } = string.Empty;
    public string Story3ImageUrl { get; set; } = string.Empty;
    public string Story3VideoUrl { get; set; } = string.Empty;
    public string Story3LinkUrl { get; set; } = string.Empty;
    public string Story4Title { get; set; } = string.Empty;
    public string Story4Tag { get; set; } = string.Empty;
    public string Story4Description { get; set; } = string.Empty;
    public string Story4ImageUrl { get; set; } = string.Empty;
    public string Story4VideoUrl { get; set; } = string.Empty;
    public string Story4LinkUrl { get; set; } = string.Empty;
}
