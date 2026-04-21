using Nop.Web.Framework.Models;

namespace Nop.Plugin.Widgets.SocialShare.Models;

public record ConfigurationModel : BaseNopModel
{
    public int ActiveStoreScopeConfiguration { get; init; }
    public string Title { get; set; } = string.Empty;
    public bool ShowWhatsApp { get; set; }
    public bool ShowFacebook { get; set; }
    public bool ShowX { get; set; }
    public bool ShowCopyLink { get; set; }
}
