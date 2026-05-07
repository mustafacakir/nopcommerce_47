using System.Reflection;
using Nop.Data.Migrations;
using Nop.Services.Configuration;
using Nop.Services.Plugins;

namespace Nop.Plugin.Misc.KurbanOrganization;

public class KurbanOrganizationPlugin : BasePlugin
{
    private readonly ISettingService _settingService;
    private readonly IMigrationManager _migrationManager;

    public KurbanOrganizationPlugin(
        ISettingService settingService,
        IMigrationManager migrationManager)
    {
        _settingService = settingService;
        _migrationManager = migrationManager;
    }

    public override string GetConfigurationPageUrl() => "/Admin/KurbanAdmin/Configure";

    public override async Task InstallAsync()
    {
        _migrationManager.ApplyUpMigrations(Assembly.GetExecutingAssembly(), MigrationProcessType.Installation);
        await _settingService.SaveSettingAsync(new KurbanOrganizationSettings());
        await base.InstallAsync();
    }

    public override async Task UninstallAsync()
    {
        _migrationManager.ApplyDownMigrations(Assembly.GetExecutingAssembly());
        await _settingService.DeleteSettingAsync<KurbanOrganizationSettings>();
        await base.UninstallAsync();
    }
}
