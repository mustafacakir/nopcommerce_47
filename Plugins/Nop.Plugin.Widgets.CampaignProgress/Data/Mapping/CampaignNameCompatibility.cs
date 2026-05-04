using Nop.Data.Mapping;
using Nop.Plugin.Widgets.CampaignProgress.Domain;

namespace Nop.Plugin.Widgets.CampaignProgress.Data.Mapping;

public class CampaignNameCompatibility : INameCompatibility
{
    public Dictionary<Type, string> TableNames => new()
    {
        { typeof(Campaign), "CampaignProgress" }
    };

    public Dictionary<(Type, string), string> ColumnName => new();
}
