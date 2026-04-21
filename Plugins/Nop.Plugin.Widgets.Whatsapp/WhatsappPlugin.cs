using Nop.Core;
using Nop.Services.Cms;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Plugins;
using Nop.Web.Framework.Infrastructure;

namespace Nop.Plugin.Widgets.Whatsapp;

public class WhatsappPlugin : BasePlugin, IWidgetPlugin
{
    private readonly ILocalizationService _localizationService;
    private readonly ISettingService _settingService;
    private readonly IWebHelper _webHelper;

    public WhatsappPlugin(ILocalizationService localizationService, ISettingService settingService, IWebHelper webHelper)
    {
        _localizationService = localizationService;
        _settingService = settingService;
        _webHelper = webHelper;
    }

    public bool HideInWidgetList => false;

    public Type GetWidgetViewComponent(string widgetZone)
    {
        return typeof(Components.WidgetsWhatsappViewComponent);
    }

    public Task<IList<string>> GetWidgetZonesAsync()
    {
        return Task.FromResult<IList<string>>(new List<string> { PublicWidgetZones.ContentAfter });
    }

    public override string GetConfigurationPageUrl()
    {
        return $"{_webHelper.GetStoreLocation()}Admin/WidgetsWhatsapp/Configure";
    }

    public override async Task InstallAsync()
    {
        await _localizationService.AddOrUpdateLocaleResourceAsync(new Dictionary<string, string>
        {
            ["Plugins.Widgets.Whatsapp.PhoneNumber"] = "PhoneNumber",
            ["Plugins.Widgets.Whatsapp.PhoneNumber.Hint"] = "Enter phone number.",
            ["Plugins.Widgets.Whatsapp.IconWidthAndHeight"] = "IconWidthAndHeight",
            ["Plugins.Widgets.Whatsapp.IconWidthAndHeight.Hint"] = "Enter IconWidthAndHeight."
        });
        await base.InstallAsync();
    }

    public override async Task UninstallAsync()
    {
        await _settingService.DeleteSettingAsync<WhatsappWidgetsSettings>();
        await _localizationService.DeleteLocaleResourcesAsync("Plugins.Widgets.Whatsapp");
        await base.UninstallAsync();
    }
}
