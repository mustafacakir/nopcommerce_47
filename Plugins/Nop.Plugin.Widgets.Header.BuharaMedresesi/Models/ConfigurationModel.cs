using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Plugin.Widgets.Header.BuharaMedresesi.Models;

public record ConfigurationModel : BaseNopModel
{
    public int ActiveStoreScopeConfiguration { get; set; }

    [NopResourceDisplayName("Phone")]        public string Phone        { get; set; }
    [NopResourceDisplayName("Email")]        public string Email        { get; set; }
    [NopResourceDisplayName("InstagramUrl")] public string InstagramUrl { get; set; }
    [NopResourceDisplayName("TwitterUrl")]   public string TwitterUrl   { get; set; }
    [NopResourceDisplayName("YoutubeUrl")]   public string YoutubeUrl   { get; set; }
    [NopResourceDisplayName("FacebookUrl")]  public string FacebookUrl  { get; set; }
    [NopResourceDisplayName("LinkedinUrl")]  public string LinkedinUrl  { get; set; }
    [NopResourceDisplayName("TiktokUrl")]    public string TiktokUrl    { get; set; }
    [NopResourceDisplayName("DonateUrl")]    public string DonateUrl    { get; set; }
    [NopResourceDisplayName("DonateText")]   public string DonateText   { get; set; }
}
