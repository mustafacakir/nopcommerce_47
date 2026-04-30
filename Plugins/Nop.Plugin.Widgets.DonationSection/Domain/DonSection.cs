using LinqToDB.Mapping;
using Nop.Core;

namespace Nop.Plugin.Widgets.DonationSection.Domain;

public class DonSection : BaseEntity
{
    [Column("name")]         public string Name        { get; set; }
    [Column("iconsvg")]      public string IconSvg     { get; set; }
    [Column("color")]        public string Color       { get; set; }
    [Column("displayorder")] public int    DisplayOrder{ get; set; }
    [Column("isactive")]     public bool   IsActive    { get; set; }
}
