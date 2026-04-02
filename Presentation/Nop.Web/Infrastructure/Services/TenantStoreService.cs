using Nop.Core;
using Nop.Core.Domain.Stores;
using Nop.Data;
using Nop.Services.Customers;
using Nop.Services.Stores;

namespace Nop.Web.Infrastructure.Services;

/// <summary>
/// Admin panelinde tenant admin'lerin sadece kendi store'larını görmesini sağlar.
/// Public store routing etkilenmez — sadece /Admin/ path'inde filtreleme yapılır.
/// </summary>
public class TenantStoreService : StoreService
{
    private readonly IWorkContext _workContext;
    private readonly ICustomerService _customerService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public TenantStoreService(
        IRepository<Store> storeRepository,
        IWorkContext workContext,
        ICustomerService customerService,
        IHttpContextAccessor httpContextAccessor) : base(storeRepository)
    {
        _workContext = workContext;
        _customerService = customerService;
        _httpContextAccessor = httpContextAccessor;
    }

    public override async Task<IList<Store>> GetAllStoresAsync()
    {
        var allStores = await base.GetAllStoresAsync();

        // Sadece admin panelinde filtrele — public store routing etkilenmesin
        var httpContext = _httpContextAccessor.HttpContext;
        if (httpContext == null || !httpContext.Request.Path.StartsWithSegments("/Admin"))
            return allStores;

        var currentCustomer = await _workContext.GetCurrentCustomerAsync();

        // Super admin tüm store'ları görür
        if (await _customerService.IsAdminAsync(currentCustomer))
            return allStores;

        // Tenant admin sadece kendi store'unu görür
        var registeredInStoreId = currentCustomer.RegisteredInStoreId;
        if (registeredInStoreId == 0)
            return allStores;

        return allStores.Where(s => s.Id == registeredInStoreId).ToList();
    }
}
