using Nop.Core.Configuration;

namespace Nop.Plugin.Misc.TelegramNotification;

public class TelegramNotificationSettings : ISettings
{
    public bool Enabled { get; set; }
    public string BotToken { get; set; } = string.Empty;
    public string ChatId { get; set; } = string.Empty;
    public string MessageTemplate { get; set; } = "Yeni siparis!\nNo: {siparis_no}\nMusteri: {musteri}\nTelefon: {telefon}\nTutar: {tutar}";
}
