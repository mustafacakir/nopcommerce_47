using Nop.Web.Framework.Models;

namespace Nop.Plugin.Widgets.RecentDonations.Models;

public record ConfigurationModel : BaseNopModel
{
    public int ActiveStoreScopeConfiguration { get; init; }
    public int ItemCount { get; set; }
    public bool ShowAmount { get; set; }
    public bool ShowProductName { get; set; }
    public string FallbackText { get; set; } = string.Empty;
}
