using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Plugin.Widgets.JoinCta.Models;

public record ConfigurationModel : BaseNopModel
{
    [NopResourceDisplayName("Admin.JoinCta.Title")]
    public string Title              { get; set; }

    [NopResourceDisplayName("Admin.JoinCta.BankSectionTitle")]
    public string BankSectionTitle   { get; set; }

    public string Bank1Currency      { get; set; }
    public string Bank1Name          { get; set; }
    public string Bank1Iban          { get; set; }

    public string Bank2Currency      { get; set; }
    public string Bank2Name          { get; set; }
    public string Bank2Iban          { get; set; }

    public string Bank3Currency      { get; set; }
    public string Bank3Name          { get; set; }
    public string Bank3Iban          { get; set; }

    [NopResourceDisplayName("Admin.JoinCta.DonateTitle")]
    public string DonateTitle        { get; set; }

    [NopResourceDisplayName("Admin.JoinCta.DonateDescription")]
    public string DonateDescription  { get; set; }

    [NopResourceDisplayName("Admin.JoinCta.DonateLinkText")]
    public string DonateLinkText     { get; set; }

    [NopResourceDisplayName("Admin.JoinCta.DonateUrl")]
    public string DonateUrl          { get; set; }

    [NopResourceDisplayName("Admin.JoinCta.ContactTitle")]
    public string ContactTitle       { get; set; }

    [NopResourceDisplayName("Admin.JoinCta.ContactDescription")]
    public string ContactDescription { get; set; }

    [NopResourceDisplayName("Admin.JoinCta.ContactLinkText")]
    public string ContactLinkText    { get; set; }
}
