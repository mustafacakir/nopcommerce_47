using Nop.Core;

namespace Nop.Plugin.Widgets.VolunteerCta.Domain;

public class GonulluBasvuru : BaseEntity
{
    public string Ad { get; set; }
    public string Soyad { get; set; }
    public string Telefon { get; set; }
    public string Email { get; set; }
    public string Not { get; set; }
    public DateTime CreatedOnUtc { get; set; }
}
