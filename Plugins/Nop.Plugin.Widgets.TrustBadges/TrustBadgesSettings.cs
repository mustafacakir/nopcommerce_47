using Nop.Core.Configuration;

namespace Nop.Plugin.Widgets.TrustBadges;

public class TrustBadgesSettings : ISettings
{
    public string Badge1Icon { get; set; } = "🏛️";
    public string Badge1Title { get; set; } = "Resmi Dernek";
    public string Badge1Subtitle { get; set; } = "Tescilli & denetlenen";

    public string Badge2Icon { get; set; } = "📋";
    public string Badge2Title { get; set; } = "Vergi Muafiyeti";
    public string Badge2Subtitle { get; set; } = "Bağışlar vergiden düşülür";

    public string Badge3Icon { get; set; } = "🔒";
    public string Badge3Title { get; set; } = "SSL Güvenli";
    public string Badge3Subtitle { get; set; } = "256-bit şifreleme";

    public string Badge4Icon { get; set; } = "💳";
    public string Badge4Title { get; set; } = "Güvenli Ödeme";
    public string Badge4Subtitle { get; set; } = "Banka güvencesiyle";

    public string Badge5Icon { get; set; } = "📊";
    public string Badge5Title { get; set; } = "Şeffaf Raporlama";
    public string Badge5Subtitle { get; set; } = "Yıllık faaliyet raporu";
}
