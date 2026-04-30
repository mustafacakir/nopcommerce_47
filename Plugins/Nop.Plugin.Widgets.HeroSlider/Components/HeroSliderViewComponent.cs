using Microsoft.AspNetCore.Mvc;
using Nop.Plugin.Widgets.HeroSlider.Models.Public;
using Nop.Plugin.Widgets.HeroSlider.Services;
using Nop.Web.Framework.Components;

namespace Nop.Plugin.Widgets.HeroSlider.Components;

public class HeroSliderViewComponent : NopViewComponent
{
    private readonly IHeroSliderService _service;

    public HeroSliderViewComponent(IHeroSliderService service)
        => _service = service;

    public async Task<IViewComponentResult> InvokeAsync(string widgetZone, object additionalData)
    {
        var slides = await _service.GetActiveSlidesAsync();
        var model = new HeroSliderPublicModel
        {
            Slides = slides.Select(s => new HeroSlidePublic
            {
                Title               = s.Title,
                Subtitle            = s.Subtitle,
                BadgeLabel          = s.BadgeLabel,
                PriceBadge          = s.PriceBadge,
                PrimaryButtonText   = s.PrimaryButtonText,
                PrimaryButtonUrl    = s.PrimaryButtonUrl,
                SecondaryButtonText = s.SecondaryButtonText,
                SecondaryButtonUrl  = s.SecondaryButtonUrl,
                ImageUrl            = s.ImageUrl,
                CategoryName        = s.CategoryName,
                CategoryIcon        = s.CategoryIcon
            }).ToList()
        };
        return View("~/Plugins/Widgets.HeroSlider/Views/Components/HeroSlider/Default.cshtml", model);
    }
}
