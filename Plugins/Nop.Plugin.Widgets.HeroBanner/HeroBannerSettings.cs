using Nop.Core.Configuration;

namespace Nop.Plugin.Widgets.HeroBanner;

public class HeroBannerSettings : ISettings
{
    public string Title { get; set; } = "Bir Umut Işığı Ol";
    public string Subtitle { get; set; } = "Bağışlarınızla ihtiyaç sahiplerine dokunun. Her katkı bir hayat değiştirir.";
    public string ButtonText { get; set; } = "Hemen Bağış Yap";
    public string ButtonUrl { get; set; } = "#donation-catalog";
    public string BackgroundColor { get; set; } = "#1a1a2e";
    public string AccentColor { get; set; } = "#e43d51";
    public string BackgroundImageUrl { get; set; }
}
