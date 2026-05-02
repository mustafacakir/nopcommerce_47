using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Plugin.Widgets.ContactInfo.Models;
using Nop.Services.Configuration;
using Nop.Services.Security;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc.Filters;

namespace Nop.Plugin.Widgets.ContactInfo.Controllers;

[AuthorizeAdmin]
[Area(AreaNames.ADMIN)]
public class ContactInfoController : BasePluginController
{
    private readonly IPermissionService _permissionService;
    private readonly ISettingService _settingService;
    private readonly IStoreContext _storeContext;

    public ContactInfoController(
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
        if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageWidgets))
            return AccessDeniedView();

        var storeScope = await _storeContext.GetActiveStoreScopeConfigurationAsync();
        var settings = await _settingService.LoadSettingAsync<ContactInfoSettings>(storeScope);

        var model = new ConfigureModel
        {
            ActiveStoreScopeConfiguration = storeScope,
            Phone        = settings.Phone,
            WhatsApp     = settings.WhatsApp,
            Email        = settings.Email,
            Address      = settings.Address,
            MapEmbedUrl  = settings.MapEmbedUrl,
            FacebookUrl  = settings.FacebookUrl,
            InstagramUrl = settings.InstagramUrl,
            TwitterUrl   = settings.TwitterUrl,
            YoutubeUrl   = settings.YoutubeUrl,
        };

        if (storeScope > 0)
        {
            model.Phone_OverrideForStore        = await _settingService.SettingExistsAsync(settings, x => x.Phone, storeScope);
            model.WhatsApp_OverrideForStore      = await _settingService.SettingExistsAsync(settings, x => x.WhatsApp, storeScope);
            model.Email_OverrideForStore         = await _settingService.SettingExistsAsync(settings, x => x.Email, storeScope);
            model.Address_OverrideForStore       = await _settingService.SettingExistsAsync(settings, x => x.Address, storeScope);
            model.MapEmbedUrl_OverrideForStore   = await _settingService.SettingExistsAsync(settings, x => x.MapEmbedUrl, storeScope);
            model.FacebookUrl_OverrideForStore   = await _settingService.SettingExistsAsync(settings, x => x.FacebookUrl, storeScope);
            model.InstagramUrl_OverrideForStore  = await _settingService.SettingExistsAsync(settings, x => x.InstagramUrl, storeScope);
            model.TwitterUrl_OverrideForStore    = await _settingService.SettingExistsAsync(settings, x => x.TwitterUrl, storeScope);
            model.YoutubeUrl_OverrideForStore    = await _settingService.SettingExistsAsync(settings, x => x.YoutubeUrl, storeScope);
        }

        return View("~/Plugins/Widgets.ContactInfo/Views/Configure.cshtml", model);
    }

    [HttpPost]
    public async Task<IActionResult> Configure(ConfigureModel model)
    {
        if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageWidgets))
            return AccessDeniedView();

        var storeScope = await _storeContext.GetActiveStoreScopeConfigurationAsync();
        var settings = await _settingService.LoadSettingAsync<ContactInfoSettings>(storeScope);

        settings.Phone        = model.Phone;
        settings.WhatsApp     = model.WhatsApp;
        settings.Email        = model.Email;
        settings.Address      = model.Address;
        settings.MapEmbedUrl  = model.MapEmbedUrl;
        settings.FacebookUrl  = model.FacebookUrl;
        settings.InstagramUrl = model.InstagramUrl;
        settings.TwitterUrl   = model.TwitterUrl;
        settings.YoutubeUrl   = model.YoutubeUrl;

        await _settingService.SaveSettingOverridablePerStoreAsync(settings, x => x.Phone,        model.Phone_OverrideForStore,        storeScope, false);
        await _settingService.SaveSettingOverridablePerStoreAsync(settings, x => x.WhatsApp,     model.WhatsApp_OverrideForStore,      storeScope, false);
        await _settingService.SaveSettingOverridablePerStoreAsync(settings, x => x.Email,        model.Email_OverrideForStore,         storeScope, false);
        await _settingService.SaveSettingOverridablePerStoreAsync(settings, x => x.Address,      model.Address_OverrideForStore,       storeScope, false);
        await _settingService.SaveSettingOverridablePerStoreAsync(settings, x => x.MapEmbedUrl,  model.MapEmbedUrl_OverrideForStore,   storeScope, false);
        await _settingService.SaveSettingOverridablePerStoreAsync(settings, x => x.FacebookUrl,  model.FacebookUrl_OverrideForStore,   storeScope, false);
        await _settingService.SaveSettingOverridablePerStoreAsync(settings, x => x.InstagramUrl, model.InstagramUrl_OverrideForStore,  storeScope, false);
        await _settingService.SaveSettingOverridablePerStoreAsync(settings, x => x.TwitterUrl,   model.TwitterUrl_OverrideForStore,    storeScope, false);
        await _settingService.SaveSettingOverridablePerStoreAsync(settings, x => x.YoutubeUrl,   model.YoutubeUrl_OverrideForStore,    storeScope, false);

        await _settingService.ClearCacheAsync();

        return await Configure();
    }
}
