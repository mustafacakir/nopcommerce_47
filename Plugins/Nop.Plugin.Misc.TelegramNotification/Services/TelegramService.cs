using System.Net;
using System.Text;
using System.Text.Json;
using Nop.Services.Logging;

namespace Nop.Plugin.Misc.TelegramNotification.Services;

public class TelegramService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger _logger;

    public TelegramService(IHttpClientFactory httpClientFactory, ILogger logger)
    {
        _httpClientFactory = httpClientFactory;
        _logger = logger;
    }

    public async Task SendAsync(TelegramNotificationSettings settings, string siparisNo, string musteri, string telefon, string tutar)
    {
        if (!settings.Enabled)
            return;

        if (string.IsNullOrWhiteSpace(settings.BotToken) || string.IsNullOrWhiteSpace(settings.ChatId))
        {
            await _logger.WarningAsync("Telegram: BotToken veya ChatId boş.");
            return;
        }

        var mesaj = settings.MessageTemplate
            .Replace("{siparis_no}", siparisNo)
            .Replace("{musteri}", musteri)
            .Replace("{telefon}", telefon)
            .Replace("{tutar}", tutar);

        var url = $"https://api.telegram.org/bot{settings.BotToken}/sendMessage";
        var payload = new { chat_id = settings.ChatId, text = mesaj };
        var json = JsonSerializer.Serialize(payload);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        try
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.PostAsync(url, content);
            var body = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                await _logger.ErrorAsync($"Telegram hata ({response.StatusCode}): {body}");
            else
                await _logger.InformationAsync($"Telegram gönderildi. Sipariş: {siparisNo}");
        }
        catch (Exception ex)
        {
            await _logger.ErrorAsync("Telegram gönderme hatası", ex);
        }
    }
}
