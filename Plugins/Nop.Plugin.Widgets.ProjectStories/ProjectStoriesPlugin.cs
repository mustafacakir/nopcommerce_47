using Nop.Core;
using Nop.Plugin.Widgets.ProjectStories.Components;
using Nop.Services.Cms;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Plugins;
using Nop.Web.Framework.Infrastructure;

namespace Nop.Plugin.Widgets.ProjectStories;

public class ProjectStoriesPlugin : BasePlugin, IWidgetPlugin
{
    private readonly ILocalizationService _localizationService;
    private readonly ISettingService _settingService;
    private readonly IWebHelper _webHelper;

    public ProjectStoriesPlugin(ILocalizationService localizationService, ISettingService settingService, IWebHelper webHelper)
    {
        _localizationService = localizationService;
        _settingService = settingService;
        _webHelper = webHelper;
    }

    public bool HideInWidgetList => false;
    public Type GetWidgetViewComponent(string widgetZone) => typeof(ProjectStoriesViewComponent);

    public Task<IList<string>> GetWidgetZonesAsync()
    {
        return Task.FromResult<IList<string>>(new List<string> { PublicWidgetZones.HomepageBeforeProducts });
    }

    public override string GetConfigurationPageUrl() =>
        $"{_webHelper.GetStoreLocation()}Admin/WidgetsProjectStories/Configure";

    public override async Task InstallAsync()
    {
        await _settingService.SaveSettingAsync(new ProjectStoriesSettings());
        await base.InstallAsync();
    }

    public override async Task UninstallAsync()
    {
        await _settingService.DeleteSettingAsync<ProjectStoriesSettings>();
        await base.UninstallAsync();
    }
}
