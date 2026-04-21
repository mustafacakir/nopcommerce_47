using Nop.Core;
using Nop.Plugin.Widgets.RecurringDonation.Components;
using Nop.Services.Cms;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Plugins;
using Nop.Web.Framework.Infrastructure;

namespace Nop.Plugin.Widgets.RecurringDonation;

public class RecurringDonationPlugin : BasePlugin, IWidgetPlugin
{
    private readonly ILocalizationService _localizationService;
    private readonly ISettingService _settingService;
    private readonly IWebHelper _webHelper;

    public RecurringDonationPlugin(ILocalizationService localizationService, ISettingService settingService, IWebHelper webHelper)
    {
        _localizationService = localizationService;
        _settingService = settingService;
        _webHelper = webHelper;
    }

    public bool HideInWidgetList => false;
    public Type GetWidgetViewComponent(string widgetZone) => typeof(RecurringDonationViewComponent);

    public Task<IList<string>> GetWidgetZonesAsync()
    {
        return Task.FromResult<IList<string>>(new List<string> { PublicWidgetZones.HomepageBottom });
    }

    public override string GetConfigurationPageUrl() =>
        $"{_webHelper.GetStoreLocation()}Admin/WidgetsRecurringDonation/Configure";

    public override async Task InstallAsync()
    {
        await _settingService.SaveSettingAsync(new RecurringDonationSettings());
        await _localizationService.AddOrUpdateLocaleResourceAsync(new Dictionary<string, string>
        {
            ["Plugins.Widgets.RecurringDonation.Title"] = "Başlık",
            ["Plugins.Widgets.RecurringDonation.Subtitle"] = "Alt Başlık",
            ["Plugins.Widgets.RecurringDonation.Amount1Label"] = "Tutar 1 Etiketi",
            ["Plugins.Widgets.RecurringDonation.Amount1Url"] = "Tutar 1 Linki",
            ["Plugins.Widgets.RecurringDonation.Amount2Label"] = "Tutar 2 Etiketi",
            ["Plugins.Widgets.RecurringDonation.Amount2Url"] = "Tutar 2 Linki",
            ["Plugins.Widgets.RecurringDonation.Amount3Label"] = "Tutar 3 Etiketi",
            ["Plugins.Widgets.RecurringDonation.Amount3Url"] = "Tutar 3 Linki",
            ["Plugins.Widgets.RecurringDonation.Amount4Label"] = "Tutar 4 Etiketi",
            ["Plugins.Widgets.RecurringDonation.Amount4Url"] = "Tutar 4 Linki",
            ["Plugins.Widgets.RecurringDonation.CustomAmountUrl"] = "Özel Tutar Linki",
        });
        await base.InstallAsync();
    }

    public override async Task UninstallAsync()
    {
        await _settingService.DeleteSettingAsync<RecurringDonationSettings>();
        await _localizationService.DeleteLocaleResourcesAsync("Plugins.Widgets.RecurringDonation");
        await base.UninstallAsync();
    }
}
