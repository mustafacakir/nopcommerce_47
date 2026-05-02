using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Plugin.Misc.WhatsAppNotification.Models;
using Nop.Services.Configuration;
using Nop.Services.Security;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc.Filters;

namespace Nop.Plugin.Misc.WhatsAppNotification.Controllers;

[AuthorizeAdmin]
[Area(AreaNames.ADMIN)]
public class WhatsAppNotificationController : BasePluginController
{
    private readonly IPermissionService _permissionService;
    private readonly ISettingService _settingService;
    private readonly IStoreContext _storeContext;

    public WhatsAppNotificationController(
        IPermissionService permissionService,
        ISettingService settingService,
        IStoreContext storeContext)
    {
        _permissionService = permissionService;
        _settingService = settingService;
        _storeContext = storeContext;
    }

    public async Task<IActionResult> Configure()
    {
        if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManagePlugins))
            return AccessDeniedView();

        var storeScope = await _storeContext.GetActiveStoreScopeConfigurationAsync();
        var settings = await _settingService.LoadSettingAsync<WhatsAppNotificationSettings>(storeScope);

        var model = new ConfigureModel
        {
            ActiveStoreScopeConfiguration = storeScope,
            Enabled          = settings.Enabled,
            AccessToken      = settings.AccessToken,
            PhoneNumberId    = settings.PhoneNumberId,
            RecipientPhone   = settings.RecipientPhone,
            TemplateName     = settings.TemplateName,
            TemplateLanguage = settings.TemplateLanguage,
        };

        if (storeScope > 0)
        {
            model.Enabled_OverrideForStore          = await _settingService.SettingExistsAsync(settings, x => x.Enabled, storeScope);
            model.AccessToken_OverrideForStore      = await _settingService.SettingExistsAsync(settings, x => x.AccessToken, storeScope);
            model.PhoneNumberId_OverrideForStore    = await _settingService.SettingExistsAsync(settings, x => x.PhoneNumberId, storeScope);
            model.RecipientPhone_OverrideForStore   = await _settingService.SettingExistsAsync(settings, x => x.RecipientPhone, storeScope);
            model.TemplateName_OverrideForStore     = await _settingService.SettingExistsAsync(settings, x => x.TemplateName, storeScope);
            model.TemplateLanguage_OverrideForStore = await _settingService.SettingExistsAsync(settings, x => x.TemplateLanguage, storeScope);
        }

        return View("~/Plugins/Misc.WhatsAppNotification/Views/Configure.cshtml", model);
    }

    [HttpPost]
    public async Task<IActionResult> Configure(ConfigureModel model)
    {
        if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManagePlugins))
            return AccessDeniedView();

        var storeScope = await _storeContext.GetActiveStoreScopeConfigurationAsync();
        var settings = await _settingService.LoadSettingAsync<WhatsAppNotificationSettings>(storeScope);

        settings.Enabled          = model.Enabled;
        settings.AccessToken      = model.AccessToken;
        settings.PhoneNumberId    = model.PhoneNumberId;
        settings.RecipientPhone   = model.RecipientPhone;
        settings.TemplateName     = model.TemplateName;
        settings.TemplateLanguage = model.TemplateLanguage;

        await _settingService.SaveSettingOverridablePerStoreAsync(settings, x => x.Enabled,          model.Enabled_OverrideForStore,          storeScope, false);
        await _settingService.SaveSettingOverridablePerStoreAsync(settings, x => x.AccessToken,      model.AccessToken_OverrideForStore,      storeScope, false);
        await _settingService.SaveSettingOverridablePerStoreAsync(settings, x => x.PhoneNumberId,    model.PhoneNumberId_OverrideForStore,    storeScope, false);
        await _settingService.SaveSettingOverridablePerStoreAsync(settings, x => x.RecipientPhone,   model.RecipientPhone_OverrideForStore,   storeScope, false);
        await _settingService.SaveSettingOverridablePerStoreAsync(settings, x => x.TemplateName,     model.TemplateName_OverrideForStore,     storeScope, false);
        await _settingService.SaveSettingOverridablePerStoreAsync(settings, x => x.TemplateLanguage, model.TemplateLanguage_OverrideForStore, storeScope, false);

        await _settingService.ClearCacheAsync();

        return await Configure();
    }
}
