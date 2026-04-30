using Nop.Core.Configuration;

namespace Nop.Plugin.Widgets.ChildDonation;

public class ChildDonationSettings : ISettings
{
    public string ChildName { get; set; } = "Ahmet";
    public string ChildImageUrl { get; set; }
    public string WidgetTitle { get; set; } = "Merhaba, benim adım {name}!";
    public string WidgetSubtitle { get; set; } = "İhtiyaç sahibi bir çocuğu mutlu etmeye ne dersiniz?";

    public string Cat1Name { get; set; } = "Yemek";
    public string Cat1IconUrl { get; set; }
    public string Cat1OverlayUrl { get; set; }
    public decimal Cat1Price { get; set; } = 240;
    public int Cat1ProductId { get; set; }

    public string Cat2Name { get; set; } = "Oyuncak";
    public string Cat2IconUrl { get; set; }
    public string Cat2OverlayUrl { get; set; }
    public decimal Cat2Price { get; set; } = 150;
    public int Cat2ProductId { get; set; }

    public string Cat3Name { get; set; } = "Kırtasiye";
    public string Cat3IconUrl { get; set; }
    public string Cat3OverlayUrl { get; set; }
    public decimal Cat3Price { get; set; } = 150;
    public int Cat3ProductId { get; set; }

    public string Cat4Name { get; set; } = "Giyecek";
    public string Cat4IconUrl { get; set; }
    public string Cat4OverlayUrl { get; set; }
    public decimal Cat4Price { get; set; } = 250;
    public int Cat4ProductId { get; set; }
}
