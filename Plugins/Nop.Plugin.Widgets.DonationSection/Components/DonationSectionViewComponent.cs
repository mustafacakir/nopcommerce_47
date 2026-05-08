using Microsoft.AspNetCore.Mvc;
using Nop.Plugin.Widgets.DonationSection.Models.Public;
using Nop.Plugin.Widgets.DonationSection.Services;
using Nop.Services.Catalog;
using Nop.Web.Framework.Components;

namespace Nop.Plugin.Widgets.DonationSection.Components;

public class DonationSectionViewComponent : NopViewComponent
{
    private readonly IDonationSectionService _service;
    private readonly IProductService _productService;

    public DonationSectionViewComponent(
        IDonationSectionService service,
        IProductService productService)
    {
        _service = service;
        _productService = productService;
    }

    public async Task<IViewComponentResult> InvokeAsync(string widgetZone, object additionalData)
    {
        var sections = await _service.GetActiveSectionsAsync();
        var model = new DonationPublicModel();

        foreach (var section in sections)
        {
            model.Sections.Add(new DonSectionPublic
            {
                Id      = section.Id,
                Name    = section.Name,
                IconSvg = section.IconSvg,
                Color   = section.Color,
            });

            var items = await _service.GetActiveItemsBySectionAsync(section.Id);
            foreach (var item in items)
            {
                var product = item.ProductId > 0
                    ? await _productService.GetProductByIdAsync(item.ProductId)
                    : null;

                model.AllItems.Add(new DonItemPublic
                {
                    Id                  = item.Id,
                    SectionId           = item.SectionId,
                    Name                = item.Name,
                    Description         = item.Description,
                    ImageUrl            = item.ImageUrl,
                    Price               = item.Price,
                    ProductId           = item.ProductId,
                    PriceFormatted      = item.Price.ToString("N0") + " ₺",
                    CustomerEntersPrice = product?.CustomerEntersPrice ?? false,
                });
            }
        }

        if (model.Sections.Any())
            model.FirstSectionId = model.Sections.First().Id;

        return View("~/Plugins/Widgets.DonationSection/Views/Components/DonationSection/Default.cshtml", model);
    }
}
