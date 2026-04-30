namespace Nop.Plugin.Widgets.Footer.BuharaMedresesi.Models;

public class LinkItem
{
    public string Text { get; set; }
    public string Url  { get; set; }
}

public class PublicInfoModel
{
    // CTA
    public string CtaTitle      { get; set; }
    public string CtaSubtitle   { get; set; }
    public string CtaButtonText { get; set; }
    public string CtaButtonUrl  { get; set; }

    // Marka
    public string LogoUrl     { get; set; }
    public string Description { get; set; }

    // Sosyal
    public string FacebookUrl  { get; set; }
    public string InstagramUrl { get; set; }
    public string TwitterUrl   { get; set; }
    public string YoutubeUrl   { get; set; }
    public string LinkedinUrl  { get; set; }
    public string TiktokUrl    { get; set; }
    public string WhatsappUrl  { get; set; }

    // Linkler
    public string QuickLinksTitle    { get; set; }
    public List<LinkItem> QuickLinks { get; set; } = new();

    public string CategoryLinksTitle    { get; set; }
    public List<LinkItem> CategoryLinks { get; set; } = new();

    // İletişim
    public string ContactTitle { get; set; }
    public string Address      { get; set; }
    public string Phone        { get; set; }
    public string Email        { get; set; }

    // Alt
    public string CopyrightText { get; set; }
    public string KvkkUrl       { get; set; }
    public string GizlilikUrl   { get; set; }
}
