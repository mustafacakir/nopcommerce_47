using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Plugin.Misc.WhatsAppNotification.Models;

public record ConfigureModel : BaseNopModel
{
    public int ActiveStoreScopeConfiguration { get; set; }

    [NopResourceDisplayName("Aktif")]
    public bool Enabled { get; set; }
    public bool Enabled_OverrideForStore { get; set; }

    [NopResourceDisplayName("Access Token")]
    public string AccessToken { get; set; }
    public bool AccessToken_OverrideForStore { get; set; }

    [NopResourceDisplayName("Phone Number ID")]
    public string PhoneNumberId { get; set; }
    public bool PhoneNumberId_OverrideForStore { get; set; }

    [NopResourceDisplayName("Alıcı Telefon (ör: +905XXXXXXXXX)")]
    public string RecipientPhone { get; set; }
    public bool RecipientPhone_OverrideForStore { get; set; }

    [NopResourceDisplayName("Şablon Adı")]
    public string TemplateName { get; set; }
    public bool TemplateName_OverrideForStore { get; set; }

    [NopResourceDisplayName("Şablon Dili")]
    public string TemplateLanguage { get; set; }
    public bool TemplateLanguage_OverrideForStore { get; set; }
}
