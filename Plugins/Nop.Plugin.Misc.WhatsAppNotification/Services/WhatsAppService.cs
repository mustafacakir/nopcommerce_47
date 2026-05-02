using System.Text;
using System.Text.Json;

namespace Nop.Plugin.Misc.WhatsAppNotification.Services;

public class WhatsAppService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public WhatsAppService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task SendOrderNotificationAsync(
        WhatsAppNotificationSettings settings,
        string orderId,
        string customerName,
        string orderTotal,
        string storeName)
    {
        if (!settings.Enabled
            || string.IsNullOrEmpty(settings.AccessToken)
            || string.IsNullOrEmpty(settings.PhoneNumberId)
            || string.IsNullOrEmpty(settings.RecipientPhone))
            return;

        var payload = new
        {
            messaging_product = "whatsapp",
            to = settings.RecipientPhone.TrimStart('+'),
            type = "template",
            template = new
            {
                name = settings.TemplateName,
                language = new { code = settings.TemplateLanguage },
                components = new[]
                {
                    new
                    {
                        type = "body",
                        parameters = new object[]
                        {
                            new { type = "text", text = orderId },
                            new { type = "text", text = customerName },
                            new { type = "text", text = orderTotal },
                            new { type = "text", text = storeName }
                        }
                    }
                }
            }
        };

        var client = _httpClientFactory.CreateClient();
        client.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", settings.AccessToken);

        var json = JsonSerializer.Serialize(payload);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        await client.PostAsync(
            $"https://graph.facebook.com/v19.0/{settings.PhoneNumberId}/messages",
            content);
    }
}
