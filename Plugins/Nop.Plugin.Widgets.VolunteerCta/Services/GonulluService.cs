using Nop.Data;
using Nop.Plugin.Widgets.VolunteerCta.Domain;

namespace Nop.Plugin.Widgets.VolunteerCta.Services;

public class GonulluService
{
    private readonly IRepository<GonulluBasvuru> _repository;

    public GonulluService(IRepository<GonulluBasvuru> repository)
    {
        _repository = repository;
    }

    public async Task InsertAsync(GonulluBasvuru basvuru) =>
        await _repository.InsertAsync(basvuru);

    public async Task DeleteAsync(GonulluBasvuru basvuru) =>
        await _repository.DeleteAsync(basvuru);

    public async Task<GonulluBasvuru> GetByIdAsync(int id) =>
        await _repository.GetByIdAsync(id);

    public async Task<IList<GonulluBasvuru>> GetAllAsync() =>
        (await _repository.GetAllAsync(q => q.OrderByDescending(x => x.CreatedOnUtc))).ToList();
}
