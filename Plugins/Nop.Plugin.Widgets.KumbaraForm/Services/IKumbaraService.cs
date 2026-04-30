using Nop.Core;
using Nop.Plugin.Widgets.KumbaraForm.Domain;

namespace Nop.Plugin.Widgets.KumbaraForm.Services;

public interface IKumbaraService
{
    Task InsertEntryAsync(KumbaraEntry entry);
    Task<IPagedList<KumbaraEntry>> GetAllEntriesAsync(int pageIndex = 0, int pageSize = int.MaxValue);
    Task<KumbaraEntry> GetEntryByIdAsync(int id);
    Task DeleteEntryAsync(KumbaraEntry entry);
    Task MarkAsReadAsync(KumbaraEntry entry);
}
