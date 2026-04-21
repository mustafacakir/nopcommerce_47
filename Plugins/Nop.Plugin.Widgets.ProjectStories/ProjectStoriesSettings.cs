using Nop.Core.Configuration;

namespace Nop.Plugin.Widgets.ProjectStories;

public class ProjectStoriesSettings : ISettings
{
    public string SectionTitle { get; set; } = "Projelerimizden Kareler";
    public string SectionSubtitle { get; set; } = "Bağışlarınızın sahada yarattığı farkı birlikte görüyoruz.";

    public string Story1Title { get; set; } = "Yemen'de Su Kuyusu";
    public string Story1Tag { get; set; } = "Yurtdışı";
    public string Story1Description { get; set; } = "Yemen'in Hadramut bölgesinde 300 aileye temiz su ulaştıran kuyumuz tamamlandı.";
    public string Story1ImageUrl { get; set; } = "";
    public string Story1VideoUrl { get; set; } = "";
    public string Story1LinkUrl { get; set; } = "";

    public string Story2Title { get; set; } = "Suriyeli Öğrencilere Kırtasiye";
    public string Story2Tag { get; set; } = "Eğitim";
    public string Story2Description { get; set; } = "500 Suriyeli çocuğa okul çantası ve kırtasiye seti ulaştırıldı.";
    public string Story2ImageUrl { get; set; } = "";
    public string Story2VideoUrl { get; set; } = "";
    public string Story2LinkUrl { get; set; } = "";

    public string Story3Title { get; set; } = "Kışlık Yardım Paketi";
    public string Story3Tag { get; set; } = "Yurtiçi";
    public string Story3Description { get; set; } = "Doğu Anadolu'da 1.200 aileye battaniye, mont ve temel ihtiyaç paketi teslim edildi.";
    public string Story3ImageUrl { get; set; } = "";
    public string Story3VideoUrl { get; set; } = "";
    public string Story3LinkUrl { get; set; } = "";

    public string Story4Title { get; set; } = "";
    public string Story4Tag { get; set; } = "";
    public string Story4Description { get; set; } = "";
    public string Story4ImageUrl { get; set; } = "";
    public string Story4VideoUrl { get; set; } = "";
    public string Story4LinkUrl { get; set; } = "";
}
