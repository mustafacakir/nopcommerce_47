namespace Nop.Plugin.Misc.KurbanOrganization.Models;

public class KurbanHisseModel
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public string HisseKodu { get; set; }
    public string KurbanTuru { get; set; }
    public int HisseSayisi { get; set; }
    public string MusteriAd { get; set; }
    public string MusteriTelefon { get; set; }
    public bool Kesildi { get; set; }
    public string KesimTarihi { get; set; }
    public bool BildirimGonderildi { get; set; }
    public string CreatedOn { get; set; }
    public string WhatsAppPhone { get; set; }
}

public class KurbanConfigureModel
{
    public int KurbanCategoryId { get; set; }
    public string WhatsAppAccessToken { get; set; }
    public string WhatsAppPhoneNumberId { get; set; }
    public string KesimTemplateName { get; set; }
    public string TemplateLanguage { get; set; }
    public bool Enabled { get; set; }
}
