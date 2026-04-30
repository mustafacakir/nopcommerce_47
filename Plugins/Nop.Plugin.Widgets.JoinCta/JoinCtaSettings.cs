using Nop.Core.Configuration;

namespace Nop.Plugin.Widgets.JoinCta;

public class JoinCtaSettings : ISettings
{
    public string Title               { get; set; } = "Siz de Bu Yolculukta Yerinizi Alın";
    public string BankSectionTitle    { get; set; } = "Havale veya EFT ile Bağış";

    public string Bank1Currency       { get; set; } = "TL";
    public string Bank1Name           { get; set; } = "";
    public string Bank1Iban           { get; set; } = "";

    public string Bank2Currency       { get; set; } = "USD";
    public string Bank2Name           { get; set; } = "";
    public string Bank2Iban           { get; set; } = "";

    public string Bank3Currency       { get; set; } = "EUR";
    public string Bank3Name           { get; set; } = "";
    public string Bank3Iban           { get; set; } = "";

    public string DonateTitle         { get; set; } = "Bağış Yap";
    public string DonateDescription   { get; set; } = "İhtiyaç sahiplerine umut olun, projelere destek verin.";
    public string DonateLinkText      { get; set; } = "Daha Fazla";
    public string DonateUrl           { get; set; } = "/donation";

    public string ContactTitle        { get; set; } = "İletişime Geç";
    public string ContactDescription  { get; set; } = "Sorularınız veya işbirlikleri için bize ulaşın.";
    public string ContactLinkText     { get; set; } = "Daha Fazla";
}
