namespace Nop.Plugin.Widgets.Footer.MinikhediyelCom.Models;

public class PublicInfoModel
{
    public string StoreName { get; set; }
    public string LogoLetter { get; set; }
    public string Tagline { get; set; }
    public string InstagramUrl { get; set; }
    public string FacebookUrl { get; set; }
    public string TwitterUrl { get; set; }

    public string QuickLinksTitle { get; set; }
    public IList<(string Text, string Url)> QuickLinks { get; set; } = new List<(string, string)>();

    public string SupportTitle { get; set; }
    public IList<(string Text, string Url)> SupportLinks { get; set; } = new List<(string, string)>();

    public string ContactTitle { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }

    public string CopyrightText { get; set; }
}
