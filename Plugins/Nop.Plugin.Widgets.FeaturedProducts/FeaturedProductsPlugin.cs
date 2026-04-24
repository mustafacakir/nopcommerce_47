using Nop.Core;
using Nop.Plugin.Widgets.FeaturedProducts.Components;
using Nop.Services.Cms;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Plugins;
using Nop.Web.Framework.Infrastructure;

namespace Nop.Plugin.Widgets.FeaturedProducts;

public class FeaturedProductsPlugin : BasePlugin, IWidgetPlugin
{
    private readonly ILocalizationService _localizationService;
    private readonly ISettingService _settingService;
    private readonly IWebHelper _webHelper;

    public FeaturedProductsPlugin(ILocalizationService localizationService, ISettingService settingService, IWebHelper webHelper)
    {
        _localizationService = localizationService;
        _settingService = settingService;
        _webHelper = webHelper;
    }

    public bool HideInWidgetList => false;

    public Type GetWidgetViewComponent(string widgetZone) => typeof(FeaturedProductsViewComponent);

    public Task<IList<string>> GetWidgetZonesAsync()
    {
        return Task.FromResult<IList<string>>(new List<string> { PublicWidgetZones.HomepageTop });
    }

    public override string GetConfigurationPageUrl()
    {
        return $"{_webHelper.GetStoreLocation()}Admin/WidgetsFeaturedProducts/Configure";
    }

    public override async Task InstallAsync()
    {
        await _settingService.SaveSettingAsync(new FeaturedProductsSettings());
        await _localizationService.AddOrUpdateLocaleResourceAsync(new Dictionary<string, string>
        {
            ["Plugins.Widgets.FeaturedProducts.SectionBadge"] = "Bölüm Rozeti",
            ["Plugins.Widgets.FeaturedProducts.SectionTitle"] = "Başlık",
            ["Plugins.Widgets.FeaturedProducts.SectionSubtitle"] = "Alt Başlık",
            ["Plugins.Widgets.FeaturedProducts.ViewAllText"] = "Tüm Ürünler Butonu Metni",
            ["Plugins.Widgets.FeaturedProducts.ViewAllUrl"] = "Tüm Ürünler Butonu Linki",
            ["Plugins.Widgets.FeaturedProducts.ProductCount"] = "Gösterilecek Ürün Sayısı",
        });
        await base.InstallAsync();
    }

    public override async Task UninstallAsync()
    {
        await _settingService.DeleteSettingAsync<FeaturedProductsSettings>();
        await _localizationService.DeleteLocaleResourcesAsync("Plugins.Widgets.FeaturedProducts");
        await base.UninstallAsync();
    }
}
