namespace Nop.Plugin.Widgets.JoinCta.Models;

public class BankAccountModel
{
    public string Currency { get; set; }
    public string BankName { get; set; }
    public string Iban     { get; set; }
}

public class PublicModel
{
    public string Title              { get; set; }
    public string BankSectionTitle   { get; set; }
    public List<BankAccountModel> BankAccounts { get; set; } = new();
    public string DonateTitle        { get; set; }
    public string DonateDescription  { get; set; }
    public string DonateLinkText     { get; set; }
    public string DonateUrl          { get; set; }
    public string ContactTitle       { get; set; }
    public string ContactDescription { get; set; }
    public string ContactLinkText    { get; set; }
}
