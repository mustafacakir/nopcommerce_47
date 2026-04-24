using Nop.Core;
using Nop.Plugin.Widgets.CategoryShowcase.Components;
using Nop.Services.Cms;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Plugins;
using Nop.Web.Framework.Infrastructure;

namespace Nop.Plugin.Widgets.CategoryShowcase;

public class CategoryShowcasePlugin : BasePlugin, IWidgetPlugin
{
    private readonly ILocalizationService _localizationService;
    private readonly ISettingService _settingService;
    private readonly IWebHelper _webHelper;

    public CategoryShowcasePlugin(ILocalizationService localizationService, ISettingService settingService, IWebHelper webHelper)
    {
        _localizationService = localizationService;
        _settingService = settingService;
        _webHelper = webHelper;
    }

    public bool HideInWidgetList => false;

    public Type GetWidgetViewComponent(string widgetZone) => typeof(CategoryShowcaseViewComponent);

    public Task<IList<string>> GetWidgetZonesAsync()
    {
        return Task.FromResult<IList<string>>(new List<string> { PublicWidgetZones.HomepageTop });
    }

    public override string GetConfigurationPageUrl()
    {
        return $"{_webHelper.GetStoreLocation()}Admin/WidgetsCategoryShowcase/Configure";
    }

    public override async Task InstallAsync()
    {
        await _settingService.SaveSettingAsync(new CategoryShowcaseSettings());
        await _localizationService.AddOrUpdateLocaleResourceAsync(new Dictionary<string, string>
        {
            ["Plugins.Widgets.CategoryShowcase.SectionBadge"] = "Bölüm Rozeti",
            ["Plugins.Widgets.CategoryShowcase.SectionTitle"] = "Bölüm Başlığı",
            ["Plugins.Widgets.CategoryShowcase.SectionSubtitle"] = "Bölüm Alt Başlığı",
            ["Plugins.Widgets.CategoryShowcase.Card1Title"] = "Kart 1 Başlık",
            ["Plugins.Widgets.CategoryShowcase.Card1Description"] = "Kart 1 Açıklama",
            ["Plugins.Widgets.CategoryShowcase.Card1Badge"] = "Kart 1 Rozet",
            ["Plugins.Widgets.CategoryShowcase.Card1ImageUrl"] = "Kart 1 Görsel URL",
            ["Plugins.Widgets.CategoryShowcase.Card1Url"] = "Kart 1 Link",
            ["Plugins.Widgets.CategoryShowcase.Card2Title"] = "Kart 2 Başlık",
            ["Plugins.Widgets.CategoryShowcase.Card2Description"] = "Kart 2 Açıklama",
            ["Plugins.Widgets.CategoryShowcase.Card2Badge"] = "Kart 2 Rozet",
            ["Plugins.Widgets.CategoryShowcase.Card2ImageUrl"] = "Kart 2 Görsel URL",
            ["Plugins.Widgets.CategoryShowcase.Card2Url"] = "Kart 2 Link",
        });
        await base.InstallAsync();
    }

    public override async Task UninstallAsync()
    {
        await _settingService.DeleteSettingAsync<CategoryShowcaseSettings>();
        await _localizationService.DeleteLocaleResourcesAsync("Plugins.Widgets.CategoryShowcase");
        await base.UninstallAsync();
    }
}
