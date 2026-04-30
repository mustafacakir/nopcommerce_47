using LinqToDB;
using Nop.Core;
using Nop.Core.Caching;
using Nop.Core.Domain.Orders;
using Nop.Core.Domain.Payments;
using Nop.Data;
using Nop.Plugin.Widgets.CampaignProgress.Domain;

namespace Nop.Plugin.Widgets.CampaignProgress.Services;

public class CampaignService : ICampaignService
{
    private static readonly CacheKey _activeCampaignsCacheKey =
        new("campaign.progress.active.all", "campaign.progress.");

    private readonly IRepository<Campaign> _campaignRepository;
    private readonly IRepository<Order> _orderRepository;
    private readonly IRepository<OrderItem> _orderItemRepository;
    private readonly IStaticCacheManager _cacheManager;

    public CampaignService(
        IRepository<Campaign> campaignRepository,
        IRepository<Order> orderRepository,
        IRepository<OrderItem> orderItemRepository,
        IStaticCacheManager cacheManager)
    {
        _campaignRepository = campaignRepository;
        _orderRepository = orderRepository;
        _orderItemRepository = orderItemRepository;
        _cacheManager = cacheManager;
    }

    public async Task<IList<Campaign>> GetActiveCampaignsAsync()
    {
        return await _campaignRepository.GetAllAsync(
            query => query.Where(c => c.IsActive).OrderBy(c => c.DisplayOrder),
            _ => _activeCampaignsCacheKey);
    }

    public async Task<IPagedList<Campaign>> GetAllCampaignsPagedAsync(int pageIndex = 0, int pageSize = int.MaxValue)
    {
        return await _campaignRepository.GetAllPagedAsync(
            query => query.OrderBy(c => c.DisplayOrder),
            pageIndex, pageSize);
    }

    public async Task<Campaign> GetByIdAsync(int id)
    {
        return await _campaignRepository.GetByIdAsync(id);
    }

    public async Task<Campaign> GetBySlugAsync(string slug)
    {
        if (string.IsNullOrWhiteSpace(slug))
            return null;

        return await _campaignRepository.Table
            .FirstOrDefaultAsync(c => c.Slug == slug && c.IsActive);
    }

    public async Task InsertAsync(Campaign campaign)
    {
        ArgumentNullException.ThrowIfNull(campaign);
        campaign.CreatedOnUtc = DateTime.UtcNow;
        await _campaignRepository.InsertAsync(campaign);
        await _cacheManager.RemoveByPrefixAsync("campaign.progress.");
    }

    public async Task UpdateAsync(Campaign campaign)
    {
        ArgumentNullException.ThrowIfNull(campaign);
        await _campaignRepository.UpdateAsync(campaign);
        await _cacheManager.RemoveByPrefixAsync("campaign.progress.");
    }

    public async Task DeleteAsync(Campaign campaign)
    {
        ArgumentNullException.ThrowIfNull(campaign);
        await _campaignRepository.DeleteAsync(campaign);
        await _cacheManager.RemoveByPrefixAsync("campaign.progress.");
    }

    public async Task<decimal> GetCurrentAmountAsync(Campaign campaign)
    {
        if (campaign.LinkedProductId <= 0)
            return campaign.ManualBonus;

        var paid = (int)PaymentStatus.Paid;

        var query = from oi in _orderItemRepository.Table
                    join o in _orderRepository.Table on oi.OrderId equals o.Id
                    where oi.ProductId == campaign.LinkedProductId
                       && o.PaymentStatusId == paid
                       && !o.Deleted
                    select oi.PriceInclTax;

        var fromOrders = (await query.ToListAsync()).Sum();
        return fromOrders + campaign.ManualBonus;
    }
}
