namespace Nop.Plugin.Widgets.DonationSection.Models.Public;

public class DonSectionPublic
{
    public int    Id           { get; set; }
    public string Name         { get; set; }
    public string IconSvg      { get; set; }
    public string Color        { get; set; }
    public string IconUrl      { get; set; }
}

public class DonItemPublic
{
    public int     Id                  { get; set; }
    public int     SectionId           { get; set; }
    public string  Name                { get; set; }
    public string  Description         { get; set; }
    public string  ImageUrl            { get; set; }
    public decimal Price               { get; set; }
    public int     ProductId           { get; set; }
    public string  PriceFormatted      { get; set; }
    public bool    CustomerEntersPrice { get; set; }
}

public class DonationPublicModel
{
    public List<DonSectionPublic> Sections  { get; set; } = new();
    public List<DonItemPublic>    AllItems  { get; set; } = new();
    public int                    FirstSectionId { get; set; }
}
