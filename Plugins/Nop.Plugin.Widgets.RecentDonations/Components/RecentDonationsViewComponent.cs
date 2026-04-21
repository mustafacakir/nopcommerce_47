using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Core.Domain.Orders;
using Nop.Plugin.Widgets.RecentDonations.Models;
using Nop.Services.Catalog;
using Nop.Services.Common;
using Nop.Services.Configuration;
using Nop.Services.Orders;
using Nop.Web.Framework.Components;

namespace Nop.Plugin.Widgets.RecentDonations.Components;

public class RecentDonationsViewComponent : NopViewComponent
{
    private readonly ISettingService _settingService;
    private readonly IStoreContext _storeContext;
    private readonly IOrderService _orderService;
    private readonly IAddressService _addressService;
    private readonly IProductService _productService;

    public RecentDonationsViewComponent(ISettingService settingService, IStoreContext storeContext,
        IOrderService orderService, IAddressService addressService, IProductService productService)
    {
        _settingService = settingService;
        _storeContext = storeContext;
        _orderService = orderService;
        _addressService = addressService;
        _productService = productService;
    }

    public async Task<IViewComponentResult> InvokeAsync(string widgetZone, object? additionalData = null)
    {
        var store = await _storeContext.GetCurrentStoreAsync();
        var s = await _settingService.LoadSettingAsync<RecentDonationsSettings>(store.Id);

        var orders = await _orderService.SearchOrdersAsync(
            storeId: store.Id,
            osIds: new List<int> { (int)OrderStatus.Complete },
            pageSize: s.ItemCount);

        var items = new List<DonationItem>();
        foreach (var order in orders)
        {
            var donorName = "Bağışçı";
            if (order.BillingAddressId > 0)
            {
                var address = await _addressService.GetAddressByIdAsync(order.BillingAddressId);
                if (address != null)
                {
                    var first = address.FirstName?.Trim() ?? "Bağışçı";
                    var last = address.LastName?.Trim();
                    donorName = string.IsNullOrEmpty(last) ? first : $"{first} {last[0]}.";
                }
            }

            var productName = string.Empty;
            if (s.ShowProductName)
            {
                var orderItems = await _orderService.GetOrderItemsAsync(order.Id);
                var firstItem = orderItems.FirstOrDefault();
                if (firstItem != null)
                {
                    var product = await _productService.GetProductByIdAsync(firstItem.ProductId);
                    productName = product?.Name ?? string.Empty;
                }
            }

            items.Add(new DonationItem
            {
                DonorName = donorName,
                ProductName = productName,
                Amount = s.ShowAmount ? order.OrderTotal.ToString("N0") + "₺" : string.Empty,
                TimeAgo = GetTimeAgo(order.CreatedOnUtc)
            });
        }

        return View("~/Plugins/Widgets.RecentDonations/Views/Components/RecentDonations/Default.cshtml",
            new PublicInfoModel { Items = items, FallbackText = s.FallbackText });
    }

    private static string GetTimeAgo(DateTime createdUtc)
    {
        var diff = DateTime.UtcNow - createdUtc;
        if (diff.TotalMinutes < 60) return (int)diff.TotalMinutes + " dk önce";
        if (diff.TotalHours < 24) return (int)diff.TotalHours + " saat önce";
        if (diff.TotalDays < 30) return (int)diff.TotalDays + " gün önce";
        return (int)(diff.TotalDays / 30) + " ay önce";
    }
}
