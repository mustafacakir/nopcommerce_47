using Nop.Core;
using Nop.Plugin.Widgets.AccessoryHero.Components;
using Nop.Services.Cms;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Plugins;
using Nop.Web.Framework.Infrastructure;

namespace Nop.Plugin.Widgets.AccessoryHero;

public class AccessoryHeroPlugin : BasePlugin, IWidgetPlugin
{
    private readonly ILocalizationService _localizationService;
    private readonly ISettingService _settingService;
    private readonly IWebHelper _webHelper;

    public AccessoryHeroPlugin(ILocalizationService localizationService, ISettingService settingService, IWebHelper webHelper)
    {
        _localizationService = localizationService;
        _settingService = settingService;
        _webHelper = webHelper;
    }

    public bool HideInWidgetList => false;

    public Type GetWidgetViewComponent(string widgetZone) => typeof(AccessoryHeroViewComponent);

    public Task<IList<string>> GetWidgetZonesAsync()
    {
        return Task.FromResult<IList<string>>(new List<string> { PublicWidgetZones.HomepageTop });
    }

    public override string GetConfigurationPageUrl()
    {
        return $"{_webHelper.GetStoreLocation()}Admin/WidgetsAccessoryHero/Configure";
    }

    public override async Task InstallAsync()
    {
        await _settingService.SaveSettingAsync(new AccessoryHeroSettings());
        await _localizationService.AddOrUpdateLocaleResourceAsync(new Dictionary<string, string>
        {
            ["Plugins.Widgets.AccessoryHero.BadgeText"] = "Rozet Metni",
            ["Plugins.Widgets.AccessoryHero.TitleLine1"] = "Başlık 1. Satır",
            ["Plugins.Widgets.AccessoryHero.TitleAccent"] = "Vurgulu Satır (turuncu)",
            ["Plugins.Widgets.AccessoryHero.TitleLine2"] = "Başlık 3. Satır",
            ["Plugins.Widgets.AccessoryHero.Description"] = "Açıklama",
            ["Plugins.Widgets.AccessoryHero.Button1Text"] = "1. Buton Metni",
            ["Plugins.Widgets.AccessoryHero.Button1Url"] = "1. Buton Linki",
            ["Plugins.Widgets.AccessoryHero.Button2Text"] = "2. Buton Metni",
            ["Plugins.Widgets.AccessoryHero.Button2Url"] = "2. Buton Linki",
            ["Plugins.Widgets.AccessoryHero.Image1Url"] = "Görsel 1 URL",
            ["Plugins.Widgets.AccessoryHero.Image2Url"] = "Görsel 2 URL",
            ["Plugins.Widgets.AccessoryHero.Image3Url"] = "Görsel 3 URL",
            ["Plugins.Widgets.AccessoryHero.Image4Url"] = "Görsel 4 URL",
        });
        await base.InstallAsync();
    }

    public override async Task UninstallAsync()
    {
        await _settingService.DeleteSettingAsync<AccessoryHeroSettings>();
        await _localizationService.DeleteLocaleResourcesAsync("Plugins.Widgets.AccessoryHero");
        await base.UninstallAsync();
    }
}
