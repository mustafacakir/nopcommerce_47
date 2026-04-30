using System.ComponentModel.DataAnnotations;
using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Plugin.Widgets.HeroSlider.Models.Admin;

public record HeroSlideListModel : BasePagedListModel<HeroSlideModel> { }

public record HeroSlideSearchModel : BaseSearchModel { }

public record HeroSlideModel : BaseNopEntityModel
{
    [NopResourceDisplayName("Admin.HeroSlider.CategoryName")]
    [Required]
    public string CategoryName { get; set; }

    [NopResourceDisplayName("Admin.HeroSlider.CategoryIcon")]
    public string CategoryIcon { get; set; }

    [NopResourceDisplayName("Admin.HeroSlider.Title")]
    [Required]
    public string Title { get; set; }

    [NopResourceDisplayName("Admin.HeroSlider.Subtitle")]
    public string Subtitle { get; set; }

    [NopResourceDisplayName("Admin.HeroSlider.BadgeLabel")]
    public string BadgeLabel { get; set; }

    [NopResourceDisplayName("Admin.HeroSlider.PriceBadge")]
    public string PriceBadge { get; set; }

    [NopResourceDisplayName("Admin.HeroSlider.PrimaryButtonText")]
    public string PrimaryButtonText { get; set; }

    [NopResourceDisplayName("Admin.HeroSlider.PrimaryButtonUrl")]
    public string PrimaryButtonUrl { get; set; }

    [NopResourceDisplayName("Admin.HeroSlider.SecondaryButtonText")]
    public string SecondaryButtonText { get; set; }

    [NopResourceDisplayName("Admin.HeroSlider.SecondaryButtonUrl")]
    public string SecondaryButtonUrl { get; set; }

    [NopResourceDisplayName("Admin.HeroSlider.ImageUrl")]
    public string ImageUrl { get; set; }

    [NopResourceDisplayName("Admin.HeroSlider.DisplayOrder")]
    public int DisplayOrder { get; set; }

    [NopResourceDisplayName("Admin.HeroSlider.IsActive")]
    public bool IsActive { get; set; } = true;
}
