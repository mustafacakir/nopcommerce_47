using Nop.Core.Configuration;

namespace Nop.Plugin.Widgets.CategoryShowcase;

public class CategoryShowcaseSettings : ISettings
{
    public string SectionBadge { get; set; } = "Kategoriler";
    public string SectionTitle { get; set; } = "Koleksiyonlarımızı Keşfedin";
    public string SectionSubtitle { get; set; } = "Her zevke ve tarza uygun geniş ürün yelpazemizle tanışın";

    public string Card1Title { get; set; } = "Anahtarlıklar";
    public string Card1Description { get; set; } = "Deri, metal ve ahşap el yapımı anahtarlıklar";
    public string Card1Badge { get; set; } = "85+ Ürün";
    public string Card1ImageUrl { get; set; } = "";
    public string Card1Url { get; set; } = "/catalog";

    public string Card2Title { get; set; } = "Bileklikler";
    public string Card2Description { get; set; } = "Boncuk, deri ve metal tasarım bileklikler";
    public string Card2Badge { get; set; } = "150+ Ürün";
    public string Card2ImageUrl { get; set; } = "";
    public string Card2Url { get; set; } = "/catalog";
}
