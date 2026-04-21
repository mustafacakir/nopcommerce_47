using Nop.Core;
using Nop.Plugin.Widgets.MarqueeBanner.Components;
using Nop.Services.Cms;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Plugins;
using Nop.Web.Framework.Infrastructure;

namespace Nop.Plugin.Widgets.MarqueeBanner;

public class MarqueeBannerPlugin : BasePlugin, IWidgetPlugin
{
    private readonly ILocalizationService _localizationService;
    private readonly ISettingService _settingService;
    private readonly IWebHelper _webHelper;

    public MarqueeBannerPlugin(ILocalizationService localizationService, ISettingService settingService, IWebHelper webHelper)
    {
        _localizationService = localizationService;
        _settingService = settingService;
        _webHelper = webHelper;
    }

    public bool HideInWidgetList => false;

    public Type GetWidgetViewComponent(string widgetZone) => typeof(MarqueeBannerViewComponent);

    public Task<IList<string>> GetWidgetZonesAsync()
    {
        return Task.FromResult<IList<string>>(new List<string> { PublicWidgetZones.BodyStartHtmlTagAfter });
    }

    public override string GetConfigurationPageUrl()
    {
        return $"{_webHelper.GetStoreLocation()}Admin/WidgetsMarqueeBanner/Configure";
    }

    public override async Task InstallAsync()
    {
        await _settingService.SaveSettingAsync(new MarqueeBannerSettings
        {
            Text = "Hoş geldiniz! Bağışlarınız için teşekkür ederiz.",
            BackgroundColor = "#e43d51",
            TextColor = "#ffffff",
            Speed = 40
        });

        await _localizationService.AddOrUpdateLocaleResourceAsync(new Dictionary<string, string>
        {
            ["Plugins.Widgets.MarqueeBanner.Text"] = "Metin",
            ["Plugins.Widgets.MarqueeBanner.Text.Hint"] = "Kayan bantta gösterilecek metin.",
            ["Plugins.Widgets.MarqueeBanner.Link"] = "Link",
            ["Plugins.Widgets.MarqueeBanner.Link.Hint"] = "Tıklandığında gidilecek URL (boş bırakılabilir).",
            ["Plugins.Widgets.MarqueeBanner.BackgroundColor"] = "Arka Plan Rengi",
            ["Plugins.Widgets.MarqueeBanner.BackgroundColor.Hint"] = "Hex renk kodu (örn: #e43d51)",
            ["Plugins.Widgets.MarqueeBanner.TextColor"] = "Metin Rengi",
            ["Plugins.Widgets.MarqueeBanner.TextColor.Hint"] = "Hex renk kodu (örn: #ffffff)",
            ["Plugins.Widgets.MarqueeBanner.Speed"] = "Hız",
            ["Plugins.Widgets.MarqueeBanner.Speed.Hint"] = "Kayma hızı (saniye, düşük = hızlı).",
        });

        await base.InstallAsync();
    }

    public override async Task UninstallAsync()
    {
        await _settingService.DeleteSettingAsync<MarqueeBannerSettings>();
        await _localizationService.DeleteLocaleResourcesAsync("Plugins.Widgets.MarqueeBanner");
        await base.UninstallAsync();
    }
}
