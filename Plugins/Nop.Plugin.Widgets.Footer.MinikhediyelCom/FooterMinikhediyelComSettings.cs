using Nop.Core.Configuration;

namespace Nop.Plugin.Widgets.Footer.MinikhediyelCom;

/// <summary>
/// minikhediyen.com — footer widget ayarları
/// </summary>
public class FooterMinikhediyelComSettings : ISettings
{
    // Marka
    public string StoreName { get; set; } = "Aksesuar Dünyası";
    public string LogoLetter { get; set; } = "A";
    public string Tagline { get; set; } = "El yapımı, özel tasarım anahtarlıklar ve bilekliklerle tarzınızı yansıtın.";
    public string InstagramUrl { get; set; } = "#";
    public string FacebookUrl { get; set; } = "#";
    public string TwitterUrl { get; set; } = "#";

    // Hızlı Linkler
    public string QuickLinksTitle { get; set; } = "Hızlı Linkler";
    public string QuickLink1Text { get; set; } = "Ana Sayfa";
    public string QuickLink1Url { get; set; } = "/";
    public string QuickLink2Text { get; set; } = "Ürünler";
    public string QuickLink2Url { get; set; } = "/catalog";
    public string QuickLink3Text { get; set; } = "Kategoriler";
    public string QuickLink3Url { get; set; } = "/catalog";
    public string QuickLink4Text { get; set; } = "Hakkımızda";
    public string QuickLink4Url { get; set; } = "/about-us";

    // Destek
    public string SupportTitle { get; set; } = "Destek";
    public string SupportLink1Text { get; set; } = "Sıkça Sorulanlar";
    public string SupportLink1Url { get; set; } = "/faqs";
    public string SupportLink2Text { get; set; } = "Kargo Bilgileri";
    public string SupportLink2Url { get; set; } = "/shipping-info";
    public string SupportLink3Text { get; set; } = "İade Politikası";
    public string SupportLink3Url { get; set; } = "/return-policy";
    public string SupportLink4Text { get; set; } = "Gizlilik Sözleşmesi";
    public string SupportLink4Url { get; set; } = "/privacy-policy";

    // İletişim
    public string ContactTitle { get; set; } = "İletişim";
    public string Address { get; set; } = "İstanbul, Türkiye";
    public string Phone { get; set; } = "+90 555 123 4567";
    public string Email { get; set; } = "info@minikhediyen.com";

    // Alt çubuk
    public string CopyrightText { get; set; } = "© 2024 Aksesuar Dünyası. Tüm hakları saklıdır.";
}
