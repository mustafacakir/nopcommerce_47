using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Plugin.Misc.TelegramNotification.Models;
using Nop.Services.Configuration;
using Nop.Services.Security;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc.Filters;

namespace Nop.Plugin.Misc.TelegramNotification.Controllers;

[AuthorizeAdmin]
[Area(AreaNames.ADMIN)]
public class TelegramNotificationController : BasePluginController
{
    private readonly IPermissionService _permissionService;
    private readonly ISettingService _settingService;
    private readonly IStoreContext _storeContext;

    public TelegramNotificationController(
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
        if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageOrders))
            return AccessDeniedView();

        var storeScope = await _storeContext.GetActiveStoreScopeConfigurationAsync();
        var settings = await _settingService.LoadSettingAsync<TelegramNotificationSettings>(storeScope);

        var model = new ConfigureModel
        {
            ActiveStoreScopeConfiguration = storeScope,
            Enabled         = settings.Enabled,
            BotToken        = settings.BotToken,
            ChatId          = settings.ChatId,
            MessageTemplate = settings.MessageTemplate,
        };

        if (storeScope > 0)
        {
            model.Enabled_OverrideForStore         = await _settingService.SettingExistsAsync(settings, x => x.Enabled, storeScope);
            model.BotToken_OverrideForStore         = await _settingService.SettingExistsAsync(settings, x => x.BotToken, storeScope);
            model.ChatId_OverrideForStore           = await _settingService.SettingExistsAsync(settings, x => x.ChatId, storeScope);
            model.MessageTemplate_OverrideForStore  = await _settingService.SettingExistsAsync(settings, x => x.MessageTemplate, storeScope);
        }

        return View("~/Plugins/Misc.TelegramNotification/Views/Configure.cshtml", model);
    }

    [HttpPost]
    public async Task<IActionResult> Configure(ConfigureModel model)
    {
        if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageOrders))
            return AccessDeniedView();

        var storeScope = await _storeContext.GetActiveStoreScopeConfigurationAsync();
        var settings = await _settingService.LoadSettingAsync<TelegramNotificationSettings>(storeScope);

        settings.Enabled         = model.Enabled;
        settings.BotToken        = model.BotToken;
        settings.ChatId          = model.ChatId;
        settings.MessageTemplate = model.MessageTemplate;

        await _settingService.SaveSettingOverridablePerStoreAsync(settings, x => x.Enabled,         model.Enabled_OverrideForStore,        storeScope, false);
        await _settingService.SaveSettingOverridablePerStoreAsync(settings, x => x.BotToken,         model.BotToken_OverrideForStore,        storeScope, false);
        await _settingService.SaveSettingOverridablePerStoreAsync(settings, x => x.ChatId,           model.ChatId_OverrideForStore,          storeScope, false);
        await _settingService.SaveSettingOverridablePerStoreAsync(settings, x => x.MessageTemplate,  model.MessageTemplate_OverrideForStore, storeScope, false);

        await _settingService.ClearCacheAsync();

        return await Configure();
    }
}
