using Nop.Core.Configuration;

namespace Nop.Plugin.Widgets.SocialShare;

public class SocialShareSettings : ISettings
{
    public string Title { get; set; } = "Bu kampanyayı paylaş";
    public bool ShowWhatsApp { get; set; } = true;
    public bool ShowFacebook { get; set; } = true;
    public bool ShowX { get; set; } = true;
    public bool ShowCopyLink { get; set; } = true;
}
