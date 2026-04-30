using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Plugin.Widgets.DonationSection.Models.Public;
using Nop.Services.Catalog;
using Nop.Services.Media;
using Nop.Web.Factories;
using Nop.Web.Framework.Components;

namespace Nop.Plugin.Widgets.DonationSection.Components;

public class DonationSectionViewComponent : NopViewComponent
{
    private readonly ICategoryService _categoryService;
    private readonly IProductService _productService;
    private readonly IPictureService _pictureService;
    private readonly IProductModelFactory _productModelFactory;
    private readonly IStoreContext _storeContext;

    public DonationSectionViewComponent(
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
        var store = await _storeContext.GetCurrentStoreAsync();
        var allCategories = await _categoryService.GetAllCategoriesAsync(storeId: store.Id);

        var model = new DonationPublicModel();

        foreach (var category in allCategories)
        {
            var products = (await _productService.SearchProductsAsync(
                categoryIds: new List<int> { category.Id },
                storeId: store.Id,
                showHidden: false,
                pageSize: 100)).ToList();

            if (!products.Any())
                continue;

            var sec = new DonSectionPublic
            {
                Id   = category.Id,
                Name = category.Name
            };

            if (category.PictureId > 0)
            {
                var pic = await _pictureService.GetPictureByIdAsync(category.PictureId);
                sec.IconUrl = (await _pictureService.GetPictureUrlAsync(pic, 80)).Url;
            }

            model.Sections.Add(sec);

            var overviews = (await _productModelFactory.PrepareProductOverviewModelsAsync(
                products, preparePictureModel: false)).ToList();

            for (var i = 0; i < products.Count; i++)
            {
                model.AllItems.Add(new DonItemPublic
                {
                    Id                  = products[i].Id,
                    SectionId           = category.Id,
                    Name                = products[i].Name,
                    ProductId           = products[i].Id,
                    Price               = products[i].Price,
                    PriceFormatted      = overviews.Count > i ? overviews[i].ProductPrice.Price : products[i].Price.ToString("N0") + " ₺",
                    CustomerEntersPrice = products[i].CustomerEntersPrice
                });
            }
        }

        if (model.Sections.Any())
            model.FirstSectionId = model.Sections.First().Id;

        return View("~/Plugins/Widgets.DonationSection/Views/Components/DonationSection/Default.cshtml", model);
    }
}
