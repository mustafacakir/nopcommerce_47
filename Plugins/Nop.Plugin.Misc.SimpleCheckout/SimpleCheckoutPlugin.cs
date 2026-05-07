using Nop.Services.Configuration;
using Nop.Services.Plugins;

namespace Nop.Plugin.Misc.SimpleCheckout;

public class SimpleCheckoutPlugin : BasePlugin
{
    private readonly ISettingService _settingService;

    public SimpleCheckoutPlugin(ISettingService settingService)
    {
        _settingService = settingService;
    }

    public override string GetConfigurationPageUrl() => "/Admin/SimpleCheckoutAdmin/Configure";

    public override async Task InstallAsync()
    {
        await _settingService.SaveSettingAsync(new SimpleCheckoutSettings());
        await base.InstallAsync();
    }

    public override async Task UninstallAsync()
    {
        await _settingService.DeleteSettingAsync<SimpleCheckoutSettings>();
        await base.UninstallAsync();
    }
}
