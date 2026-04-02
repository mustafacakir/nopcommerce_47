using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Core;
using Nop.Core.Domain.Stores;
using Nop.Services.Stores;
using Nop.Web.Framework.Factories;
using Nop.Web.Framework.Models;

namespace Nop.Web.Infrastructure.Services;

/// <summary>
/// Tenant admin sadece tek mağazaya sahip olduğunda AvailableStores listesini boş döndürür.
/// Bu sayede tüm admin view'larındaki SelectionIsNotPossible() kontrolü otomatik olarak
/// store seçim alanlarını gizler — her sayfayı tek tek düzenlemeye gerek kalmaz.
/// </summary>
public class TenantStoreMappingSupportedModelFactory : StoreMappingSupportedModelFactory
{
    public TenantStoreMappingSupportedModelFactory(
        IStoreMappingService storeMappingService,
        IStoreService storeService) : base(storeMappingService, storeService)
    {
    }

    public override async Task PrepareModelStoresAsync<TModel>(TModel model)
    {
        await base.PrepareModelStoresAsync(model);

        // Seçim yapılamıyorsa (tek store) listeyi boşalt — view'lar SelectionIsNotPossible() ile gizler
        if (model.AvailableStores.Count(x => !x.Value.Equals("0")) < 2)
            model.AvailableStores = new List<SelectListItem>();
    }
}
