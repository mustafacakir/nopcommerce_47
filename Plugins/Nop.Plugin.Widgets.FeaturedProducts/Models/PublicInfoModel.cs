namespace Nop.Plugin.Widgets.FeaturedProducts.Models;

public class PublicInfoModel
{
    public string SectionBadge { get; set; }
    public string SectionTitle { get; set; }
    public string SectionSubtitle { get; set; }
    public string ViewAllText { get; set; }
    public string ViewAllUrl { get; set; }
    public IList<ProductCardModel> Products { get; set; } = new List<ProductCardModel>();
}
