using Nop.Core;
using Nop.Plugin.Widgets.SocialShare.Components;
using Nop.Services.Cms;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Plugins;
using Nop.Web.Framework.Infrastructure;

namespace Nop.Plugin.Widgets.SocialShare;

public class SocialSharePlugin : BasePlugin, IWidgetPlugin
{
    private readonly ILocalizationService _localizationService;
    private readonly ISettingService _settingService;
    private readonly IWebHelper _webHelper;

    public SocialSharePlugin(ILocalizationService localizationService, ISettingService settingService, IWebHelper webHelper)
    {
        _localizationService = localizationService;
        _settingService = settingService;
        _webHelper = webHelper;
    }

    public bool HideInWidgetList => false;
    public Type GetWidgetViewComponent(string widgetZone) => typeof(SocialShareViewComponent);

    public Task<IList<string>> GetWidgetZonesAsync()
    {
        return Task.FromResult<IList<string>>(new List<string> { PublicWidgetZones.ProductDetailsBeforeCollateral });
    }

    public override string GetConfigurationPageUrl() =>
        $"{_webHelper.GetStoreLocation()}Admin/WidgetsSocialShare/Configure";

    public override async Task InstallAsync()
    {
        await _settingService.SaveSettingAsync(new SocialShareSettings());
        await _localizationService.AddOrUpdateLocaleResourceAsync(new Dictionary<string, string>
        {
            ["Plugins.Widgets.SocialShare.Title"] = "Paylaşım Başlığı",
            ["Plugins.Widgets.SocialShare.ShowWhatsApp"] = "WhatsApp Göster",
            ["Plugins.Widgets.SocialShare.ShowFacebook"] = "Facebook Göster",
            ["Plugins.Widgets.SocialShare.ShowX"] = "X (Twitter) Göster",
            ["Plugins.Widgets.SocialShare.ShowCopyLink"] = "Link Kopyala Göster",
        });
        await base.InstallAsync();
    }

    public override async Task UninstallAsync()
    {
        await _settingService.DeleteSettingAsync<SocialShareSettings>();
        await _localizationService.DeleteLocaleResourcesAsync("Plugins.Widgets.SocialShare");
        await base.UninstallAsync();
    }
}
