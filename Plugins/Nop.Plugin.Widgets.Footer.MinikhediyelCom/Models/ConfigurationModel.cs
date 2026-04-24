using Nop.Web.Framework.Models;

namespace Nop.Plugin.Widgets.Footer.MinikhediyelCom.Models;

public record ConfigurationModel : BaseNopModel
{
    public int ActiveStoreScopeConfiguration { get; set; }

    public string StoreName { get; set; }
    public string LogoLetter { get; set; }
    public string Tagline { get; set; }
    public string InstagramUrl { get; set; }
    public string FacebookUrl { get; set; }
    public string TwitterUrl { get; set; }

    public string QuickLinksTitle { get; set; }
    public string QuickLink1Text { get; set; }
    public string QuickLink1Url { get; set; }
    public string QuickLink2Text { get; set; }
    public string QuickLink2Url { get; set; }
    public string QuickLink3Text { get; set; }
    public string QuickLink3Url { get; set; }
    public string QuickLink4Text { get; set; }
    public string QuickLink4Url { get; set; }

    public string SupportTitle { get; set; }
    public string SupportLink1Text { get; set; }
    public string SupportLink1Url { get; set; }
    public string SupportLink2Text { get; set; }
    public string SupportLink2Url { get; set; }
    public string SupportLink3Text { get; set; }
    public string SupportLink3Url { get; set; }
    public string SupportLink4Text { get; set; }
    public string SupportLink4Url { get; set; }

    public string ContactTitle { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }

    public string CopyrightText { get; set; }
}
