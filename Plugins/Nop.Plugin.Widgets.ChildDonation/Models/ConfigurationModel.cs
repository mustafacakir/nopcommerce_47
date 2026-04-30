using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Plugin.Widgets.ChildDonation.Models;

public record ConfigurationModel : BaseNopModel
{
    [NopResourceDisplayName("Çocuk Adı")]
    public string ChildName { get; set; }

    [NopResourceDisplayName("Çocuk Görseli URL")]
    public string ChildImageUrl { get; set; }

    [NopResourceDisplayName("Başlık ({name} çocuğun adıyla değişir)")]
    public string WidgetTitle { get; set; }

    [NopResourceDisplayName("Alt Başlık")]
    public string WidgetSubtitle { get; set; }

    [NopResourceDisplayName("Kategori 1 Adı")]
    public string Cat1Name { get; set; }
    [NopResourceDisplayName("Kategori 1 İkon URL")]
    public string Cat1IconUrl { get; set; }
    [NopResourceDisplayName("Kategori 1 Animasyon Katmanı URL (PNG, şeffaf)")]
    public string Cat1OverlayUrl { get; set; }
    [NopResourceDisplayName("Kategori 1 Fiyat (₺)")]
    public decimal Cat1Price { get; set; }
    [NopResourceDisplayName("Kategori 1 Ürün ID")]
    public int Cat1ProductId { get; set; }

    [NopResourceDisplayName("Kategori 2 Adı")]
    public string Cat2Name { get; set; }
    [NopResourceDisplayName("Kategori 2 İkon URL")]
    public string Cat2IconUrl { get; set; }
    [NopResourceDisplayName("Kategori 2 Animasyon Katmanı URL")]
    public string Cat2OverlayUrl { get; set; }
    [NopResourceDisplayName("Kategori 2 Fiyat (₺)")]
    public decimal Cat2Price { get; set; }
    [NopResourceDisplayName("Kategori 2 Ürün ID")]
    public int Cat2ProductId { get; set; }

    [NopResourceDisplayName("Kategori 3 Adı")]
    public string Cat3Name { get; set; }
    [NopResourceDisplayName("Kategori 3 İkon URL")]
    public string Cat3IconUrl { get; set; }
    [NopResourceDisplayName("Kategori 3 Animasyon Katmanı URL")]
    public string Cat3OverlayUrl { get; set; }
    [NopResourceDisplayName("Kategori 3 Fiyat (₺)")]
    public decimal Cat3Price { get; set; }
    [NopResourceDisplayName("Kategori 3 Ürün ID")]
    public int Cat3ProductId { get; set; }

    [NopResourceDisplayName("Kategori 4 Adı")]
    public string Cat4Name { get; set; }
    [NopResourceDisplayName("Kategori 4 İkon URL")]
    public string Cat4IconUrl { get; set; }
    [NopResourceDisplayName("Kategori 4 Animasyon Katmanı URL")]
    public string Cat4OverlayUrl { get; set; }
    [NopResourceDisplayName("Kategori 4 Fiyat (₺)")]
    public decimal Cat4Price { get; set; }
    [NopResourceDisplayName("Kategori 4 Ürün ID")]
    public int Cat4ProductId { get; set; }
}
