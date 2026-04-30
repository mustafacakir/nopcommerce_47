using Nop.Core;
using Nop.Plugin.Widgets.CampaignProgress.Domain;

namespace Nop.Plugin.Widgets.CampaignProgress.Services;

public interface ICampaignService
{
    Task<IList<Campaign>> GetActiveCampaignsAsync();
    Task<IPagedList<Campaign>> GetAllCampaignsPagedAsync(int pageIndex = 0, int pageSize = int.MaxValue);
    Task<Campaign> GetByIdAsync(int id);
    Task<Campaign> GetBySlugAsync(string slug);
    Task InsertAsync(Campaign campaign);
    Task UpdateAsync(Campaign campaign);
    Task DeleteAsync(Campaign campaign);
    Task<decimal> GetCurrentAmountAsync(Campaign campaign);
}
