using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Plugin.Widgets.DonationGrid.Models;
using Nop.Services.Catalog;
using Nop.Services.Configuration;
using Nop.Services.Media;
using Nop.Web.Factories;
using Nop.Web.Framework.Components;
using Nop.Web.Models.Catalog;

namespace Nop.Plugin.Widgets.DonationGrid.Components;

public class DonationGridViewComponent : NopViewComponent
{
    private readonly ICategoryService _categoryService;
    private readonly IProductService _productService;
    private readonly IPictureService _pictureService;
    private readonly IProductModelFactory _productModelFactory;
    private readonly ISettingService _settingService;
    private readonly IStoreContext _storeContext;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public DonationGridViewComponent(
        ICategoryService categoryService,
        IProductService productService,
        IPictureService pictureService,
        IProductModelFactory productModelFactory,
        ISettingService settingService,
        IStoreContext storeContext,
        IHttpContextAccessor httpContextAccessor)
    {
        _categoryService = categoryService;
        _productService = productService;
        _pictureService = pictureService;
        _productModelFactory = productModelFactory;
        _settingService = settingService;
        _storeContext = storeContext;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<IViewComponentResult> InvokeAsync(string widgetZone, object additionalData)
    {
        if (additionalData is not CategoryModel categoryModel)
            return Content("");

        var store = await _storeContext.GetCurrentStoreAsync();
        var settings = await _settingService.LoadSettingAsync<DonationGridSettings>(store.Id);

        if (settings.RootCategoryId != 0 && categoryModel.Id != settings.RootCategoryId)
            return Content("");

        var subCats = await _categoryService.GetAllCategoriesByParentCategoryIdAsync(
            parentCategoryId: categoryModel.Id,
            showHidden: false);

        var model = new DonationGridModel();

        var sourceCategories = subCats.Any() ? subCats : new List<Core.Domain.Catalog.Category>();

        if (!sourceCategories.Any())
        {
            var products = (await _productService.SearchProductsAsync(
                categoryIds: new List<int> { categoryModel.Id },
                storeId: store.Id,
                showHidden: false,
                pageSize: 100)).ToList();

            if (!products.Any())
                return Content("");

            var cat = new DonationGridCategoryModel { Id = categoryModel.Id, Name = categoryModel.Name };
            cat.Products = (await _productModelFactory.PrepareProductOverviewModelsAsync(
                products, preparePictureModel: true, productThumbPictureSize: 400)).ToList();
            model.Categories.Add(cat);
        }
        else
        {
            foreach (var subcat in sourceCategories)
            {
                var products = (await _productService.SearchProductsAsync(
                    categoryIds: new List<int> { subcat.Id },
                    storeId: store.Id,
                    showHidden: false,
                    pageSize: 100)).ToList();

                if (!products.Any())
                    continue;

                var catModel = new DonationGridCategoryModel { Id = subcat.Id, Name = subcat.Name };

                if (subcat.PictureId > 0)
                {
                    var pic = await _pictureService.GetPictureByIdAsync(subcat.PictureId);
                    catModel.IconUrl = (await _pictureService.GetPictureUrlAsync(pic, 60)).Url;
                }

                catModel.Products = (await _productModelFactory.PrepareProductOverviewModelsAsync(
                    products, preparePictureModel: true, productThumbPictureSize: 400)).ToList();

                model.Categories.Add(catModel);
            }
        }

        if (!model.Categories.Any())
            return Content("");

        _httpContextAccessor.HttpContext!.Items["DonationGridActive"] = true;

        return View("~/Plugins/Widgets.DonationGrid/Views/Components/DonationGrid/Default.cshtml", model);
    }
}
