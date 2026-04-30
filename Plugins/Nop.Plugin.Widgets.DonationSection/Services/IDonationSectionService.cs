using Nop.Core;
using Nop.Plugin.Widgets.DonationSection.Domain;

namespace Nop.Plugin.Widgets.DonationSection.Services;

public interface IDonationSectionService
{
    // Sections
    Task<IList<DonSection>> GetActiveSectionsAsync();
    Task<IPagedList<DonSection>> GetAllSectionsPagedAsync(int pageIndex = 0, int pageSize = int.MaxValue);
    Task<DonSection> GetSectionByIdAsync(int id);
    Task InsertSectionAsync(DonSection section);
    Task UpdateSectionAsync(DonSection section);
    Task DeleteSectionAsync(DonSection section);

    // Items
    Task<IList<DonItem>> GetActiveItemsBySectionAsync(int sectionId);
    Task<IPagedList<DonItem>> GetAllItemsPagedAsync(int sectionId = 0, int pageIndex = 0, int pageSize = int.MaxValue);
    Task<DonItem> GetItemByIdAsync(int id);
    Task InsertItemAsync(DonItem item);
    Task UpdateItemAsync(DonItem item);
    Task DeleteItemAsync(DonItem item);
}
