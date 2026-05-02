using Nop.Plugin.Widgets.ContactInfo.Components;
using Nop.Services.Cms;
using Nop.Services.Configuration;
using Nop.Services.Plugins;
using Nop.Web.Framework.Infrastructure;

namespace Nop.Plugin.Widgets.ContactInfo;

public class ContactInfoPlugin : BasePlugin, IWidgetPlugin
{
    private readonly ISettingService _settingService;

    public ContactInfoPlugin(ISettingService settingService)
    {
        _settingService = settingService;
    }

    public bool HideInWidgetList => false;

    public Type GetWidgetViewComponent(string widgetZone) => typeof(ContactInfoViewComponent);

    public Task<IList<string>> GetWidgetZonesAsync() =>
        Task.FromResult<IList<string>>(new List<string> { PublicWidgetZones.ContactUsTop });

    public override string GetConfigurationPageUrl() =>
        "/Admin/ContactInfo/Configure";

    public override async Task InstallAsync()
    {
        await _settingService.SaveSettingAsync(new ContactInfoSettings());
        await base.InstallAsync();
    }

    public override async Task UninstallAsync()
    {
        await _settingService.DeleteSettingAsync<ContactInfoSettings>();
        await base.UninstallAsync();
    }
}
