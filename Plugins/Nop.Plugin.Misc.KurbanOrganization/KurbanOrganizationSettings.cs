using Nop.Core.Configuration;

namespace Nop.Plugin.Misc.KurbanOrganization;

public class KurbanOrganizationSettings : ISettings
{
    public int KurbanCategoryId { get; set; }
    public string WhatsAppAccessToken { get; set; }
    public string WhatsAppPhoneNumberId { get; set; }
    public string KesimTemplateName { get; set; } = "kurban_kesildi";
    public string TemplateLanguage { get; set; } = "tr";
    public bool Enabled { get; set; } = true;
}
