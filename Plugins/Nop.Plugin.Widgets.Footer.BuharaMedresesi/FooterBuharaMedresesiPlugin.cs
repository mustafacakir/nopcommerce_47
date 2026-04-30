using Nop.Core;
using Nop.Plugin.Widgets.Footer.BuharaMedresesi.Components;
using Nop.Services.Cms;
using Nop.Services.Configuration;
using Nop.Services.Plugins;

namespace Nop.Plugin.Widgets.Footer.BuharaMedresesi;

public class FooterBuharaMedresesiPlugin : BasePlugin, IWidgetPlugin
{
    private readonly ISettingService _settingService;
    private readonly IWebHelper _webHelper;

    public FooterBuharaMedresesiPlugin(ISettingService settingService, IWebHelper webHelper)
    {
        _settingService = settingService;
        _webHelper = webHelper;
    }

    public bool HideInWidgetList => false;
    public Type GetWidgetViewComponent(string widgetZone) => typeof(FooterBuharaMedresesiViewComponent);

    public Task<IList<string>> GetWidgetZonesAsync() =>
        Task.FromResult<IList<string>>(new List<string> { "voyage_footer_content" });

    public override string GetConfigurationPageUrl() =>
        $"{_webHelper.GetStoreLocation()}Admin/FooterBuharaMedresesiAdmin/Configure";

    public override async Task InstallAsync()
    {
        await _settingService.SaveSettingAsync(new FooterBuharaMedresesiSettings());
        await base.InstallAsync();
    }

    public override async Task UninstallAsync()
    {
        await _settingService.DeleteSettingAsync<FooterBuharaMedresesiSettings>();
        await base.UninstallAsync();
    }
}
