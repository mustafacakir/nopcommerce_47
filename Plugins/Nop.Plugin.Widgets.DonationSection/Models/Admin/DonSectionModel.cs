using System.ComponentModel.DataAnnotations;
using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Plugin.Widgets.DonationSection.Models.Admin;

public record DonSectionListModel : BasePagedListModel<DonSectionModel> { }
public record DonSectionSearchModel : BaseSearchModel { }

public record DonSectionModel : BaseNopEntityModel
{
    [Required, NopResourceDisplayName("Admin.DonSection.Name")]
    public string Name { get; set; }

    [NopResourceDisplayName("Admin.DonSection.IconSvg")]
    public string IconSvg { get; set; }

    [NopResourceDisplayName("Admin.DonSection.Color")]
    public string Color { get; set; } = "#16a34a";

    [NopResourceDisplayName("Admin.DonSection.DisplayOrder")]
    public int DisplayOrder { get; set; }

    [NopResourceDisplayName("Admin.DonSection.IsActive")]
    public bool IsActive { get; set; } = true;
}
