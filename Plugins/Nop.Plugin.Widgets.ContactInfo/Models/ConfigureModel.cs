using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Plugin.Widgets.ContactInfo.Models;

public record ConfigureModel : BaseNopModel
{
    public int ActiveStoreScopeConfiguration { get; set; }

    [NopResourceDisplayName("Telefon")]
    public string Phone { get; set; }
    public bool Phone_OverrideForStore { get; set; }

    [NopResourceDisplayName("WhatsApp")]
    public string WhatsApp { get; set; }
    public bool WhatsApp_OverrideForStore { get; set; }

    [NopResourceDisplayName("E-posta")]
    public string Email { get; set; }
    public bool Email_OverrideForStore { get; set; }

    [NopResourceDisplayName("Adres")]
    public string Address { get; set; }
    public bool Address_OverrideForStore { get; set; }

    [NopResourceDisplayName("Harita Embed URL")]
    public string MapEmbedUrl { get; set; }
    public bool MapEmbedUrl_OverrideForStore { get; set; }

    [NopResourceDisplayName("Facebook URL")]
    public string FacebookUrl { get; set; }
    public bool FacebookUrl_OverrideForStore { get; set; }

    [NopResourceDisplayName("Instagram URL")]
    public string InstagramUrl { get; set; }
    public bool InstagramUrl_OverrideForStore { get; set; }

    [NopResourceDisplayName("Twitter/X URL")]
    public string TwitterUrl { get; set; }
    public bool TwitterUrl_OverrideForStore { get; set; }

    [NopResourceDisplayName("YouTube URL")]
    public string YoutubeUrl { get; set; }
    public bool YoutubeUrl_OverrideForStore { get; set; }
}
