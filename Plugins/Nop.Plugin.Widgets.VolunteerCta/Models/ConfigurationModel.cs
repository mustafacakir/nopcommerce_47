using Nop.Web.Framework.Models;

namespace Nop.Plugin.Widgets.VolunteerCta.Models;

public record ConfigurationModel : BaseNopModel
{
    public int ActiveStoreScopeConfiguration { get; init; }
    public string Title { get; set; } = string.Empty;
    public string Subtitle { get; set; } = string.Empty;
    public string ButtonText { get; set; } = string.Empty;
    public string ButtonUrl { get; set; } = string.Empty;
    public string Stat1Value { get; set; } = string.Empty;
    public string Stat1Label { get; set; } = string.Empty;
    public string Stat2Value { get; set; } = string.Empty;
    public string Stat2Label { get; set; } = string.Empty;
    public string Stat3Value { get; set; } = string.Empty;
    public string Stat3Label { get; set; } = string.Empty;
}
