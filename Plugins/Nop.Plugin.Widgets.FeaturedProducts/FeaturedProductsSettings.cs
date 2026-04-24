using Nop.Core.Configuration;

namespace Nop.Plugin.Widgets.FeaturedProducts;

public class FeaturedProductsSettings : ISettings
{
    public string SectionBadge { get; set; } = "Öne Çıkanlar";
    public string SectionTitle { get; set; } = "Öne Çıkan Ürünler";
    public string SectionSubtitle { get; set; } = "En beğenilen ürünlerimizi keşfedin";
    public string ViewAllText { get; set; } = "Tüm Ürünleri Gör";
    public string ViewAllUrl { get; set; } = "/catalog";
    public int ProductCount { get; set; } = 8;
}
