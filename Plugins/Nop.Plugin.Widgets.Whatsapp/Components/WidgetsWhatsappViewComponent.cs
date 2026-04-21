using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Plugin.Widgets.Whatsapp.Models;
using Nop.Services.Configuration;
using Nop.Web.Framework.Components;

namespace Nop.Plugin.Widgets.Whatsapp.Components;

[ViewComponent(Name = "WidgetsWhatsapp")]
public class WidgetsWhatsappViewComponent : NopViewComponent
{
    private readonly IStoreContext _storeContext;
    private readonly ISettingService _settingService;

    public WidgetsWhatsappViewComponent(IStoreContext storeContext, ISettingService settingService)
    {
        _storeContext = storeContext;
        _settingService = settingService;
    }

    public async Task<IViewComponentResult> InvokeAsync(string widgetZone, object additionalData)
    {
        var store = await _storeContext.GetCurrentStoreAsync();
        var settings = await _settingService.LoadSettingAsync<WhatsappWidgetsSettings>(store.Id);

        var model = new PublicInfoModel
        {
            PhoneNumber = settings.PhoneNumber,
            IconWidthAndHeight = settings.IconWidthAndHeight
        };

        return View("~/Plugins/Widgets.Whatsapp/Views/PublicInfo.cshtml", model);
    }
}
