// PEKIN_CUSTOM: StoreOwner için LowStock raporunu store'a göre filtreler
using Nop.Core;
using Nop.Services.Catalog;
using Nop.Services.Customers;
using Nop.Services.Directory;
using Nop.Services.Helpers;
using Nop.Services.Localization;
using Nop.Services.Orders;
using Nop.Services.Stores;
using Nop.Web.Areas.Admin.Models.Reports;
using Nop.Web.Framework.Models.Extensions;

namespace Nop.Web.Areas.Admin.Factories;

public class TenantReportModelFactory : ReportModelFactory
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IStoreMappingService _storeMappingService;

    public TenantReportModelFactory(
        IHttpContextAccessor httpContextAccessor,
        IStoreMappingService storeMappingService,
        IBaseAdminModelFactory baseAdminModelFactory,
        ICountryService countryService,
        ICustomerReportService customerReportService,
        ICustomerService customerService,
        IDateTimeHelper dateTimeHelper,
        ILocalizationService localizationService,
        IOrderReportService orderReportService,
        IPriceFormatter priceFormatter,
        IProductAttributeFormatter productAttributeFormatter,
        IProductService productService,
        IStoreContext storeContext,
        IWorkContext workContext) : base(
            baseAdminModelFactory, countryService, customerReportService, customerService,
            dateTimeHelper, localizationService, orderReportService, priceFormatter,
            productAttributeFormatter, productService, storeContext, workContext)
    {
        _httpContextAccessor = httpContextAccessor;
        _storeMappingService = storeMappingService;
    }

    public override async Task<LowStockProductListModel> PrepareLowStockProductListModelAsync(LowStockProductSearchModel searchModel)
    {
        var model = await base.PrepareLowStockProductListModelAsync(searchModel);

        var httpContext = _httpContextAccessor.HttpContext;
        if (httpContext == null) return model;

        var workContext = httpContext.RequestServices.GetService<IWorkContext>();
        var customerService = httpContext.RequestServices.GetService<ICustomerService>();
        if (workContext == null || customerService == null) return model;

        var currentCustomer = await workContext.GetCurrentCustomerAsync();
        if (await customerService.IsAdminAsync(currentCustomer)) return model;

        var storeId = currentCustomer.RegisteredInStoreId;
        if (storeId == 0) return model;

        var filtered = new List<LowStockProductModel>();
        foreach (var item in model.Data)
        {
            var storeMappings = await _storeMappingService.GetStoreMappingsAsync(
                new Nop.Core.Domain.Catalog.Product { Id = item.Id });
            if (storeMappings.Any(m => m.StoreId == storeId))
                filtered.Add(item);
        }

        model.Data = filtered;
        model.RecordsTotal = filtered.Count;
        model.RecordsFiltered = filtered.Count;

        return model;
    }
}
