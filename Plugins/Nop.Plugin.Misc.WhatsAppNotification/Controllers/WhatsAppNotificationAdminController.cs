using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Plugin.Misc.WhatsAppNotification.Models;
using Nop.Services.Common;
using Nop.Services.Configuration;
using Nop.Services.Customers;
using Nop.Services.Orders;
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
    private readonly IOrderService _orderService;
    private readonly ICustomerService _customerService;
    private readonly IAddressService _addressService;

    public WhatsAppNotificationController(
        IPermissionService permissionService,
        ISettingService settingService,
        IStoreContext storeContext,
        IOrderService orderService,
        ICustomerService customerService,
        IAddressService addressService)
    {
        _permissionService = permissionService;
        _settingService = settingService;
        _storeContext = storeContext;
        _orderService = orderService;
        _customerService = customerService;
        _addressService = addressService;
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

    public async Task<IActionResult> Orders(int days = 7)
    {
        if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageOrders))
            return AccessDeniedView();

        var from = DateTime.UtcNow.AddDays(-days);
        var orders = await _orderService.SearchOrdersAsync(createdFromUtc: from, pageSize: 200);

        var model = new List<OrderWhatsAppModel>();
        foreach (var order in orders)
        {
            var customer = await _customerService.GetCustomerByIdAsync(order.CustomerId);
            var musteriAd = !string.IsNullOrEmpty(customer?.FirstName)
                ? $"{customer.FirstName} {customer.LastName}".Trim()
                : customer?.Email ?? "Misafir";

            var telefon = string.Empty;
            if (order.BillingAddressId > 0)
            {
                var address = await _addressService.GetAddressByIdAsync(order.BillingAddressId);
                telefon = address?.PhoneNumber ?? string.Empty;
            }

            model.Add(new OrderWhatsAppModel
            {
                OrderId = order.Id,
                CustomOrderNumber = order.CustomOrderNumber ?? order.Id.ToString(),
                MusteriAd = musteriAd,
                MusteriTelefon = telefon,
                WhatsAppPhone = FormatWhatsAppPhone(telefon),
                OrderTotal = order.OrderTotal.ToString("N2") + " ₺",
                CreatedOn = order.CreatedOnUtc.ToLocalTime().ToString("dd.MM.yyyy HH:mm")
            });
        }

        ViewBag.Days = days;
        return View("~/Plugins/Misc.WhatsAppNotification/Views/Orders.cshtml", model);
    }

    private static string FormatWhatsAppPhone(string phone)
    {
        if (string.IsNullOrWhiteSpace(phone))
            return string.Empty;

        var digits = new string(phone.Where(char.IsDigit).ToArray());

        if (digits.StartsWith("90") && digits.Length == 12)
            return digits;
        if (digits.StartsWith("0") && digits.Length == 11)
            return "90" + digits[1..];
        if (digits.Length == 10)
            return "90" + digits;

        return digits;
    }
}
