using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Plugin.Widgets.CampaignProgress.Models;

public record ConfigurationModel : BaseNopModel
{
    [NopResourceDisplayName("Plugins.Widgets.CampaignProgress.Title")]
    public string Title { get; set; }

    [NopResourceDisplayName("Plugins.Widgets.CampaignProgress.Description")]
    public string Description { get; set; }

    [NopResourceDisplayName("Plugins.Widgets.CampaignProgress.GoalAmount")]
    public decimal GoalAmount { get; set; }

    [NopResourceDisplayName("Plugins.Widgets.CampaignProgress.CurrentAmount")]
    public decimal CurrentAmount { get; set; }

    [NopResourceDisplayName("Plugins.Widgets.CampaignProgress.Currency")]
    public string Currency { get; set; }

    [NopResourceDisplayName("Plugins.Widgets.CampaignProgress.EndDate")]
    public string EndDate { get; set; }

    [NopResourceDisplayName("Plugins.Widgets.CampaignProgress.ButtonText")]
    public string ButtonText { get; set; }

    [NopResourceDisplayName("Plugins.Widgets.CampaignProgress.ButtonUrl")]
    public string ButtonUrl { get; set; }

    public int ActiveStoreScopeConfiguration { get; set; }
}
