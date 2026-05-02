using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Plugin.Widgets.DonationGrid.Models;

public record ConfigureModel : BaseNopModel
{
    public int ActiveStoreScopeConfiguration { get; set; }

    [NopResourceDisplayName("Kök Kategori ID")]
    public int RootCategoryId { get; set; }
    public bool RootCategoryId_OverrideForStore { get; set; }
}
