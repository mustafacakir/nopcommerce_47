using Nop.Core.Configuration;

namespace Nop.Plugin.Widgets.RecentDonations;

public class RecentDonationsSettings : ISettings
{
    public int ItemCount { get; set; } = 10;
    public bool ShowAmount { get; set; } = true;
    public bool ShowProductName { get; set; } = true;
    public string FallbackText { get; set; } = "İlk bağışçı sen ol!";
}
