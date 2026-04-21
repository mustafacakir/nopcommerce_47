using Nop.Web.Framework.Models;

namespace Nop.Plugin.Widgets.CampaignProgress.Models;

public record PublicInfoModel : BaseNopModel
{
    public string Title { get; set; }
    public string Description { get; set; }
    public decimal GoalAmount { get; set; }
    public decimal CurrentAmount { get; set; }
    public string Currency { get; set; }
    public int ProgressPercent { get; set; }
    public string EndDate { get; set; }
    public string ButtonText { get; set; }
    public string ButtonUrl { get; set; }
}
