using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Plugin.Widgets.DonationSection.Models.Admin;

public record DonItemListModel : BasePagedListModel<DonItemModel> { }

public record DonItemSearchModel : BaseSearchModel
{
    public int SectionId { get; set; }
}

public record DonItemModel : BaseNopEntityModel
{
    [Required, NopResourceDisplayName("Admin.DonItem.SectionId")]
    public int SectionId { get; set; }

    [Required, NopResourceDisplayName("Admin.DonItem.Name")]
    public string Name { get; set; }

    [NopResourceDisplayName("Admin.DonItem.Description")]
    public string Description { get; set; }

    [NopResourceDisplayName("Admin.DonItem.ImageUrl")]
    public string ImageUrl { get; set; }

    [NopResourceDisplayName("Admin.DonItem.Price")]
    public decimal Price { get; set; }

    [NopResourceDisplayName("Admin.DonItem.ProductId")]
    public int ProductId { get; set; }

    [NopResourceDisplayName("Admin.DonItem.DisplayOrder")]
    public int DisplayOrder { get; set; }

    [NopResourceDisplayName("Admin.DonItem.IsActive")]
    public bool IsActive { get; set; } = true;

    public IList<SelectListItem> AvailableSections { get; set; } = new List<SelectListItem>();
}
