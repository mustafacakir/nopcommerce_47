using Nop.Web.Models.Catalog;

namespace Nop.Plugin.Widgets.DonationCatalog.Models;

public class DonationPageModel
{
    public List<DonationCategoryViewModel> Categories { get; set; } = new();
}

public class DonationCategoryViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string IconUrl { get; set; }
    public List<ProductOverviewModel> Products { get; set; } = new();
}
