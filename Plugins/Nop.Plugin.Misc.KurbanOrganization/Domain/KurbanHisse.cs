using Nop.Core;

namespace Nop.Plugin.Misc.KurbanOrganization.Domain;

public class KurbanHisse : BaseEntity
{
    public int OrderId { get; set; }
    public int OrderItemId { get; set; }
    public int CustomerId { get; set; }
    public string HisseKodu { get; set; }
    public string KurbanTuru { get; set; }
    public int HisseSayisi { get; set; }
    public bool Kesildi { get; set; }
    public DateTime? KesimTarihi { get; set; }
    public bool BildirimGonderildi { get; set; }
    public string MusteriAd { get; set; }
    public string MusteriTelefon { get; set; }
    public DateTime CreatedOnUtc { get; set; }
}
