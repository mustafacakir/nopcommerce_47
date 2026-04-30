using Nop.Core;

namespace Nop.Plugin.Widgets.KumbaraForm.Domain;

public class KumbaraEntry : BaseEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string City { get; set; }
    public string District { get; set; }
    public string Address { get; set; }
    public int Quantity { get; set; }
    public string UsagePlace { get; set; }
    public string Message { get; set; }
    public bool IsRead { get; set; }
    public DateTime CreatedOnUtc { get; set; }
}
