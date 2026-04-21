using Nop.Core.Configuration;

namespace Nop.Plugin.Widgets.CampaignProgress;

public class CampaignProgressSettings : ISettings
{
    public string Title { get; set; } = "Ramazan Yardım Kampanyası";
    public string Description { get; set; } = "Bu Ramazan, ihtiyaç sahibi ailelere ulaşmak için destek olun.";
    public decimal GoalAmount { get; set; } = 50000;
    public decimal CurrentAmount { get; set; } = 0;
    public string Currency { get; set; } = "₺";
    public string EndDate { get; set; } = "";
    public string ButtonText { get; set; } = "Kampanyaya Katıl";
    public string ButtonUrl { get; set; } = "";
}
