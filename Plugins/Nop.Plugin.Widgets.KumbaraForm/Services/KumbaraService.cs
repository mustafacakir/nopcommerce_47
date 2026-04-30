using Nop.Core;
using Nop.Data;
using Nop.Plugin.Widgets.KumbaraForm.Domain;

namespace Nop.Plugin.Widgets.KumbaraForm.Services;

public class KumbaraService : IKumbaraService
{
    private readonly IRepository<KumbaraEntry> _repository;

    public KumbaraService(IRepository<KumbaraEntry> repository)
    {
        _repository = repository;
    }

    public async Task InsertEntryAsync(KumbaraEntry entry)
    {
        await _repository.InsertAsync(entry);
    }

    public async Task<IPagedList<KumbaraEntry>> GetAllEntriesAsync(int pageIndex = 0, int pageSize = int.MaxValue)
    {
        var query = _repository.Table.OrderByDescending(e => e.CreatedOnUtc);
        return await query.ToPagedListAsync(pageIndex, pageSize);
    }

    public async Task<KumbaraEntry> GetEntryByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task DeleteEntryAsync(KumbaraEntry entry)
    {
        await _repository.DeleteAsync(entry);
    }

    public async Task MarkAsReadAsync(KumbaraEntry entry)
    {
        entry.IsRead = true;
        await _repository.UpdateAsync(entry);
    }
}
