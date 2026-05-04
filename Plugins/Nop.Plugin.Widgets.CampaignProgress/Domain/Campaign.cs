using Nop.Core;

namespace Nop.Plugin.Widgets.CampaignProgress.Domain;

public class Campaign : BaseEntity
{
    public string Title { get; set; }
    public string Slug { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public decimal GoalAmount { get; set; }
    public decimal ManualBonus { get; set; }
    public int LinkedProductId { get; set; }
    public bool IsActive { get; set; }
    public int DisplayOrder { get; set; }
    public DateTime CreatedOnUtc { get; set; }
}
