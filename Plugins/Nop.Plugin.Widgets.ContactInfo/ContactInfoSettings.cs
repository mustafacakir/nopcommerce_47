using Nop.Core.Configuration;

namespace Nop.Plugin.Widgets.ContactInfo;

public class ContactInfoSettings : ISettings
{
    public string Phone { get; set; }
    public string WhatsApp { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
    public string MapEmbedUrl { get; set; }
    public string FacebookUrl { get; set; }
    public string InstagramUrl { get; set; }
    public string TwitterUrl { get; set; }
    public string YoutubeUrl { get; set; }
}
