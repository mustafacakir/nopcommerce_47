using Nop.Web.Models.Catalog;

namespace Nop.Plugin.Widgets.DonationGrid.Models;

public class DonationGridModel
{
    public List<DonationGridCategoryModel> Categories { get; set; } = new();
}

public class DonationGridCategoryModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string IconUrl { get; set; }
    public List<ProductOverviewModel> Products { get; set; } = new();
}
