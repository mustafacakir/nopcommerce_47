using Nop.Core.Domain.Orders;
using Nop.Core.Events;
using Nop.Plugin.Misc.KurbanOrganization.Domain;
using Nop.Plugin.Misc.KurbanOrganization.Services;
using Nop.Services.Catalog;
using Nop.Services.Common;
using Nop.Services.Configuration;
using Nop.Services.Customers;
using Nop.Services.Events;
using Nop.Services.Orders;

namespace Nop.Plugin.Misc.KurbanOrganization.Consumers;

public class OrderPlacedConsumer : IConsumer<OrderPlacedEvent>
{
    private readonly ISettingService _settingService;
    private readonly IOrderService _orderService;
    private readonly IProductService _productService;
    private readonly ICategoryService _categoryService;
    private readonly ICustomerService _customerService;
    private readonly IAddressService _addressService;
    private readonly KurbanService _kurbanService;

    public OrderPlacedConsumer(
        ISettingService settingService,
        IOrderService orderService,
        IProductService productService,
        ICategoryService categoryService,
        ICustomerService customerService,
        IAddressService addressService,
        KurbanService kurbanService)
    {
        _settingService = settingService;
        _orderService = orderService;
        _productService = productService;
        _categoryService = categoryService;
        _customerService = customerService;
        _addressService = addressService;
        _kurbanService = kurbanService;
    }

    public async Task HandleEventAsync(OrderPlacedEvent eventMessage)
    {
        var order = eventMessage.Order;
        var settings = await _settingService.LoadSettingAsync<KurbanOrganizationSettings>(order.StoreId);

        if (!settings.Enabled || settings.KurbanCategoryId == 0)
            return;

        var items = await _orderService.GetOrderItemsAsync(order.Id);
        var customer = await _customerService.GetCustomerByIdAsync(order.CustomerId);

        var musteriAd = string.IsNullOrEmpty(customer?.FirstName)
            ? customer?.Email ?? string.Empty
            : $"{customer.FirstName} {customer.LastName}".Trim();

        var musteriTelefon = string.Empty;
        if (order.BillingAddressId > 0)
        {
            var address = await _addressService.GetAddressByIdAsync(order.BillingAddressId);
            musteriTelefon = address?.PhoneNumber ?? string.Empty;
        }

        foreach (var item in items)
        {
            var product = await _productService.GetProductByIdAsync(item.ProductId);
            if (product == null) continue;

            var categories = await _categoryService.GetProductCategoriesByProductIdAsync(item.ProductId);
            var isKurban = categories.Any(c => c.CategoryId == settings.KurbanCategoryId);
            if (!isKurban) continue;

            var hisseKodu = await _kurbanService.GenerateHisseKoduAsync();

            var hisse = new KurbanHisse
            {
                OrderId = order.Id,
                OrderItemId = item.Id,
                CustomerId = order.CustomerId,
                HisseKodu = hisseKodu,
                KurbanTuru = product.Name,
                HisseSayisi = item.Quantity,
                Kesildi = false,
                BildirimGonderildi = false,
                MusteriAd = musteriAd,
                MusteriTelefon = musteriTelefon,
                CreatedOnUtc = DateTime.UtcNow
            };

            await _kurbanService.InsertHisseAsync(hisse);
        }
    }
}
