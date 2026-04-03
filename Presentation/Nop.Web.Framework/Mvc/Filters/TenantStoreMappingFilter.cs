using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Nop.Services.Customers;
using Nop.Core;

namespace Nop.Web.Framework.Mvc.Filters;

/// <summary>
/// StoreOwner rolündeki kullanıcıların oluşturduğu/güncellediği tüm entity'leri
/// otomatik olarak kendi mağazalarıyla ilişkilendirir.
/// </summary>
public class TenantStoreMappingFilter : IAsyncActionFilter
{
    private readonly IWorkContext _workContext;
    private readonly ICustomerService _customerService;

    public TenantStoreMappingFilter(IWorkContext workContext, ICustomerService customerService)
    {
        _workContext = workContext;
        _customerService = customerService;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var area = context.RouteData.Values["area"]?.ToString();
        if (context.HttpContext.Request.Method == HttpMethods.Post &&
            string.Equals(area, "Admin", StringComparison.OrdinalIgnoreCase))
        {
            var customer = await _workContext.GetCurrentCustomerAsync();
            var isStoreOwner = await _customerService.IsInCustomerRoleAsync(customer, "StoreOwner");

            if (isStoreOwner && customer.RegisteredInStoreId > 0)
            {
                var storeId = customer.RegisteredInStoreId;

                foreach (var arg in context.ActionArguments.Values)
                {
                    if (arg == null) continue;

                    var prop = arg.GetType().GetProperty("SelectedStoreIds");
                    if (prop == null) continue;

                    var current = prop.GetValue(arg) as IEnumerable<int> ?? Enumerable.Empty<int>();
                    var ids = current.ToList();
                    if (!ids.Contains(storeId))
                        ids.Add(storeId);

                    prop.SetValue(arg, ids);
                }
            }
        }

        await next();
    }
}
