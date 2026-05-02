using Nop.Core.Domain.Orders;
using Nop.Core.Events;
using Nop.Plugin.Misc.WhatsAppNotification.Services;
using Nop.Services.Configuration;
using Nop.Services.Customers;
using Nop.Services.Stores;

namespace Nop.Plugin.Misc.WhatsAppNotification.Consumers;

public class OrderPlacedConsumer : IConsumer<OrderPlacedEvent>
{
    private readonly ISettingService _settingService;
    private readonly ICustomerService _customerService;
    private readonly IStoreService _storeService;
    private readonly WhatsAppService _whatsAppService;

    public OrderPlacedConsumer(
        ISettingService settingService,
        ICustomerService customerService,
        IStoreService storeService,
        WhatsAppService whatsAppService)
    {
        _settingService = settingService;
        _customerService = customerService;
        _storeService = storeService;
        _whatsAppService = whatsAppService;
    }

    public async Task HandleEventAsync(OrderPlacedEvent eventMessage)
    {
        var order = eventMessage.Order;
        var settings = await _settingService.LoadSettingAsync<WhatsAppNotificationSettings>(order.StoreId);

        if (!settings.Enabled)
            return;

        var customer = await _customerService.GetCustomerByIdAsync(order.CustomerId);
        var store = await _storeService.GetStoreByIdAsync(order.StoreId);

        var customerName = !string.IsNullOrEmpty(customer?.FirstName)
            ? $"{customer.FirstName} {customer.LastName}".Trim()
            : customer?.Email ?? "Misafir";

        var orderTotal = order.OrderTotal.ToString("N2") + " " + (await GetCurrencySymbolAsync(order.CustomerCurrencyCode));

        await _whatsAppService.SendOrderNotificationAsync(
            settings,
            orderId: order.CustomOrderNumber ?? order.Id.ToString(),
            customerName: customerName,
            orderTotal: orderTotal,
            storeName: store?.Name ?? string.Empty);
    }

    private async Task<string> GetCurrencySymbolAsync(string currencyCode)
    {
        return currencyCode switch
        {
            "TRY" => "₺",
            "USD" => "$",
            "EUR" => "€",
            _ => currencyCode ?? string.Empty
        };
    }
}
