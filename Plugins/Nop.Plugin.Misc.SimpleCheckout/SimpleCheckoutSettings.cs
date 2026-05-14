using Nop.Core.Configuration;

namespace Nop.Plugin.Misc.SimpleCheckout;

public class SimpleCheckoutSettings : ISettings
{
    public string DonationStoreIds { get; set; } = "";

    /// <summary>
    /// EFT/havale modunda gösterilecek açıklama metni (HTML destekler)
    /// </summary>
    public string EftDescription { get; set; } = "";
}
