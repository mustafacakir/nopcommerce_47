using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Plugin.Widgets.RecurringDonation.Models;

public record ConfigurationModel : BaseNopModel
{
    [NopResourceDisplayName("Plugins.Widgets.RecurringDonation.Title")]
    public string Title { get; set; }

    [NopResourceDisplayName("Plugins.Widgets.RecurringDonation.Subtitle")]
    public string Subtitle { get; set; }

    [NopResourceDisplayName("Plugins.Widgets.RecurringDonation.Amount1Label")]
    public string Amount1Label { get; set; }
    [NopResourceDisplayName("Plugins.Widgets.RecurringDonation.Amount1Url")]
    public string Amount1Url { get; set; }

    [NopResourceDisplayName("Plugins.Widgets.RecurringDonation.Amount2Label")]
    public string Amount2Label { get; set; }
    [NopResourceDisplayName("Plugins.Widgets.RecurringDonation.Amount2Url")]
    public string Amount2Url { get; set; }

    [NopResourceDisplayName("Plugins.Widgets.RecurringDonation.Amount3Label")]
    public string Amount3Label { get; set; }
    [NopResourceDisplayName("Plugins.Widgets.RecurringDonation.Amount3Url")]
    public string Amount3Url { get; set; }

    [NopResourceDisplayName("Plugins.Widgets.RecurringDonation.Amount4Label")]
    public string Amount4Label { get; set; }
    [NopResourceDisplayName("Plugins.Widgets.RecurringDonation.Amount4Url")]
    public string Amount4Url { get; set; }

    [NopResourceDisplayName("Plugins.Widgets.RecurringDonation.CustomAmountUrl")]
    public string CustomAmountUrl { get; set; }

    public int ActiveStoreScopeConfiguration { get; set; }
}
