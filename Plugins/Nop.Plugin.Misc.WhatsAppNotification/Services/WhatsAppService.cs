using System.Text;
using System.Text.Json;
using Nop.Services.Logging;

namespace Nop.Plugin.Misc.WhatsAppNotification.Services;

public class WhatsAppService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger _logger;

    public WhatsAppService(IHttpClientFactory httpClientFactory, ILogger logger)
    {
        _httpClientFactory = httpClientFactory;
        _logger = logger;
    }

    public async Task SendOrderNotificationAsync(
        WhatsAppNotificationSettings settings,
        string orderId,
        string customerName,
        string orderTotal,
        string storeName)
    {
        if (!settings.Enabled)
        {
            await _logger.InformationAsync("WhatsApp: plugin disabled, skipping.");
            return;
        }
        if (string.IsNullOrEmpty(settings.AccessToken) || string.IsNullOrEmpty(settings.PhoneNumberId) || string.IsNullOrEmpty(settings.RecipientPhone))
        {
            await _logger.WarningAsync("WhatsApp: AccessToken, PhoneNumberId veya RecipientPhone boş.");
            return;
        }

        var payload = new
        {
            messaging_product = "whatsapp",
            to = settings.RecipientPhone.TrimStart('+'),
            type = "template",
            template = new
            {
                name = settings.TemplateName,
                language = new { code = settings.TemplateLanguage }
            }
        };

        var client = _httpClientFactory.CreateClient();
        client.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", settings.AccessToken);

        var json = JsonSerializer.Serialize(payload);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        try
        {
            var response = await client.PostAsync(
                $"https://graph.facebook.com/v19.0/{settings.PhoneNumberId}/messages",
                content);

            var responseBody = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
                await _logger.ErrorAsync($"WhatsApp API hatası ({response.StatusCode}): {responseBody}");
            else
                await _logger.InformationAsync($"WhatsApp gönderildi. Sipariş: {orderId}. Yanıt: {responseBody}");
        }
        catch (Exception ex)
        {
            await _logger.ErrorAsync("WhatsApp gönderme hatası", ex);
        }
    }
}
