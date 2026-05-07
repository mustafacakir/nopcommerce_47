using System.Reflection;
using Nop.Core;
using Nop.Data.Migrations;
using Nop.Plugin.Widgets.VolunteerCta.Components;
using Nop.Services.Cms;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Plugins;
using Nop.Web.Framework.Infrastructure;

namespace Nop.Plugin.Widgets.VolunteerCta;

public class VolunteerCtaPlugin : BasePlugin, IWidgetPlugin
{
    private readonly ILocalizationService _localizationService;
    private readonly ISettingService _settingService;
    private readonly IWebHelper _webHelper;
    private readonly IMigrationManager _migrationManager;

    public VolunteerCtaPlugin(
        ILocalizationService localizationService,
        ISettingService settingService,
        IWebHelper webHelper,
        IMigrationManager migrationManager)
    {
        _localizationService = localizationService;
        _settingService = settingService;
        _webHelper = webHelper;
        _migrationManager = migrationManager;
    }

    public bool HideInWidgetList => false;
    public Type GetWidgetViewComponent(string widgetZone) => typeof(VolunteerCtaViewComponent);

    public Task<IList<string>> GetWidgetZonesAsync()
    {
        return Task.FromResult<IList<string>>(new List<string> { PublicWidgetZones.HomepageTop });
    }

    public override string GetConfigurationPageUrl() =>
        $"{_webHelper.GetStoreLocation()}Admin/WidgetsVolunteerCta/Configure";

    public override async Task InstallAsync()
    {
        _migrationManager.ApplyUpMigrations(Assembly.GetExecutingAssembly(), MigrationProcessType.Installation);
        await _settingService.SaveSettingAsync(new VolunteerCtaSettings());
        await base.InstallAsync();
    }

    public override async Task UninstallAsync()
    {
        _migrationManager.ApplyDownMigrations(Assembly.GetExecutingAssembly());
        await _settingService.DeleteSettingAsync<VolunteerCtaSettings>();
        await base.UninstallAsync();
    }
}
