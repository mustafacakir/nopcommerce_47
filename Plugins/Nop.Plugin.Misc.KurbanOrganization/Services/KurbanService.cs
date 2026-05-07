using System.Text;
using System.Text.Json;
using Nop.Data;
using Nop.Plugin.Misc.KurbanOrganization.Domain;
using Nop.Services.Logging;

namespace Nop.Plugin.Misc.KurbanOrganization.Services;

public class KurbanService
{
    private readonly IRepository<KurbanHisse> _hisseRepository;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger _logger;

    public KurbanService(
        IRepository<KurbanHisse> hisseRepository,
        IHttpClientFactory httpClientFactory,
        ILogger logger)
    {
        _hisseRepository = hisseRepository;
        _httpClientFactory = httpClientFactory;
        _logger = logger;
    }

    public async Task<string> GenerateHisseKoduAsync()
    {
        var count = await _hisseRepository.GetAllAsync(q => q);
        var seq = count.Count + 1;
        return $"KRB-{DateTime.UtcNow.Year}-{seq:D4}";
    }

    public async Task InsertHisseAsync(KurbanHisse hisse)
    {
        await _hisseRepository.InsertAsync(hisse);
    }

    public async Task UpdateHisseAsync(KurbanHisse hisse)
    {
        await _hisseRepository.UpdateAsync(hisse);
    }

    public async Task<KurbanHisse> GetHisseByIdAsync(int id)
    {
        return await _hisseRepository.GetByIdAsync(id);
    }

    public async Task<KurbanHisse> GetHisseByOrderItemIdAsync(int orderItemId)
    {
        var all = await _hisseRepository.GetAllAsync(q =>
            q.Where(h => h.OrderItemId == orderItemId));
        return all.FirstOrDefault();
    }

    public async Task<IList<KurbanHisse>> GetAllHisselerAsync(bool? kesildi = null)
    {
        return await _hisseRepository.GetAllAsync(q =>
        {
            if (kesildi.HasValue)
                q = q.Where(h => h.Kesildi == kesildi.Value);
            return q.OrderByDescending(h => h.CreatedOnUtc);
        });
    }

    public async Task MarkAsKesildiAsync(KurbanHisse hisse)
    {
        hisse.Kesildi = true;
        hisse.KesimTarihi = DateTime.UtcNow;
        await _hisseRepository.UpdateAsync(hisse);
    }

    public async Task SendKesimBildirimiAsync(
        KurbanOrganizationSettings settings,
        KurbanHisse hisse)
    {
        if (string.IsNullOrEmpty(settings.WhatsAppAccessToken) ||
            string.IsNullOrEmpty(settings.WhatsAppPhoneNumberId) ||
            string.IsNullOrEmpty(hisse.MusteriTelefon))
        {
            await _logger.WarningAsync($"Kurban WhatsApp bildirimi gönderilemedi — eksik yapılandırma. HisseKodu: {hisse.HisseKodu}");
            return;
        }

        var phone = hisse.MusteriTelefon.Replace(" ", "").Replace("-", "").TrimStart('+');

        var payload = new
        {
            messaging_product = "whatsapp",
            to = phone,
            type = "template",
            template = new
            {
                name = settings.KesimTemplateName,
                language = new { code = settings.TemplateLanguage },
                components = new[]
                {
                    new
                    {
                        type = "body",
                        parameters = new[]
                        {
                            new { type = "text", text = hisse.HisseKodu },
                            new { type = "text", text = hisse.KurbanTuru }
                        }
                    }
                }
            }
        };

        var client = _httpClientFactory.CreateClient();
        client.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", settings.WhatsAppAccessToken);

        var json = JsonSerializer.Serialize(payload);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        try
        {
            var response = await client.PostAsync(
                $"https://graph.facebook.com/v19.0/{settings.WhatsAppPhoneNumberId}/messages",
                content);

            var responseBody = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                await _logger.ErrorAsync($"Kurban WhatsApp API hatası ({response.StatusCode}): {responseBody}");
            }
            else
            {
                hisse.BildirimGonderildi = true;
                await _hisseRepository.UpdateAsync(hisse);
                await _logger.InformationAsync($"Kurban kesim bildirimi gönderildi. HisseKodu: {hisse.HisseKodu}");
            }
        }
        catch (Exception ex)
        {
            await _logger.ErrorAsync($"Kurban WhatsApp gönderme hatası. HisseKodu: {hisse.HisseKodu}", ex);
        }
    }
}
