// PEKIN_CUSTOM: StoreOwner için ürün niteliği sayfasındaki ürün listesini store'a göre filtreler
using Nop.Core;
using Nop.Core.Domain.Catalog;
using Nop.Services.Catalog;
using Nop.Services.Customers;
using Nop.Services.Localization;
using Nop.Services.Stores;
using Nop.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using Nop.Web.Areas.Admin.Models.Catalog;
using Nop.Web.Framework.Factories;
using Nop.Web.Framework.Models.Extensions;

namespace Nop.Web.Areas.Admin.Factories;

public class TenantProductAttributeModelFactory : ProductAttributeModelFactory
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IStoreMappingService _storeMappingService;

    public TenantProductAttributeModelFactory(
        IHttpContextAccessor httpContextAccessor,
        IStoreMappingService storeMappingService,
        ILocalizationService localizationService,
        ILocalizedModelFactory localizedModelFactory,
        IProductAttributeService productAttributeService,
        IProductService productService) : base(localizationService, localizedModelFactory, productAttributeService, productService)
    {
        _httpContextAccessor = httpContextAccessor;
        _storeMappingService = storeMappingService;
    }

    public override async Task<ProductAttributeProductListModel> PrepareProductAttributeProductListModelAsync(
        ProductAttributeProductSearchModel searchModel, ProductAttribute productAttribute)
    {
        var model = await base.PrepareProductAttributeProductListModelAsync(searchModel, productAttribute);

        var httpContext = _httpContextAccessor.HttpContext;
        if (httpContext == null) return model;

        var workContext = httpContext.RequestServices.GetService<IWorkContext>();
        var customerService = httpContext.RequestServices.GetService<ICustomerService>();
        if (workContext == null || customerService == null) return model;

        var currentCustomer = await workContext.GetCurrentCustomerAsync();
        if (await customerService.IsAdminAsync(currentCustomer)) return model;

        var storeId = currentCustomer.RegisteredInStoreId;
        if (storeId == 0) return model;

        // Sadece bu store'a ait ürünleri bırak
        var filtered = new List<ProductAttributeProductModel>();
        foreach (var item in model.Data)
        {
            var storeMappings = await _storeMappingService.GetStoreMappingsAsync<Product>(new Product { Id = item.Id });
            if (storeMappings.Any(m => m.StoreId == storeId))
                filtered.Add(item);
        }

        model.Data = filtered;
        model.RecordsTotal = filtered.Count;
        model.RecordsFiltered = filtered.Count;

        return model;
    }
}
