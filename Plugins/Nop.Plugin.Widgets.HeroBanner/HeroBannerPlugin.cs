using Nop.Core;
using Nop.Plugin.Widgets.HeroBanner.Components;
using Nop.Services.Cms;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Plugins;
using Nop.Web.Framework.Infrastructure;

namespace Nop.Plugin.Widgets.HeroBanner;

public class HeroBannerPlugin : BasePlugin, IWidgetPlugin
{
    private readonly ILocalizationService _localizationService;
    private readonly ISettingService _settingService;
    private readonly IWebHelper _webHelper;

    public HeroBannerPlugin(ILocalizationService localizationService, ISettingService settingService, IWebHelper webHelper)
    {
        _localizationService = localizationService;
        _settingService = settingService;
        _webHelper = webHelper;
    }

    public bool HideInWidgetList => false;

    public Type GetWidgetViewComponent(string widgetZone) => typeof(HeroBannerViewComponent);

    public Task<IList<string>> GetWidgetZonesAsync()
    {
        return Task.FromResult<IList<string>>(new List<string> { PublicWidgetZones.HomepageTop });
    }

    public override string GetConfigurationPageUrl()
    {
        return $"{_webHelper.GetStoreLocation()}Admin/WidgetsHeroBanner/Configure";
    }

    public override async Task InstallAsync()
    {
        await _settingService.SaveSettingAsync(new HeroBannerSettings());
        await _localizationService.AddOrUpdateLocaleResourceAsync(new Dictionary<string, string>
        {
            ["Plugins.Widgets.HeroBanner.Title"] = "Başlık",
            ["Plugins.Widgets.HeroBanner.Title.Hint"] = "Ana başlık metni.",
            ["Plugins.Widgets.HeroBanner.Subtitle"] = "Alt Başlık",
            ["Plugins.Widgets.HeroBanner.Subtitle.Hint"] = "Açıklama metni.",
            ["Plugins.Widgets.HeroBanner.ButtonText"] = "Buton Yazısı",
            ["Plugins.Widgets.HeroBanner.ButtonText.Hint"] = "CTA butonunda görünecek yazı.",
            ["Plugins.Widgets.HeroBanner.ButtonUrl"] = "Buton Linki",
            ["Plugins.Widgets.HeroBanner.ButtonUrl.Hint"] = "Butona tıklandığında gidilecek URL.",
            ["Plugins.Widgets.HeroBanner.BackgroundColor"] = "Arka Plan Rengi",
            ["Plugins.Widgets.HeroBanner.BackgroundColor.Hint"] = "Hex renk kodu.",
            ["Plugins.Widgets.HeroBanner.AccentColor"] = "Vurgu Rengi",
            ["Plugins.Widgets.HeroBanner.AccentColor.Hint"] = "Buton ve vurgu ögeleri için renk.",
            ["Plugins.Widgets.HeroBanner.BackgroundImageUrl"] = "Arka Plan Görseli URL",
            ["Plugins.Widgets.HeroBanner.BackgroundImageUrl.Hint"] = "Arka plan için görsel URL (boş bırakılabilir).",
        });
        await base.InstallAsync();
    }

    public override async Task UninstallAsync()
    {
        await _settingService.DeleteSettingAsync<HeroBannerSettings>();
        await _localizationService.DeleteLocaleResourcesAsync("Plugins.Widgets.HeroBanner");
        await base.UninstallAsync();
    }
}
