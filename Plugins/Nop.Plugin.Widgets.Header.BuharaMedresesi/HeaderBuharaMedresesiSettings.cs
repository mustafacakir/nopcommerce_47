using Nop.Core.Configuration;

namespace Nop.Plugin.Widgets.Header.BuharaMedresesi;

public class HeaderBuharaMedresesiSettings : ISettings
{
    public string Phone        { get; set; } = "+90 212 664 85 71";
    public string Email        { get; set; } = "info@buharamedresesi.org.tr";
    public string InstagramUrl { get; set; } = "#";
    public string TwitterUrl   { get; set; } = "#";
    public string YoutubeUrl   { get; set; } = "#";
    public string FacebookUrl  { get; set; } = "#";
    public string LinkedinUrl  { get; set; } = "#";
    public string TiktokUrl    { get; set; } = "#";
    public string DonateUrl    { get; set; } = "/bagis";
    public string DonateText   { get; set; } = "Bağış Yap";
}
