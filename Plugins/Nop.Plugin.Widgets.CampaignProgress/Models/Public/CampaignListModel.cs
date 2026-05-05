namespace Nop.Plugin.Widgets.CampaignProgress.Models.Public;

public class CampaignListModel
{
    public IList<CampaignWidgetItemModel> Campaigns { get; set; } = new List<CampaignWidgetItemModel>();
}
