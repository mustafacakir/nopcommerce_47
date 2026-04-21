namespace Nop.Plugin.Widgets.SocialShare.Models;

public record PublicInfoModel
{
    public string Title { get; init; } = string.Empty;
    public bool ShowWhatsApp { get; init; }
    public bool ShowFacebook { get; init; }
    public bool ShowX { get; init; }
    public bool ShowCopyLink { get; init; }
}
