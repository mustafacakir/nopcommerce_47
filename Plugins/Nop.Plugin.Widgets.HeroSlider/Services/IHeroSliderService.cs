using Nop.Core;
using Nop.Plugin.Widgets.HeroSlider.Domain;

namespace Nop.Plugin.Widgets.HeroSlider.Services;

public interface IHeroSliderService
{
    Task<IList<HeroSlide>> GetActiveSlidesAsync();
    Task<IPagedList<HeroSlide>> GetAllPagedAsync(int pageIndex = 0, int pageSize = int.MaxValue);
    Task<HeroSlide?> GetByIdAsync(int id);
    Task InsertAsync(HeroSlide slide);
    Task UpdateAsync(HeroSlide slide);
    Task DeleteAsync(HeroSlide slide);
}
