using Nop.Core;
using Nop.Core.Caching;
using Nop.Data;
using Nop.Plugin.Widgets.DonationSection.Domain;

namespace Nop.Plugin.Widgets.DonationSection.Services;

public class DonationSectionService : IDonationSectionService
{
    private static readonly CacheKey _sectionsKey = new("donsection.sections.active", "donsection.");
    private static readonly string _prefix = "donsection.";

    private readonly IRepository<DonSection> _sectionRepo;
    private readonly IRepository<DonItem> _itemRepo;
    private readonly IStaticCacheManager _cache;

    public DonationSectionService(
        IRepository<DonSection> sectionRepo,
        IRepository<DonItem> itemRepo,
        IStaticCacheManager cache)
    {
        _sectionRepo = sectionRepo;
        _itemRepo = itemRepo;
        _cache = cache;
    }

    public async Task<IList<DonSection>> GetActiveSectionsAsync() =>
        await _sectionRepo.GetAllAsync(
            q => q.Where(s => s.IsActive).OrderBy(s => s.DisplayOrder),
            _ => _sectionsKey);

    public async Task<IPagedList<DonSection>> GetAllSectionsPagedAsync(int pageIndex = 0, int pageSize = int.MaxValue) =>
        await _sectionRepo.GetAllPagedAsync(q => q.OrderBy(s => s.DisplayOrder), pageIndex, pageSize);

    public async Task<DonSection> GetSectionByIdAsync(int id) => await _sectionRepo.GetByIdAsync(id);

    public async Task InsertSectionAsync(DonSection section) { await _sectionRepo.InsertAsync(section); await _cache.RemoveByPrefixAsync(_prefix); }
    public async Task UpdateSectionAsync(DonSection section) { await _sectionRepo.UpdateAsync(section); await _cache.RemoveByPrefixAsync(_prefix); }
    public async Task DeleteSectionAsync(DonSection section) { await _sectionRepo.DeleteAsync(section); await _cache.RemoveByPrefixAsync(_prefix); }

    public async Task<IList<DonItem>> GetActiveItemsBySectionAsync(int sectionId)
    {
        var key = new CacheKey($"donsection.items.{sectionId}", _prefix);
        return await _itemRepo.GetAllAsync(
            q => q.Where(i => i.SectionId == sectionId && i.IsActive).OrderBy(i => i.DisplayOrder),
            _ => key);
    }

    public async Task<IPagedList<DonItem>> GetAllItemsPagedAsync(int sectionId = 0, int pageIndex = 0, int pageSize = int.MaxValue) =>
        await _itemRepo.GetAllPagedAsync(
            q => sectionId > 0
                ? q.Where(i => i.SectionId == sectionId).OrderBy(i => i.DisplayOrder)
                : q.OrderBy(i => i.SectionId).ThenBy(i => i.DisplayOrder),
            pageIndex, pageSize);

    public async Task<DonItem> GetItemByIdAsync(int id) => await _itemRepo.GetByIdAsync(id);

    public async Task InsertItemAsync(DonItem item) { await _itemRepo.InsertAsync(item); await _cache.RemoveByPrefixAsync(_prefix); }
    public async Task UpdateItemAsync(DonItem item) { await _itemRepo.UpdateAsync(item); await _cache.RemoveByPrefixAsync(_prefix); }
    public async Task DeleteItemAsync(DonItem item) { await _itemRepo.DeleteAsync(item); await _cache.RemoveByPrefixAsync(_prefix); }
}
