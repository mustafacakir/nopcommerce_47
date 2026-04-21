using Nop.Core.Configuration;

namespace Nop.Plugin.Widgets.DonationTransparency;

public class DonationTransparencySettings : ISettings
{
    public string SectionTitle { get; set; } = "Bağışınız Nereye Gidiyor?";
    public string SectionSubtitle { get; set; } = "Her kuruşun hesabını veriyoruz. Yıllık denetim raporlarımız kamuya açıktır.";
    public string ReportLinkText { get; set; } = "Faaliyet Raporunu İndir";
    public string ReportLinkUrl { get; set; } = "";

    public string Item1Label { get; set; } = "Doğrudan Yardım";
    public int Item1Percent { get; set; } = 72;
    public string Item1Color { get; set; } = "#e43d51";
    public string Item1Icon { get; set; } = "🤲";
    public string Item1Description { get; set; } = "Doğrudan ihtiyaç sahiplerine ulaşan yardımlar";

    public string Item2Label { get; set; } = "Proje Giderleri";
    public int Item2Percent { get; set; } = 18;
    public string Item2Color { get; set; } = "#f39c12";
    public string Item2Icon { get; set; } = "🏗️";
    public string Item2Description { get; set; } = "Kuyu, okul, klinik gibi kalıcı yapı projeleri";

    public string Item3Label { get; set; } = "İdari Giderler";
    public int Item3Percent { get; set; } = 7;
    public string Item3Color { get; set; } = "#3498db";
    public string Item3Icon { get; set; } = "🏢";
    public string Item3Description { get; set; } = "Ofis, personel ve operasyonel giderler";

    public string Item4Label { get; set; } = "Bağış Toplama";
    public int Item4Percent { get; set; } = 3;
    public string Item4Color { get; set; } = "#9b59b6";
    public string Item4Icon { get; set; } = "📣";
    public string Item4Description { get; set; } = "Farkındalık ve bağış kampanyaları";

    public string Item5Label { get; set; } = "";
    public int Item5Percent { get; set; } = 0;
    public string Item5Color { get; set; } = "#2ecc71";
    public string Item5Icon { get; set; } = "";
    public string Item5Description { get; set; } = "";
}
