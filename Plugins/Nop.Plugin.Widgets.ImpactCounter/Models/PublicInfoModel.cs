using Nop.Web.Framework.Models;

namespace Nop.Plugin.Widgets.ImpactCounter.Models;

public record PublicInfoModel : BaseNopModel
{
    public List<StatItem> Stats { get; set; } = new();
}

public class StatItem
{
    public string Icon { get; set; }
    public string Value { get; set; }
    public string Label { get; set; }
}
