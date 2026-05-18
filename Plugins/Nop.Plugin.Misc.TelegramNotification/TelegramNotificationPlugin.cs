using Nop.Services.Configuration;
using Nop.Services.Plugins;

namespace Nop.Plugin.Misc.TelegramNotification;

public class TelegramNotificationPlugin : BasePlugin
{
    private readonly ISettingService _settingService;

    public TelegramNotificationPlugin(ISettingService settingService)
    {
        _settingService = settingService;
    }

    public override string GetConfigurationPageUrl() => "/Admin/TelegramNotification/Configure";

    public override async Task InstallAsync()
    {
        await _settingService.SaveSettingAsync(new TelegramNotificationSettings());
        await base.InstallAsync();
    }

    public override async Task UninstallAsync()
    {
        await _settingService.DeleteSettingAsync<TelegramNotificationSettings>();
        await base.UninstallAsync();
    }
}
