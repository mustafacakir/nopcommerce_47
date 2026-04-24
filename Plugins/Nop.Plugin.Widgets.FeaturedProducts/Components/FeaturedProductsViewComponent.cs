using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Plugin.Widgets.FeaturedProducts.Models;
using Nop.Services.Catalog;
using Nop.Services.Configuration;
using Nop.Services.Media;
using Nop.Services.Security;
using Nop.Services.Seo;
using Nop.Services.Stores;
using Nop.Web.Framework.Components;

namespace Nop.Plugin.Widgets.FeaturedProducts.Components;

public class FeaturedProductsViewComponent : NopViewComponent
{
    private readonly IAclService _aclService;
    private readonly ICategoryService _categoryService;
    private readonly IPictureService _pictureService;
    private readonly IPriceCalculationService _priceCalculationService;
    private readonly IPriceFormatter _priceFormatter;
    private readonly IProductService _productService;
    private readonly ISettingService _settingService;
    private readonly IStoreMappingService _storeMappingService;
    private readonly IStoreContext _storeContext;
    private readonly IUrlRecordService _urlRecordService;
    private readonly IWorkContext _workContext;

    public FeaturedProductsViewComponent(
        IAclService aclService,
        ICategoryService categoryService,
        IPictureService pictureService,
        IPriceCalculationService priceCalculationService,
        IPriceFormatter priceFormatter,
        IProductService productService,
        ISettingService settingService,
        IStoreMappingService storeMappingService,
        IStoreContext storeContext,
        IUrlRecordService urlRecordService,
        IWorkContext workContext)
    {
        _aclService = aclService;
        _categoryService = categoryService;
        _pictureService = pictureService;
        _priceCalculationService = priceCalculationService;
        _priceFormatter = priceFormatter;
        _productService = productService;
        _settingService = settingService;
        _storeMappingService = storeMappingService;
        _storeContext = storeContext;
        _urlRecordService = urlRecordService;
        _workContext = workContext;
    }

    public async Task<IViewComponentResult> InvokeAsync(string widgetZone, object additionalData)
    {
        var store = await _storeContext.GetCurrentStoreAsync();
        var settings = await _settingService.LoadSettingAsync<FeaturedProductsSettings>(store.Id);

        var allProducts = await _productService.GetAllProductsDisplayedOnHomepageAsync();

        var products = await allProducts
            .WhereAwait(async p =>
                await _aclService.AuthorizeAsync(p) &&
                await _storeMappingService.AuthorizeAsync(p))
            .Where(p => _productService.ProductIsAvailable(p) && p.VisibleIndividually)
            .Take(settings.ProductCount)
            .ToListAsync();

        var customer = await _workContext.GetCurrentCustomerAsync();
        var cards = new List<ProductCardModel>();

        foreach (var product in products)
        {
            var (_, finalPrice, _, _) = await _priceCalculationService.GetFinalPriceAsync(product, customer, store);
            var priceStr = await _priceFormatter.FormatPriceAsync(finalPrice);

            var hasDiscount = product.OldPrice > 0 && product.OldPrice > finalPrice;
            var oldPriceStr = hasDiscount ? await _priceFormatter.FormatPriceAsync(product.OldPrice) : null;

            var pictures = await _pictureService.GetPicturesByProductIdAsync(product.Id, 1);
            var imageUrl = pictures.Count > 0
                ? (await _pictureService.GetPictureUrlAsync(pictures[0], 370)).Url
                : await _pictureService.GetDefaultPictureUrlAsync(370);

            var seName = await _urlRecordService.GetSeNameAsync(product);

            var productCategories = await _categoryService.GetProductCategoriesByProductIdAsync(product.Id);
            var categoryName = string.Empty;
            if (productCategories.Any())
            {
                var category = await _categoryService.GetCategoryByIdAsync(productCategories[0].CategoryId);
                categoryName = category?.Name ?? string.Empty;
            }

            var rating = product.ApprovedTotalReviews > 0
                ? (double)product.ApprovedRatingSum / product.ApprovedTotalReviews
                : 0;

            cards.Add(new ProductCardModel
            {
                Id = product.Id,
                Name = product.Name,
                SeName = seName,
                ImageUrl = imageUrl,
                CategoryName = categoryName,
                Price = priceStr,
                OldPrice = oldPriceStr,
                HasDiscount = hasDiscount,
                ReviewCount = product.ApprovedTotalReviews,
                Rating = Math.Round(rating, 1)
            });
        }

        var model = new PublicInfoModel
        {
            SectionBadge = settings.SectionBadge,
            SectionTitle = settings.SectionTitle,
            SectionSubtitle = settings.SectionSubtitle,
            ViewAllText = settings.ViewAllText,
            ViewAllUrl = settings.ViewAllUrl,
            Products = cards
        };

        return View("~/Plugins/Widgets.FeaturedProducts/Views/PublicInfo.cshtml", model);
    }
}
