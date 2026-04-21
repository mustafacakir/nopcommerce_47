using Nop.Web.Framework.Models;

namespace Nop.Plugin.Widgets.DonorStories.Models;

public record ConfigurationModel : BaseNopModel
{
    public int ActiveStoreScopeConfiguration { get; init; }
    public string SectionTitle { get; set; } = string.Empty;
    public string Story1Name { get; set; } = string.Empty;
    public string Story1Location { get; set; } = string.Empty;
    public string Story1Quote { get; set; } = string.Empty;
    public string Story1Avatar { get; set; } = string.Empty;
    public string Story2Name { get; set; } = string.Empty;
    public string Story2Location { get; set; } = string.Empty;
    public string Story2Quote { get; set; } = string.Empty;
    public string Story2Avatar { get; set; } = string.Empty;
    public string Story3Name { get; set; } = string.Empty;
    public string Story3Location { get; set; } = string.Empty;
    public string Story3Quote { get; set; } = string.Empty;
    public string Story3Avatar { get; set; } = string.Empty;
    public string Story4Name { get; set; } = string.Empty;
    public string Story4Location { get; set; } = string.Empty;
    public string Story4Quote { get; set; } = string.Empty;
    public string Story4Avatar { get; set; } = string.Empty;
}
