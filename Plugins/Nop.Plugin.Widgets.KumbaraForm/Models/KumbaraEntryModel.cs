using Nop.Web.Framework.Models;

namespace Nop.Plugin.Widgets.KumbaraForm.Models;

public record KumbaraEntryModel : BaseNopEntityModel
{
    public string FullName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string City { get; set; }
    public string District { get; set; }
    public string Address { get; set; }
    public int Quantity { get; set; }
    public string UsagePlace { get; set; }
    public string Message { get; set; }
    public bool IsRead { get; set; }
    public string CreatedOn { get; set; }
}

public record KumbaraEntryListModel : BasePagedListModel<KumbaraEntryModel> { }

public record KumbaraEntrySearchModel : BaseSearchModel { }
