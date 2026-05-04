using Nop.Plugin.Widgets.JoinCta.Components;
using Nop.Services.Cms;
using Nop.Services.Configuration;
using Nop.Services.Plugins;
using Nop.Web.Framework.Infrastructure;

namespace Nop.Plugin.Widgets.JoinCta;

public class JoinCtaPlugin : BasePlugin, IWidgetPlugin
{
    private readonly ISettingService _settingService;

    public JoinCtaPlugin(ISettingService settingService)
        => _settingService = settingService;

    public bool HideInWidgetList => false;
    public Type GetWidgetViewComponent(string widgetZone) => typeof(JoinCtaViewComponent);

    public Task<IList<string>> GetWidgetZonesAsync() =>
        Task.FromResult<IList<string>>(new List<string> { PublicWidgetZones.HomepageBottom });

    public override string GetConfigurationPageUrl() => "/Admin/JoinCtaAdmin/Configure";

    public override async Task InstallAsync()
    {
        await _settingService.SaveSettingAsync(new JoinCtaSettings());
        await base.InstallAsync();
    }

    public override async Task UninstallAsync()
    {
        await _settingService.DeleteSettingAsync<JoinCtaSettings>();
        await base.UninstallAsync();
    }
}
