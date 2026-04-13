// PEKIN_CUSTOM: StoreOwner için kategori listesini kendi store'uyla filtreler
using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Core;
using Nop.Core.Caching;
using Nop.Services.Catalog;
using Nop.Services.Customers;
using Nop.Services.Directory;
using Nop.Services.Helpers;
using Nop.Services.Localization;
using Nop.Services.Logging;
using Nop.Services.Messages;
using Nop.Services.Plugins;
using Nop.Services.Shipping;
using Nop.Services.Shipping.Date;
using Nop.Services.Stores;
using Nop.Services.Tax;
using Nop.Services.Topics;
using Nop.Services.Vendors;

namespace Nop.Web.Areas.Admin.Factories;

public class TenantBaseAdminModelFactory : BaseAdminModelFactory
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public TenantBaseAdminModelFactory(
        IHttpContextAccessor httpContextAccessor,
        ICategoryService categoryService,
        ICategoryTemplateService categoryTemplateService,
        ICountryService countryService,
        ICurrencyService currencyService,
        ICustomerActivityService customerActivityService,
        ICustomerService customerService,
        IDateRangeService dateRangeService,
        IDateTimeHelper dateTimeHelper,
        IEmailAccountService emailAccountService,
        ILanguageService languageService,
        ILocalizationService localizationService,
        IManufacturerService manufacturerService,
        IManufacturerTemplateService manufacturerTemplateService,
        IPluginService pluginService,
        IProductTemplateService productTemplateService,
        ISpecificationAttributeService specificationAttributeService,
        IShippingService shippingService,
        IStateProvinceService stateProvinceService,
        IStaticCacheManager staticCacheManager,
        IStoreService storeService,
        ITaxCategoryService taxCategoryService,
        ITopicTemplateService topicTemplateService,
        IVendorService vendorService) : base(
            categoryService, categoryTemplateService, countryService, currencyService,
            customerActivityService, customerService, dateRangeService, dateTimeHelper,
            emailAccountService, languageService, localizationService, manufacturerService,
            manufacturerTemplateService, pluginService, productTemplateService,
            specificationAttributeService, shippingService, stateProvinceService,
            staticCacheManager, storeService, taxCategoryService, topicTemplateService,
            vendorService)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    protected override async Task<List<SelectListItem>> GetCategoryListAsync()
    {
        var allItems = await base.GetCategoryListAsync();

        var httpContext = _httpContextAccessor.HttpContext;
        if (httpContext == null)
            return allItems;

        var workContext = httpContext.RequestServices.GetService<IWorkContext>();
        var customerService = httpContext.RequestServices.GetService<ICustomerService>();

        if (workContext == null || customerService == null)
            return allItems;

        var currentCustomer = await workContext.GetCurrentCustomerAsync();

        if (await customerService.IsAdminAsync(currentCustomer))
            return allItems;

        var storeId = currentCustomer.RegisteredInStoreId;
        if (storeId == 0)
            return allItems;

        // Sadece bu store'a ait kategorileri getir (cache'ten tüm liste alınır, sonra filtre)
        var storeCategories = await _categoryService.GetAllCategoriesAsync(showHidden: true, storeId: storeId);
        var storeCategoryIds = storeCategories.Select(c => c.Id.ToString()).ToHashSet();

        return allItems.Where(item => storeCategoryIds.Contains(item.Value)).ToList();
    }

    protected override async Task<List<SelectListItem>> GetManufacturerListAsync()
    {
        var allItems = await base.GetManufacturerListAsync();

        var httpContext = _httpContextAccessor.HttpContext;
        if (httpContext == null)
            return allItems;

        var workContext = httpContext.RequestServices.GetService<IWorkContext>();
        var customerService = httpContext.RequestServices.GetService<ICustomerService>();

        if (workContext == null || customerService == null)
            return allItems;

        var currentCustomer = await workContext.GetCurrentCustomerAsync();

        if (await customerService.IsAdminAsync(currentCustomer))
            return allItems;

        var storeId = currentCustomer.RegisteredInStoreId;
        if (storeId == 0)
            return allItems;

        // Sadece bu store'a ait üreticileri getir
        var storeManufacturers = await _manufacturerService.GetAllManufacturersAsync(showHidden: true, storeId: storeId);
        var storeManufacturerIds = storeManufacturers.Select(m => m.Id.ToString()).ToHashSet();

        return allItems.Where(item => storeManufacturerIds.Contains(item.Value)).ToList();
    }
}
