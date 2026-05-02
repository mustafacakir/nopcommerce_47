using Nop.Core.Configuration;

namespace Nop.Plugin.Misc.WhatsAppNotification;

public class WhatsAppNotificationSettings : ISettings
{
    public bool Enabled { get; set; }
    public string AccessToken { get; set; }
    public string PhoneNumberId { get; set; }
    public string RecipientPhone { get; set; }
    public string TemplateName { get; set; } = "yeni_siparis";
    public string TemplateLanguage { get; set; } = "tr";
}
