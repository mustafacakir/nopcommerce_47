using Nop.Core.Configuration;

namespace Nop.Plugin.Misc.SimpleCheckout;

public class SimpleCheckoutSettings : ISettings
{
    /// <summary>
    /// Comma-separated store IDs that use simple checkout (empty = all stores)
    /// </summary>
    public string DonationStoreIds { get; set; } = "";
}
