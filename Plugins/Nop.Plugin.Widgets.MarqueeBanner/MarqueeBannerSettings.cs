using Nop.Core.Configuration;

namespace Nop.Plugin.Widgets.MarqueeBanner;

public class MarqueeBannerSettings : ISettings
{
    public string Text { get; set; }
    public string Link { get; set; }
    public string BackgroundColor { get; set; } = "#e43d51";
    public string TextColor { get; set; } = "#ffffff";
    public int Speed { get; set; } = 40;
}
