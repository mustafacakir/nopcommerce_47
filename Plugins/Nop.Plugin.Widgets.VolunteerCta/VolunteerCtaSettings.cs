using Nop.Core.Configuration;

namespace Nop.Plugin.Widgets.VolunteerCta;

public class VolunteerCtaSettings : ISettings
{
    public string Title { get; set; } = "Gönüllü Ol!";
    public string Subtitle { get; set; } = "Zamanını ve enerjini iyi bir amaç için kullan. Ekibimize katıl, iyiliğe ortak ol.";
    public string ButtonText { get; set; } = "Gönüllü Başvurusu Yap";
    public string ButtonUrl { get; set; } = "/volunteer";
    public string Stat1Value { get; set; } = "200+";
    public string Stat1Label { get; set; } = "Aktif Gönüllü";
    public string Stat2Value { get; set; } = "12";
    public string Stat2Label { get; set; } = "Ülkede";
    public string Stat3Value { get; set; } = "5.000+";
    public string Stat3Label { get; set; } = "Saat Gönüllülük";
}
