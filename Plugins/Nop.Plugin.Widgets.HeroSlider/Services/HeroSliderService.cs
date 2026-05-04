using Nop.Core;
using Nop.Core.Caching;
using Nop.Data;
using Nop.Plugin.Widgets.HeroSlider.Domain;

namespace Nop.Plugin.Widgets.HeroSlider.Services;

public class HeroSliderService : IHeroSliderService
{
    private readonly IRepository<HeroSlide> _repo;
    private readonly IStaticCacheManager _cache;

    public HeroSliderService(IRepository<HeroSlide> repo, IStaticCacheManager cache)
    {
        _repo = repo;
        _cache = cache;
    }

    private static CacheKey ActiveKey(int storeId) => new($"heroslider.active.{storeId}", "heroslider.");

    public async Task<IList<HeroSlide>> GetActiveSlidesAsync(int storeId) =>
        await _repo.GetAllAsync(
            q => q.Where(s => s.IsActive && s.StoreId == storeId).OrderBy(s => s.DisplayOrder),
            _ => ActiveKey(storeId));

    public async Task<IPagedList<HeroSlide>> GetAllPagedAsync(int storeId, int pageIndex = 0, int pageSize = int.MaxValue) =>
        await _repo.GetAllPagedAsync(q => q.Where(s => s.StoreId == storeId).OrderBy(s => s.DisplayOrder), pageIndex, pageSize);

    public async Task<HeroSlide?> GetByIdAsync(int id) => await _repo.GetByIdAsync(id);

    public async Task InsertAsync(HeroSlide slide)
    {
        await _repo.InsertAsync(slide);
        await _cache.RemoveByPrefixAsync("heroslider.");
    }

    public async Task UpdateAsync(HeroSlide slide)
    {
        await _repo.UpdateAsync(slide);
        await _cache.RemoveByPrefixAsync("heroslider.");
    }

    public async Task DeleteAsync(HeroSlide slide)
    {
        await _repo.DeleteAsync(slide);
        await _cache.RemoveByPrefixAsync("heroslider.");
    }
}
