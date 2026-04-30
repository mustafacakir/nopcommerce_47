namespace Nop.Plugin.Widgets.ChildDonation.Models;

public record CategoryItemModel
{
    public string Name { get; set; }
    public string IconUrl { get; set; }
    public string OverlayUrl { get; set; }
    public decimal Price { get; set; }
    public int ProductId { get; set; }
}

public record ChildDonationPublicModel
{
    public string ChildName { get; set; }
    public string ChildImageUrl { get; set; }
    public string Title { get; set; }
    public string Subtitle { get; set; }
    public List<CategoryItemModel> Categories { get; set; } = new();
}
