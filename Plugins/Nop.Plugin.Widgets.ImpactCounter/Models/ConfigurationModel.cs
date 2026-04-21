using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Plugin.Widgets.ImpactCounter.Models;

public record ConfigurationModel : BaseNopModel
{
    [NopResourceDisplayName("Plugins.Widgets.ImpactCounter.Stat1Icon")]
    public string Stat1Icon { get; set; }
    [NopResourceDisplayName("Plugins.Widgets.ImpactCounter.Stat1Value")]
    public string Stat1Value { get; set; }
    [NopResourceDisplayName("Plugins.Widgets.ImpactCounter.Stat1Label")]
    public string Stat1Label { get; set; }

    [NopResourceDisplayName("Plugins.Widgets.ImpactCounter.Stat2Icon")]
    public string Stat2Icon { get; set; }
    [NopResourceDisplayName("Plugins.Widgets.ImpactCounter.Stat2Value")]
    public string Stat2Value { get; set; }
    [NopResourceDisplayName("Plugins.Widgets.ImpactCounter.Stat2Label")]
    public string Stat2Label { get; set; }

    [NopResourceDisplayName("Plugins.Widgets.ImpactCounter.Stat3Icon")]
    public string Stat3Icon { get; set; }
    [NopResourceDisplayName("Plugins.Widgets.ImpactCounter.Stat3Value")]
    public string Stat3Value { get; set; }
    [NopResourceDisplayName("Plugins.Widgets.ImpactCounter.Stat3Label")]
    public string Stat3Label { get; set; }

    [NopResourceDisplayName("Plugins.Widgets.ImpactCounter.Stat4Icon")]
    public string Stat4Icon { get; set; }
    [NopResourceDisplayName("Plugins.Widgets.ImpactCounter.Stat4Value")]
    public string Stat4Value { get; set; }
    [NopResourceDisplayName("Plugins.Widgets.ImpactCounter.Stat4Label")]
    public string Stat4Label { get; set; }

    public int ActiveStoreScopeConfiguration { get; set; }
}
