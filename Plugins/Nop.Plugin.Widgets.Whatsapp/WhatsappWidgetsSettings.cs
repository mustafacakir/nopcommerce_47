using Nop.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nop.Plugin.Widgets.Whatsapp
{
    public class WhatsappWidgetsSettings : ISettings
    {
        public string PhoneNumber { get; set; }
        public int IconWidthAndHeight { get; set; }
    }
}
