using Nop.Core;
using Nop.Services.Plugins;

namespace Nop.Plugin.Misc.PluginManager;

public class PluginManagerPlugin : BasePlugin
{
    private readonly IWebHelper _webHelper;

    public PluginManagerPlugin(IWebHelper webHelper)
    {
        _webHelper = webHelper;
    }

    public override string GetConfigurationPageUrl()
        => _webHelper.GetStoreLocation() + "Admin/PluginManager/Index";
}
