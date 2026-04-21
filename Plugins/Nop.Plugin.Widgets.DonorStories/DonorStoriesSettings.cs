using Nop.Core.Configuration;

namespace Nop.Plugin.Widgets.DonorStories;

public class DonorStoriesSettings : ISettings
{
    public string SectionTitle { get; set; } = "Bağışçılarımız Ne Diyor?";

    public string Story1Name { get; set; } = "Ahmet Y.";
    public string Story1Location { get; set; } = "İstanbul";
    public string Story1Quote { get; set; } = "Bu platformla bağış yapmak çok kolay. Paramın nereye gittiğini görmek beni çok mutlu ediyor.";
    public string Story1Avatar { get; set; } = "";

    public string Story2Name { get; set; } = "Fatma K.";
    public string Story2Location { get; set; } = "Ankara";
    public string Story2Quote { get; set; } = "Her ay düzenli bağış yapıyorum. Hayırlı bir işe ortak olmak paha biçilemez.";
    public string Story2Avatar { get; set; } = "";

    public string Story3Name { get; set; } = "Mehmet S.";
    public string Story3Location { get; set; } = "İzmir";
    public string Story3Quote { get; set; } = "Yurt dışındaki projeleri takip ediyorum. Gerçekten fark yarattıklarını görüyorum.";
    public string Story3Avatar { get; set; } = "";

    public string Story4Name { get; set; } = "";
    public string Story4Location { get; set; } = "";
    public string Story4Quote { get; set; } = "";
    public string Story4Avatar { get; set; } = "";
}
