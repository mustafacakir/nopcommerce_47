using System.ComponentModel.DataAnnotations;

namespace Nop.Plugin.Widgets.KumbaraForm.Models;

public class KumbaraFormModel
{
    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }

    [Required, EmailAddress]
    public string Email { get; set; }

    [Required]
    public string Phone { get; set; }

    [Required]
    public string City { get; set; }

    [Required]
    public string District { get; set; }

    [Required]
    public string Address { get; set; }

    [Required, Range(1, 50)]
    public int Quantity { get; set; } = 1;

    public string UsagePlace { get; set; } // ev / isyeri / okul / diger

    public string Message { get; set; }
}
