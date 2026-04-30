using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Plugin.Widgets.Footer.BuharaMedresesi.Models;

public record ConfigurationModel : BaseNopModel
{
    public int ActiveStoreScopeConfiguration { get; set; }

    [NopResourceDisplayName("CtaTitle")]      public string CtaTitle      { get; set; }
    [NopResourceDisplayName("CtaSubtitle")]   public string CtaSubtitle   { get; set; }
    [NopResourceDisplayName("CtaButtonText")] public string CtaButtonText { get; set; }
    [NopResourceDisplayName("CtaButtonUrl")]  public string CtaButtonUrl  { get; set; }

    [NopResourceDisplayName("LogoUrl")]     public string LogoUrl     { get; set; }
    [NopResourceDisplayName("Description")] public string Description { get; set; }

    [NopResourceDisplayName("FacebookUrl")]  public string FacebookUrl  { get; set; }
    [NopResourceDisplayName("InstagramUrl")] public string InstagramUrl { get; set; }
    [NopResourceDisplayName("TwitterUrl")]   public string TwitterUrl   { get; set; }
    [NopResourceDisplayName("YoutubeUrl")]   public string YoutubeUrl   { get; set; }
    [NopResourceDisplayName("LinkedinUrl")]  public string LinkedinUrl  { get; set; }
    [NopResourceDisplayName("TiktokUrl")]    public string TiktokUrl    { get; set; }
    [NopResourceDisplayName("WhatsappUrl")]  public string WhatsappUrl  { get; set; }

    [NopResourceDisplayName("QuickLinksTitle")] public string QuickLinksTitle { get; set; }
    public string QuickLink1Text { get; set; } public string QuickLink1Url { get; set; }
    public string QuickLink2Text { get; set; } public string QuickLink2Url { get; set; }
    public string QuickLink3Text { get; set; } public string QuickLink3Url { get; set; }
    public string QuickLink4Text { get; set; } public string QuickLink4Url { get; set; }
    public string QuickLink5Text { get; set; } public string QuickLink5Url { get; set; }
    public string QuickLink6Text { get; set; } public string QuickLink6Url { get; set; }

    [NopResourceDisplayName("CategoryLinksTitle")] public string CategoryLinksTitle { get; set; }
    public string CategoryLink1Text { get; set; } public string CategoryLink1Url { get; set; }
    public string CategoryLink2Text { get; set; } public string CategoryLink2Url { get; set; }
    public string CategoryLink3Text { get; set; } public string CategoryLink3Url { get; set; }
    public string CategoryLink4Text { get; set; } public string CategoryLink4Url { get; set; }

    [NopResourceDisplayName("ContactTitle")] public string ContactTitle { get; set; }
    [NopResourceDisplayName("Address")]      public string Address      { get; set; }
    [NopResourceDisplayName("Phone")]        public string Phone        { get; set; }
    [NopResourceDisplayName("Email")]        public string Email        { get; set; }

    [NopResourceDisplayName("CopyrightText")] public string CopyrightText { get; set; }
    [NopResourceDisplayName("KvkkUrl")]       public string KvkkUrl       { get; set; }
    [NopResourceDisplayName("GizlilikUrl")]   public string GizlilikUrl   { get; set; }
}
