using Nop.Core;

namespace Nop.Plugin.Widgets.HeroSlider.Domain;

public class HeroSlide : BaseEntity
{
    public string Title               { get; set; }
    public string Subtitle            { get; set; }
    public string BadgeLabel          { get; set; }
    public string PriceBadge          { get; set; }
    public string PrimaryButtonText   { get; set; }
    public string PrimaryButtonUrl    { get; set; }
    public string SecondaryButtonText { get; set; }
    public string SecondaryButtonUrl  { get; set; }
    public string ImageUrl            { get; set; }
    public string CategoryName        { get; set; }
    public string CategoryIcon        { get; set; }
    public int    DisplayOrder        { get; set; }
    public bool   IsActive            { get; set; }
    public int    StoreId             { get; set; }
}
