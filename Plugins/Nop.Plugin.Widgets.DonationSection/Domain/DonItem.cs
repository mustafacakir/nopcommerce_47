using LinqToDB.Mapping;
using Nop.Core;

namespace Nop.Plugin.Widgets.DonationSection.Domain;

public class DonItem : BaseEntity
{
    [Column("sectionid")]    public int    SectionId   { get; set; }
    [Column("name")]         public string Name        { get; set; }
    [Column("description")]  public string Description { get; set; }
    [Column("imageurl")]     public string ImageUrl    { get; set; }
    [Column("price")]        public decimal Price      { get; set; }
    [Column("productid")]    public int    ProductId   { get; set; }
    [Column("displayorder")] public int    DisplayOrder{ get; set; }
    [Column("isactive")]     public bool   IsActive    { get; set; }
}
