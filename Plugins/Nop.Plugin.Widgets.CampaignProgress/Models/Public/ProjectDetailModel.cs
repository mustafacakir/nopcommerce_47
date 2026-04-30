namespace Nop.Plugin.Widgets.CampaignProgress.Models.Public;

public record ProjectDetailModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Slug { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public decimal GoalAmount { get; set; }
    public decimal CurrentAmount { get; set; }
    public int ProgressPercent { get; set; }
    public int LinkedProductId { get; set; }
}
