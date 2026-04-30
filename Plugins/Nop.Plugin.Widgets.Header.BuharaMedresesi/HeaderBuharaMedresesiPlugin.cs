using Nop.Core;
using Nop.Plugin.Widgets.Header.BuharaMedresesi.Components;
using Nop.Services.Cms;
using Nop.Services.Configuration;
using Nop.Services.Plugins;
using Nop.Web.Framework.Infrastructure;

namespace Nop.Plugin.Widgets.Header.BuharaMedresesi;

public class HeaderBuharaMedresesiPlugin : BasePlugin, IWidgetPlugin
{
    private readonly ISettingService _settingService;
    private readonly IWebHelper _webHelper;

    public HeaderBuharaMedresesiPlugin(ISettingService settingService, IWebHelper webHelper)
    {
        _settingService = settingService;
        _webHelper = webHelper;
    }

    public bool HideInWidgetList => false;
    public Type GetWidgetViewComponent(string widgetZone) => typeof(HeaderBuharaMedresesiViewComponent);

    public Task<IList<string>> GetWidgetZonesAsync() =>
        Task.FromResult<IList<string>>(new List<string> { PublicWidgetZones.HeaderBefore });

    public override string GetConfigurationPageUrl() =>
        $"{_webHelper.GetStoreLocation()}Admin/HeaderBuharaMedresesiAdmin/Configure";

    public override async Task InstallAsync()
    {
        await _settingService.SaveSettingAsync(new HeaderBuharaMedresesiSettings());
        await base.InstallAsync();
    }

    public override async Task UninstallAsync()
    {
        await _settingService.DeleteSettingAsync<HeaderBuharaMedresesiSettings>();
        await base.UninstallAsync();
    }
}
