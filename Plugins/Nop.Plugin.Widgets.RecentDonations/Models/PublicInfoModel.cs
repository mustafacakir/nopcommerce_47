namespace Nop.Plugin.Widgets.RecentDonations.Models;

public record PublicInfoModel
{
    public List<DonationItem> Items { get; init; } = new();
    public string FallbackText { get; init; } = string.Empty;
}

public record DonationItem
{
    public string DonorName { get; init; } = string.Empty;
    public string ProductName { get; init; } = string.Empty;
    public string Amount { get; init; } = string.Empty;
    public string TimeAgo { get; init; } = string.Empty;
}
