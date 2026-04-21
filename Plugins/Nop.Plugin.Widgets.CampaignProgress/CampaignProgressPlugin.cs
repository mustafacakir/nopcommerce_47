using Nop.Core;
using Nop.Plugin.Widgets.CampaignProgress.Components;
using Nop.Services.Cms;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Plugins;
using Nop.Web.Framework.Infrastructure;

namespace Nop.Plugin.Widgets.CampaignProgress;

public class CampaignProgressPlugin : BasePlugin, IWidgetPlugin
{
    private readonly ILocalizationService _localizationService;
    private readonly ISettingService _settingService;
    private readonly IWebHelper _webHelper;

    public CampaignProgressPlugin(ILocalizationService localizationService, ISettingService settingService, IWebHelper webHelper)
    {
        _localizationService = localizationService;
        _settingService = settingService;
        _webHelper = webHelper;
    }

    public bool HideInWidgetList => false;
    public Type GetWidgetViewComponent(string widgetZone) => typeof(CampaignProgressViewComponent);

    public Task<IList<string>> GetWidgetZonesAsync()
    {
        return Task.FromResult<IList<string>>(new List<string> { PublicWidgetZones.HomepageBeforeProducts });
    }

    public override string GetConfigurationPageUrl() =>
        $"{_webHelper.GetStoreLocation()}Admin/WidgetsCampaignProgress/Configure";

    public override async Task InstallAsync()
    {
        await _settingService.SaveSettingAsync(new CampaignProgressSettings());
        await _localizationService.AddOrUpdateLocaleResourceAsync(new Dictionary<string, string>
        {
            ["Plugins.Widgets.CampaignProgress.Title"] = "Kampanya Başlığı",
            ["Plugins.Widgets.CampaignProgress.Description"] = "Açıklama",
            ["Plugins.Widgets.CampaignProgress.GoalAmount"] = "Hedef Tutar",
            ["Plugins.Widgets.CampaignProgress.CurrentAmount"] = "Mevcut Tutar",
            ["Plugins.Widgets.CampaignProgress.Currency"] = "Para Birimi",
            ["Plugins.Widgets.CampaignProgress.EndDate"] = "Bitiş Tarihi",
            ["Plugins.Widgets.CampaignProgress.ButtonText"] = "Buton Yazısı",
            ["Plugins.Widgets.CampaignProgress.ButtonUrl"] = "Buton Linki",
        });
        await base.InstallAsync();
    }

    public override async Task UninstallAsync()
    {
        await _settingService.DeleteSettingAsync<CampaignProgressSettings>();
        await _localizationService.DeleteLocaleResourcesAsync("Plugins.Widgets.CampaignProgress");
        await base.UninstallAsync();
    }
}
