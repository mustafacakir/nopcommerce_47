using System.ComponentModel.DataAnnotations;

namespace Nop.Plugin.Widgets.VolunteerCta.Models;

public class GonulluBasvuruModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Ad zorunludur.")]
    public string Ad { get; set; }

    [Required(ErrorMessage = "Soyad zorunludur.")]
    public string Soyad { get; set; }

    [Required(ErrorMessage = "Telefon zorunludur.")]
    public string Telefon { get; set; }

    public string Email { get; set; }

    public string Not { get; set; }

    public string WhatsAppPhone { get; set; }
    public string CreatedOn { get; set; }
}
