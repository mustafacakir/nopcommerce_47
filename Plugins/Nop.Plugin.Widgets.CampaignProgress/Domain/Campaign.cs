using LinqToDB.Mapping;
using Nop.Core;

namespace Nop.Plugin.Widgets.CampaignProgress.Domain;

public class Campaign : BaseEntity
{
    [Column("title")]
    public string Title { get; set; }

    [Column("slug")]
    public string Slug { get; set; }

    [Column("description")]
    public string Description { get; set; }

    [Column("imageurl")]
    public string ImageUrl { get; set; }

    [Column("goalamount")]
    public decimal GoalAmount { get; set; }

    [Column("manualbonus")]
    public decimal ManualBonus { get; set; }

    [Column("linkedproductid")]
    public int LinkedProductId { get; set; }

    [Column("isactive")]
    public bool IsActive { get; set; }

    [Column("displayorder")]
    public int DisplayOrder { get; set; }

    [Column("createdonutc")]
    public DateTime CreatedOnUtc { get; set; }
}
