using Nop.Services.Configuration;
using Nop.Services.Plugins;

namespace Nop.Plugin.Misc.WhatsAppNotification;

public class WhatsAppNotificationPlugin : BasePlugin
{
    private readonly ISettingService _settingService;

    public WhatsAppNotificationPlugin(ISettingService settingService)
    {
        _settingService = settingService;
    }

    public override string GetConfigurationPageUrl() => "/Admin/WhatsAppNotification/Configure";

    public override async Task InstallAsync()
    {
        await _settingService.SaveSettingAsync(new WhatsAppNotificationSettings());
        await base.InstallAsync();
    }

    public override async Task UninstallAsync()
    {
        await _settingService.DeleteSettingAsync<WhatsAppNotificationSettings>();
        await base.UninstallAsync();
    }
}
