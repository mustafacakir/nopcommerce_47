// PEKIN_CUSTOM: Admin panelinde StoreOwner kullanıcıları sadece kendi mağazalarını görür ve kaydeder
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Nop.Core;
using Nop.Services.Customers;

namespace Nop.Web.Areas.Admin.Filters;

/// <summary>
/// Admin panelinde tenant (StoreOwner) kullanıcıları için otomatik store filtrelemesi yapar:
/// - Listing: SearchStoreId otomatik set edilir (DB seviyesinde filtreleme)
/// - Create/Edit: SelectedStoreIds ve LimitedToStores otomatik set edilir
/// - UI: ViewBag.OwnerStoreId set edilir, JS ile store seçici gizlenir
/// </summary>
public class StoreOwnerFilter : IAsyncActionFilter
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public StoreOwnerFilter(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var httpContext = _httpContextAccessor.HttpContext;
        if (httpContext == null)
        {
            await next();
            return;
        }

        var workContext = httpContext.RequestServices.GetService<IWorkContext>();
        var customerService = httpContext.RequestServices.GetService<ICustomerService>();

        if (workContext == null || customerService == null)
        {
            await next();
            return;
        }

        var currentCustomer = await workContext.GetCurrentCustomerAsync();

        // Admin tüm store'ları görebilir, filtreleme yapma
        if (await customerService.IsAdminAsync(currentCustomer))
        {
            await next();
            return;
        }

        var storeId = currentCustomer.RegisteredInStoreId;
        if (storeId == 0)
        {
            await next();
            return;
        }

        // ViewBag'e store ID'yi set et (JS ile UI gizlemek için)
        if (context.Controller is Controller controller)
            controller.ViewBag.OwnerStoreId = storeId;

        // Action argument'larını kontrol et ve store ID'yi set et
        foreach (var arg in context.ActionArguments.Values)
        {
            if (arg == null) continue;
            var type = arg.GetType();

            // Listing: SearchStoreId veya StoreId — DB seviyesinde filtreleme
            foreach (var propName in new[] { "SearchStoreId", "StoreId" })
            {
                var searchStoreProp = type.GetProperty(propName);
                if (searchStoreProp != null && searchStoreProp.CanWrite && searchStoreProp.PropertyType == typeof(int))
                    searchStoreProp.SetValue(arg, storeId);
            }

            // Create/Edit: SelectedStoreIds + LimitedToStores
            var selectedStoresProp = type.GetProperty("SelectedStoreIds");
            if (selectedStoresProp != null && selectedStoresProp.CanWrite)
                selectedStoresProp.SetValue(arg, new List<int> { storeId });

            var limitedToStoresProp = type.GetProperty("LimitedToStores");
            if (limitedToStoresProp != null && limitedToStoresProp.CanWrite)
                limitedToStoresProp.SetValue(arg, true);
        }

        await next();
    }
}
