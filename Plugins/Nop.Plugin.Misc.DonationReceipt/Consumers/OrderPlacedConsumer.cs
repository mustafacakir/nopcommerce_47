using Nop.Core.Domain.Messages;
using Nop.Core.Domain.Orders;
using Nop.Core.Events;
using Nop.Plugin.Misc.DonationReceipt.Services;
using Nop.Services.Catalog;
using Nop.Services.Customers;
using Nop.Services.Events;
using Nop.Services.Configuration;
using Nop.Services.Messages;
using Nop.Services.Orders;
using Nop.Services.Stores;

namespace Nop.Plugin.Misc.DonationReceipt.Consumers;

public class OrderPlacedConsumer : IConsumer<OrderPlacedEvent>
{
    private readonly IOrderService _orderService;
    private readonly IProductService _productService;
    private readonly ICustomerService _customerService;
    private readonly IStoreService _storeService;
    private readonly IEmailAccountService _emailAccountService;
    private readonly IQueuedEmailService _queuedEmailService;
    private readonly ISettingService _settingService;
    private readonly ReceiptService _receiptService;

    public OrderPlacedConsumer(
        IOrderService orderService,
        IProductService productService,
        ICustomerService customerService,
        IStoreService storeService,
        IEmailAccountService emailAccountService,
        IQueuedEmailService queuedEmailService,
        ISettingService settingService,
        ReceiptService receiptService)
    {
        _orderService = orderService;
        _productService = productService;
        _customerService = customerService;
        _storeService = storeService;
        _emailAccountService = emailAccountService;
        _queuedEmailService = queuedEmailService;
        _settingService = settingService;
        _receiptService = receiptService;
    }

    public async Task HandleEventAsync(OrderPlacedEvent eventMessage)
    {
        var order = eventMessage.Order;
        if (order == null)
            return;

        var customer = await _customerService.GetCustomerByIdAsync(order.CustomerId);
        if (customer == null || string.IsNullOrWhiteSpace(customer.Email))
            return;

        var items = await _orderService.GetOrderItemsAsync(order.Id);
        var productNames = new List<string>();
        foreach (var item in items)
        {
            var product = await _productService.GetProductByIdAsync(item.ProductId);
            productNames.Add(product?.Name ?? $"Bağış #{item.Id}");
        }

        var store = await _storeService.GetStoreByIdAsync(order.StoreId);

        var html = _receiptService.GenerateHtml(order, customer, items, productNames, store);

        var defaultEmailAccountId = await _settingService.GetSettingByKeyAsync<int>("emailaccountsettings.defaultemailaccountid");
        var emailAccount = await _emailAccountService.GetEmailAccountByIdAsync(defaultEmailAccountId)
                           ?? (await _emailAccountService.GetAllEmailAccountsAsync()).FirstOrDefault();
        if (emailAccount == null)
            return;

        var storeName = store?.Name ?? "Bağış Platformu";
        await _queuedEmailService.InsertQueuedEmailAsync(new QueuedEmail
        {
            Priority = QueuedEmailPriority.High,
            From = emailAccount.Email,
            FromName = storeName,
            To = customer.Email,
            ToName = $"{customer.FirstName} {customer.LastName}".Trim(),
            Subject = $"Bağış Belgeniz – {storeName}",
            Body = html,
            EmailAccountId = emailAccount.Id,
            CreatedOnUtc = DateTime.UtcNow
        });
    }
}
