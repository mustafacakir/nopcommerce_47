using System.ComponentModel.DataAnnotations;
using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Plugin.Widgets.CampaignProgress.Models.Admin;

public record CampaignModel : BaseNopEntityModel
{
    [NopResourceDisplayName("Başlık")]
    [Required]
    [StringLength(300)]
    public string Title { get; set; }

    [NopResourceDisplayName("Slug (URL)")]
    [Required]
    [StringLength(200)]
    public string Slug { get; set; }

    [NopResourceDisplayName("Açıklama")]
    public string Description { get; set; }

    [NopResourceDisplayName("Görsel URL")]
    [StringLength(500)]
    public string ImageUrl { get; set; }

    [NopResourceDisplayName("Hedef Tutar (₺)")]
    [Required]
    public decimal GoalAmount { get; set; }

    [NopResourceDisplayName("Manuel Bonus (₺)")]
    public decimal ManualBonus { get; set; }

    [NopResourceDisplayName("Ürün ID (CustomerEntersPrice)")]
    public int LinkedProductId { get; set; }

    [NopResourceDisplayName("Aktif")]
    public bool IsActive { get; set; }

    [NopResourceDisplayName("Sıra")]
    public int DisplayOrder { get; set; }

    public decimal CurrentAmount { get; set; }
    public int ProgressPercent { get; set; }
}

public record CampaignListModel : BasePagedListModel<CampaignModel> { }
public record CampaignSearchModel : BaseSearchModel { }
