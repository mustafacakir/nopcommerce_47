// PEKIN_CUSTOM: StoreService override - admin panelinde tenant sadece kendi store'unu görür
using System.Security.Claims;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Stores;
using Nop.Data;
using Nop.Services.Stores;

namespace Nop.Web.Infrastructure.Services;

/// <summary>
/// Admin panelinde tenant admin'lerin sadece kendi store'larını görmesini sağlar.
/// RegisteredInStoreId = 0 → tüm mağazalar (super admin), > 0 → sadece o mağaza.
/// IWorkContext yerine HTTP claim'lerinden customer okunur (circular dependency engellenir).
/// </summary>
public class TenantStoreService : StoreService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IRepository<Customer> _customerRepository;

    public TenantStoreService(
        IRepository<Store> storeRepository,
        IHttpContextAccessor httpContextAccessor,
        IRepository<Customer> customerRepository) : base(storeRepository)
    {
        _httpContextAccessor = httpContextAccessor;
        _customerRepository = customerRepository;
    }

    public override async Task<IList<Store>> GetAllStoresAsync()
    {
        var allStores = await base.GetAllStoresAsync();

        var httpContext = _httpContextAccessor.HttpContext;
        if (httpContext == null || !httpContext.Request.Path.StartsWithSegments("/Admin"))
            return allStores;

        var emailClaim = httpContext.User?.FindFirst(ClaimTypes.Email)?.Value;
        if (string.IsNullOrEmpty(emailClaim))
            return allStores;

        var customers = await _customerRepository.GetAllAsync(q =>
            q.Where(c => c.Email == emailClaim && !c.Deleted));
        var customer = customers.FirstOrDefault();
        if (customer == null)
            return allStores;

        // RegisteredInStoreId = 0 → super admin, tüm mağazaları görebilir
        if (customer.RegisteredInStoreId == 0)
            return allStores;

        return allStores.Where(s => s.Id == customer.RegisteredInStoreId).ToList();
    }
}
