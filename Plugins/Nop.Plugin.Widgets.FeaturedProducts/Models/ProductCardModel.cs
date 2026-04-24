namespace Nop.Plugin.Widgets.FeaturedProducts.Models;

public class ProductCardModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string SeName { get; set; }
    public string ImageUrl { get; set; }
    public string CategoryName { get; set; }
    public string Price { get; set; }
    public string OldPrice { get; set; }
    public bool HasDiscount { get; set; }
    public int ReviewCount { get; set; }
    public double Rating { get; set; }
}
