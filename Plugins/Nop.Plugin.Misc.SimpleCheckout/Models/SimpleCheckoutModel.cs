using System.ComponentModel.DataAnnotations;

namespace Nop.Plugin.Misc.SimpleCheckout.Models;

public class SimpleCheckoutModel
{
    [Required(ErrorMessage = "Ad zorunludur")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "Soyad zorunludur")]
    public string LastName { get; set; }

    [Required(ErrorMessage = "E-posta zorunludur")]
    [EmailAddress(ErrorMessage = "Geçerli bir e-posta girin")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Telefon zorunludur")]
    public string Phone { get; set; }

    public List<CartItemModel> CartItems { get; set; } = new();
    public string OrderTotalFormatted { get; set; }
    public List<string> Errors { get; set; } = new();
}

public class CartItemModel
{
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public string SubTotalFormatted { get; set; }
}
