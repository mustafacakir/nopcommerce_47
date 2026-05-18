using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Plugin.Misc.TelegramNotification.Models;

public record ConfigureModel : BaseNopModel
{
    public int ActiveStoreScopeConfiguration { get; set; }

    [NopResourceDisplayName("Aktif")]
    public bool Enabled { get; set; }
    public bool Enabled_OverrideForStore { get; set; }

    [NopResourceDisplayName("Bot Token")]
    public string BotToken { get; set; } = string.Empty;
    public bool BotToken_OverrideForStore { get; set; }

    [NopResourceDisplayName("Chat ID")]
    public string ChatId { get; set; } = string.Empty;
    public bool ChatId_OverrideForStore { get; set; }

    [NopResourceDisplayName("Mesaj Şablonu")]
    public string MessageTemplate { get; set; } = string.Empty;
    public bool MessageTemplate_OverrideForStore { get; set; }
}
