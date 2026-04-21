using Nop.Web.Framework.Models;

namespace Nop.Plugin.Widgets.TrustBadges.Models;

public record ConfigurationModel : BaseNopModel
{
    public int ActiveStoreScopeConfiguration { get; init; }
    public string Badge1Icon { get; set; } = string.Empty;
    public string Badge1Title { get; set; } = string.Empty;
    public string Badge1Subtitle { get; set; } = string.Empty;
    public string Badge2Icon { get; set; } = string.Empty;
    public string Badge2Title { get; set; } = string.Empty;
    public string Badge2Subtitle { get; set; } = string.Empty;
    public string Badge3Icon { get; set; } = string.Empty;
    public string Badge3Title { get; set; } = string.Empty;
    public string Badge3Subtitle { get; set; } = string.Empty;
    public string Badge4Icon { get; set; } = string.Empty;
    public string Badge4Title { get; set; } = string.Empty;
    public string Badge4Subtitle { get; set; } = string.Empty;
    public string Badge5Icon { get; set; } = string.Empty;
    public string Badge5Title { get; set; } = string.Empty;
    public string Badge5Subtitle { get; set; } = string.Empty;
}
