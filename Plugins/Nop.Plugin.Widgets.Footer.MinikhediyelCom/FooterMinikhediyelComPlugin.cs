using Nop.Core;
using Nop.Plugin.Widgets.Footer.MinikhediyelCom.Components;
using Nop.Services.Cms;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Plugins;

namespace Nop.Plugin.Widgets.Footer.MinikhediyelCom;

/// <summary>
/// minikhediyen.com — özel footer widget
/// Widget zone: voyage_footer_content
/// </summary>
public class FooterMinikhediyelComPlugin : BasePlugin, IWidgetPlugin
{
    private readonly ILocalizationService _localizationService;
    private readonly ISettingService _settingService;
    private readonly IWebHelper _webHelper;

    public FooterMinikhediyelComPlugin(
        ILocalizationService localizationService,
        ISettingService settingService,
        IWebHelper webHelper)
    {
        _localizationService = localizationService;
        _settingService = settingService;
        _webHelper = webHelper;
    }

    public bool HideInWidgetList => false;

    public Type GetWidgetViewComponent(string widgetZone) => typeof(FooterMinikhediyelComViewComponent);

    public Task<IList<string>> GetWidgetZonesAsync()
    {
        return Task.FromResult<IList<string>>(new List<string> { "voyage_footer_content" });
    }

    public override string GetConfigurationPageUrl()
    {
        return $"{_webHelper.GetStoreLocation()}Admin/WidgetsFooterMinikhediyelCom/Configure";
    }

    public override async Task InstallAsync()
    {
        await _settingService.SaveSettingAsync(new FooterMinikhediyelComSettings());
        await base.InstallAsync();
    }

    public override async Task UninstallAsync()
    {
        await _settingService.DeleteSettingAsync<FooterMinikhediyelComSettings>();
        await base.UninstallAsync();
    }
}
