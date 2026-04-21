using Nop.Core.Configuration;

namespace Nop.Plugin.Widgets.ImpactCounter;

public class ImpactCounterSettings : ISettings
{
    public string Stat1Icon { get; set; } = "🤲";
    public string Stat1Value { get; set; } = "12.500+";
    public string Stat1Label { get; set; } = "Bağış Yapıldı";

    public string Stat2Icon { get; set; } = "🌍";
    public string Stat2Value { get; set; } = "48";
    public string Stat2Label { get; set; } = "Tamamlanan Proje";

    public string Stat3Icon { get; set; } = "👨‍👩‍👧‍👦";
    public string Stat3Value { get; set; } = "5.200+";
    public string Stat3Label { get; set; } = "İhtiyaç Sahibi";

    public string Stat4Icon { get; set; } = "🕌";
    public string Stat4Value { get; set; } = "12";
    public string Stat4Label { get; set; } = "Ülkede Faaliyet";
}
