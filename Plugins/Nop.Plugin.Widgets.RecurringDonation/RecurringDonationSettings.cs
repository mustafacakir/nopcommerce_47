using Nop.Core.Configuration;

namespace Nop.Plugin.Widgets.RecurringDonation;

public class RecurringDonationSettings : ISettings
{
    public string Title { get; set; } = "Düzenli Bağışçı Ol";
    public string Subtitle { get; set; } = "Her ay düzenli bağış yaparak sürekli bir iyilik zincirinin parçası olun. Aylık bağışınız ihtiyaç sahiplerine kesintisiz destek sağlar.";

    public string Amount1Label { get; set; } = "50₺ / ay";
    public string Amount1Url { get; set; } = "";

    public string Amount2Label { get; set; } = "100₺ / ay";
    public string Amount2Url { get; set; } = "";

    public string Amount3Label { get; set; } = "250₺ / ay";
    public string Amount3Url { get; set; } = "";

    public string Amount4Label { get; set; } = "500₺ / ay";
    public string Amount4Url { get; set; } = "";

    public string CustomAmountUrl { get; set; } = "";
}
