// PEKIN_CUSTOM: StoreService override - admin panelinde tenant sadece kendi store'unu görür
using Nop.Core;
using Nop.Core.Domain.Stores;
using Nop.Data;
using Nop.Services.Customers;
using Nop.Services.Stores;

namespace Nop.Web.Infrastructure.Services;

/// <summary>
/// Admin panelinde tenant admin'lerin sadece kendi store'larını görmesini sağlar.
/// IWorkContext constructor injection yerine RequestServices ile lazy resolve edilir
/// — böylece IStoreContext → IStoreService circular dependency engellenir.
/// </summary>
public class TenantStoreService : StoreService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    // Infinite recursion koruması: GetCurrentCustomerAsync içinde GetAllStoresAsync çağrılırsa
    private static readonly AsyncLocal<bool> _isResolving = new();

    public TenantStoreService(
        IRepository<Store> storeRepository,
        IHttpContextAccessor httpContextAccessor) : base(storeRepository)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public override async Task<IList<Store>> GetAllStoresAsync()
    {
        var allStores = await base.GetAllStoresAsync();

        // Recursive call koruması
        if (_isResolving.Value)
            return allStores;

        // Sadece admin panelinde filtrele
        var httpContext = _httpContextAccessor.HttpContext;
        if (httpContext == null || !httpContext.Request.Path.StartsWithSegments("/Admin"))
            return allStores;

        _isResolving.Value = true;
        try
        {
            var workContext = httpContext.RequestServices.GetService<IWorkContext>();
            var customerService = httpContext.RequestServices.GetService<ICustomerService>();

            if (workContext == null || customerService == null)
                return allStores;

            var currentCustomer = await workContext.GetCurrentCustomerAsync();

            // Super admin tüm store'ları görür
            if (await customerService.IsAdminAsync(currentCustomer))
                return allStores;

            // Tenant admin sadece kendi store'unu görür
            var registeredInStoreId = currentCustomer.RegisteredInStoreId;
            if (registeredInStoreId == 0)
                return allStores;

            return allStores.Where(s => s.Id == registeredInStoreId).ToList();
        }
        catch
        {
            return allStores;
        }
        finally
        {
            _isResolving.Value = false;
        }
    }
}
