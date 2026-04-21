using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Plugin.Widgets.DonationCatalog.Models;
using Nop.Services.Catalog;
using Nop.Services.Media;
using Nop.Web.Factories;
using Nop.Web.Framework.Components;

namespace Nop.Plugin.Widgets.DonationCatalog.Components;

public class DonationCatalogViewComponent : NopViewComponent
{
    private readonly ICategoryService _categoryService;
    private readonly IProductService _productService;
    private readonly IPictureService _pictureService;
    private readonly IProductModelFactory _productModelFactory;
    private readonly IStoreContext _storeContext;

    public DonationCatalogViewComponent(
        ICategoryService categoryService,
        IProductService productService,
        IPictureService pictureService,
        IProductModelFactory productModelFactory,
        IStoreContext storeContext)
    {
        _categoryService = categoryService;
        _productService = productService;
        _pictureService = pictureService;
        _productModelFactory = productModelFactory;
        _storeContext = storeContext;
    }

    public async Task<IViewComponentResult> InvokeAsync(string widgetZone, object additionalData)
    {
        var currentStore = await _storeContext.GetCurrentStoreAsync();
        var allCategories = (await _categoryService.GetAllCategoriesAsync(storeId: currentStore.Id))
            .Where(c => c.ShowOnHomepage)
            .ToList();
        var categoryMap = allCategories.ToDictionary(c => c.Id);

        var model = new DonationPageModel();

        foreach (var category in allCategories)
        {
            var products = (await _productService.SearchProductsAsync(
                categoryIds: new List<int> { category.Id },
                storeId: currentStore.Id,
                showHidden: false,
                pageSize: 100)).ToList();

            if (!products.Any())
                continue;

            var catModel = new DonationCategoryViewModel
            {
                Id = category.Id,
                Name = category.Name,
                ParentCategoryName = category.ParentCategoryId > 0 && categoryMap.TryGetValue(category.ParentCategoryId, out var parent)
                    ? parent.Name
                    : string.Empty
            };

            if (category.PictureId > 0)
            {
                var pic = await _pictureService.GetPictureByIdAsync(category.PictureId);
                catModel.IconUrl = (await _pictureService.GetPictureUrlAsync(pic, 80)).Url;
            }

            catModel.Products = (await _productModelFactory.PrepareProductOverviewModelsAsync(
                products, preparePictureModel: true, productThumbPictureSize: 400)).ToList();

            model.Categories.Add(catModel);
        }

        if (!model.Categories.Any())
            return Content("");

        return View("~/Plugins/Widgets.DonationCatalog/Views/Components/DonationCatalog/Default.cshtml", model);
    }
}
