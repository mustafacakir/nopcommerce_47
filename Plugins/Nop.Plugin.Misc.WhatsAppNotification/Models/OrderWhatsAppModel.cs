namespace Nop.Plugin.Misc.WhatsAppNotification.Models;

public class OrderWhatsAppModel
{
    public int OrderId { get; set; }
    public string CustomOrderNumber { get; set; }
    public string MusteriAd { get; set; }
    public string MusteriTelefon { get; set; }
    public string WhatsAppPhone { get; set; }
    public string OrderTotal { get; set; }
    public string CreatedOn { get; set; }
}
