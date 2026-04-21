using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Plugin.Widgets.DonationTransparency.Models;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Messages;
using Nop.Services.Security;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;

namespace Nop.Plugin.Widgets.DonationTransparency.Controllers;

[Area(AreaNames.ADMIN)]
[AutoValidateAntiforgeryToken]
public class WidgetsDonationTransparencyController : BasePluginController
{
    private readonly ILocalizationService _localizationService;
    private readonly INotificationService _notificationService;
    private readonly IPermissionService _permissionService;
    private readonly ISettingService _settingService;
    private readonly IStoreContext _storeContext;

    public WidgetsDonationTransparencyController(ILocalizationService localizationService, INotificationService notificationService,
        IPermissionService permissionService, ISettingService settingService, IStoreContext storeContext)
    {
        _localizationService = localizationService;
        _notificationService = notificationService;
        _permissionService = permissionService;
        _settingService = settingService;
        _storeContext = storeContext;
    }

    public async Task<IActionResult> Configure()
    {
        if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageWidgets))
            return AccessDeniedView();

        var storeScope = await _storeContext.GetActiveStoreScopeConfigurationAsync();
        var s = await _settingService.LoadSettingAsync<DonationTransparencySettings>(storeScope);

        return View("~/Plugins/Widgets.DonationTransparency/Views/Configure.cshtml", new ConfigurationModel
        {
            SectionTitle = s.SectionTitle, SectionSubtitle = s.SectionSubtitle,
            ReportLinkText = s.ReportLinkText, ReportLinkUrl = s.ReportLinkUrl,
            Item1Label = s.Item1Label, Item1Percent = s.Item1Percent, Item1Color = s.Item1Color, Item1Icon = s.Item1Icon, Item1Description = s.Item1Description,
            Item2Label = s.Item2Label, Item2Percent = s.Item2Percent, Item2Color = s.Item2Color, Item2Icon = s.Item2Icon, Item2Description = s.Item2Description,
            Item3Label = s.Item3Label, Item3Percent = s.Item3Percent, Item3Color = s.Item3Color, Item3Icon = s.Item3Icon, Item3Description = s.Item3Description,
            Item4Label = s.Item4Label, Item4Percent = s.Item4Percent, Item4Color = s.Item4Color, Item4Icon = s.Item4Icon, Item4Description = s.Item4Description,
            Item5Label = s.Item5Label, Item5Percent = s.Item5Percent, Item5Color = s.Item5Color, Item5Icon = s.Item5Icon, Item5Description = s.Item5Description,
            ActiveStoreScopeConfiguration = storeScope
        });
    }

    [HttpPost]
    public async Task<IActionResult> Configure(ConfigurationModel model)
    {
        if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageWidgets))
            return AccessDeniedView();

        var storeScope = await _storeContext.GetActiveStoreScopeConfigurationAsync();
        var s = await _settingService.LoadSettingAsync<DonationTransparencySettings>(storeScope);

        s.SectionTitle = model.SectionTitle; s.SectionSubtitle = model.SectionSubtitle;
        s.ReportLinkText = model.ReportLinkText; s.ReportLinkUrl = model.ReportLinkUrl;
        s.Item1Label = model.Item1Label; s.Item1Percent = model.Item1Percent; s.Item1Color = model.Item1Color; s.Item1Icon = model.Item1Icon; s.Item1Description = model.Item1Description;
        s.Item2Label = model.Item2Label; s.Item2Percent = model.Item2Percent; s.Item2Color = model.Item2Color; s.Item2Icon = model.Item2Icon; s.Item2Description = model.Item2Description;
        s.Item3Label = model.Item3Label; s.Item3Percent = model.Item3Percent; s.Item3Color = model.Item3Color; s.Item3Icon = model.Item3Icon; s.Item3Description = model.Item3Description;
        s.Item4Label = model.Item4Label; s.Item4Percent = model.Item4Percent; s.Item4Color = model.Item4Color; s.Item4Icon = model.Item4Icon; s.Item4Description = model.Item4Description;
        s.Item5Label = model.Item5Label; s.Item5Percent = model.Item5Percent; s.Item5Color = model.Item5Color; s.Item5Icon = model.Item5Icon; s.Item5Description = model.Item5Description;

        await _settingService.SaveSettingAsync(s, storeScope);
        await _settingService.ClearCacheAsync();
        _notificationService.SuccessNotification(await _localizationService.GetResourceAsync("Admin.Plugins.Saved"));

        return await Configure();
    }
}
