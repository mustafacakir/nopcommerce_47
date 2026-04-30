using Nop.Core.Configuration;

namespace Nop.Plugin.Widgets.Footer.BuharaMedresesi;

public class FooterBuharaMedresesiSettings : ISettings
{
    // CTA Banner
    public string CtaTitle      { get; set; } = "Birlikte Daha Güçlüyüz";
    public string CtaSubtitle   { get; set; } = "Siz de ihtiyaç sahiplerine umut olun, hayra vesile olun.";
    public string CtaButtonText { get; set; } = "Bağış Yap";
    public string CtaButtonUrl  { get; set; } = "/bagis";

    // Marka
    public string LogoUrl     { get; set; } = "";
    public string Description { get; set; } = "Yüzlerde Tebessüm, Gönüllerde Fetih. Dünyanın dört bir yanında ihtiyaç sahiplerine umut taşıyan insani yardım derneğiyiz.";

    // Sosyal medya
    public string FacebookUrl  { get; set; } = "#";
    public string InstagramUrl { get; set; } = "#";
    public string TwitterUrl   { get; set; } = "#";
    public string YoutubeUrl   { get; set; } = "#";
    public string LinkedinUrl  { get; set; } = "#";
    public string TiktokUrl    { get; set; } = "#";
    public string WhatsappUrl  { get; set; } = "#";

    // Hızlı Linkler
    public string QuickLinksTitle  { get; set; } = "Hızlı Linkler";
    public string QuickLink1Text   { get; set; } = "Hakkımızda";   public string QuickLink1Url { get; set; } = "/hakkimizda";
    public string QuickLink2Text   { get; set; } = "Projeler";     public string QuickLink2Url { get; set; } = "/projeler";
    public string QuickLink3Text   { get; set; } = "Faaliyet";     public string QuickLink3Url { get; set; } = "/faaliyet";
    public string QuickLink4Text   { get; set; } = "Blog";         public string QuickLink4Url { get; set; } = "/blog";
    public string QuickLink5Text   { get; set; } = "İletişim";     public string QuickLink5Url { get; set; } = "/iletisim";
    public string QuickLink6Text   { get; set; } = "Banka Hesapları"; public string QuickLink6Url { get; set; } = "/banka-hesaplari";

    // Proje Kategorileri
    public string CategoryLinksTitle { get; set; } = "Proje Kategorileri";
    public string CategoryLink1Text  { get; set; } = "Su ve Gıda"; public string CategoryLink1Url { get; set; } = "#";
    public string CategoryLink2Text  { get; set; } = "Eğitim";    public string CategoryLink2Url { get; set; } = "#";
    public string CategoryLink3Text  { get; set; } = "Sağlık";    public string CategoryLink3Url { get; set; } = "#";
    public string CategoryLink4Text  { get; set; } = "İnşaat";    public string CategoryLink4Url { get; set; } = "#";

    // İletişim
    public string ContactTitle { get; set; } = "İletişim";
    public string Address      { get; set; } = "Sümer, 27/1. Sk. No:16A, 34025 Zeytinburnu/İstanbul";
    public string Phone        { get; set; } = "+90 212 664 85 71";
    public string Email        { get; set; } = "info@buharamedresesi.org.tr";

    // Alt çubuk
    public string CopyrightText { get; set; } = "© 2026 Buhara Medresesi. Tüm hakları saklıdır.";
    public string KvkkUrl       { get; set; } = "/kvkk";
    public string GizlilikUrl   { get; set; } = "/gizlilik-politikasi";
}
