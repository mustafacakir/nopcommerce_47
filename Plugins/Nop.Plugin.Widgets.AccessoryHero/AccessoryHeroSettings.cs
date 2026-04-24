using Nop.Core.Configuration;

namespace Nop.Plugin.Widgets.AccessoryHero;

public class AccessoryHeroSettings : ISettings
{
    public string BadgeText { get; set; } = "El Yapımı & Özel Tasarım";
    public string TitleLine1 { get; set; } = "Tarzınızı Yansıtan";
    public string TitleAccent { get; set; } = "Benzersiz";
    public string TitleLine2 { get; set; } = "Aksesuarlar";
    public string Description { get; set; } = "Her biri özenle tasarlanmış anahtarlıklar ve bileklikler kendinizi ifade edin. Kaliteli malzemeler, özgün tasarımlar.";
    public string Button1Text { get; set; } = "Ürünleri Keşfet";
    public string Button1Url { get; set; } = "/catalog";
    public string Button2Text { get; set; } = "Koleksiyonu Gör";
    public string Button2Url { get; set; } = "/catalog";
    public string Image1Url { get; set; } = "";
    public string Image2Url { get; set; } = "";
    public string Image3Url { get; set; } = "";
    public string Image4Url { get; set; } = "";
}
