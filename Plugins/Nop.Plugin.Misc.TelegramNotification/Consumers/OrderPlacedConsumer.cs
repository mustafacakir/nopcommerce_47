using Nop.Core.Domain.Orders;
using Nop.Core.Events;
using Nop.Plugin.Misc.TelegramNotification.Services;
using Nop.Services.Common;
using Nop.Services.Configuration;
using Nop.Services.Customers;
using Nop.Services.Events;

namespace Nop.Plugin.Misc.TelegramNotification.Consumers;

public class OrderPlacedConsumer : IConsumer<OrderPlacedEvent>
{
    private readonly ISettingService _settingService;
    private readonly ICustomerService _customerService;
    private readonly IAddressService _addressService;
    private readonly TelegramService _telegramService;

    public OrderPlacedConsumer(
        ISettingService settingService,
        ICustomerService customerService,
        IAddressService addressService,
        TelegramService telegramService)
    {
        _settingService = settingService;
        _customerService = customerService;
        _addressService = addressService;
        _telegramService = telegramService;
    }

    public async Task HandleEventAsync(OrderPlacedEvent eventMessage)
    {
        var order = eventMessage.Order;
        var settings = await _settingService.LoadSettingAsync<TelegramNotificationSettings>(order.StoreId);

        if (!settings.Enabled)
            return;

        var customer = await _customerService.GetCustomerByIdAsync(order.CustomerId);
        var address = order.BillingAddressId > 0
            ? await _addressService.GetAddressByIdAsync(order.BillingAddressId)
            : null;

        var musteri = !string.IsNullOrEmpty(customer?.FirstName)
            ? $"{customer.FirstName} {customer.LastName}".Trim()
            : address != null && !string.IsNullOrEmpty(address.FirstName)
                ? $"{address.FirstName} {address.LastName}".Trim()
                : customer?.Email ?? "Misafir";

        var telefon = address?.PhoneNumber ?? string.Empty;
        var tutar = order.OrderTotal.ToString("N2") + " ₺";
        var siparisNo = order.CustomOrderNumber ?? order.Id.ToString();

        await _telegramService.SendAsync(settings, siparisNo, musteri, telefon, tutar);
    }
}
