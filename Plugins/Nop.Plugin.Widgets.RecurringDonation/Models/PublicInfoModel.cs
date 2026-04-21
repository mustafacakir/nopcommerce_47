using Nop.Web.Framework.Models;

namespace Nop.Plugin.Widgets.RecurringDonation.Models;

public record PublicInfoModel : BaseNopModel
{
    public string Title { get; set; }
    public string Subtitle { get; set; }
    public List<AmountOption> Amounts { get; set; } = new();
    public string CustomAmountUrl { get; set; }
}

public class AmountOption
{
    public string Label { get; set; }
    public string Url { get; set; }
}
