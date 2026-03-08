using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Core.Http.Extensions;
using Nop.Plugin.Payments.Iyzipay.Models;
using Nop.Services.Localization;
using Nop.Services.Orders;
using Nop.Web.Framework.Components;
using Nop.Core;
using Nop.Services.Payments;
namespace Nop.Plugin.Payments.Iyzipay.Components;

[ViewComponent(Name = "PaymentIyzipay")]
public class PaymentInfoViewComponent : NopViewComponent
{

    #region Fields

    private readonly ILocalizationService _localizationService;
    private readonly IyzipayPaymentSettings _iyzipayPaymentSettings;
    private readonly IWorkContext _workContext;
    private readonly IOrderProcessingService _orderProcessingService;

    #endregion

    #region Ctor

    public PaymentInfoViewComponent(ILocalizationService localizationService,
        IyzipayPaymentSettings iyzipayPaymentSettings,
        IWorkContext workContext,
        IOrderProcessingService orderProcessingService)
    {
        _localizationService = localizationService;
        _iyzipayPaymentSettings = iyzipayPaymentSettings;
        _workContext = workContext;
        _orderProcessingService = orderProcessingService;
    }

    #endregion

    #region Methods

    public async Task<IViewComponentResult> InvokeAsync(string widgetZone, object additionalData)
    {
        var orderGuid = Guid.Empty;
        
        var processPaymentRequest = await HttpContext.Session.GetAsync<ProcessPaymentRequest>("OrderPaymentInfo");
        if (processPaymentRequest != null && processPaymentRequest.OrderGuid != Guid.Empty)
        {
            orderGuid = processPaymentRequest.OrderGuid;
        }
        else
        {
            var tempOrderGuid = await HttpContext.Session.GetAsync<byte[]>("IyzipayTempOrderGuid");
            if (tempOrderGuid != null)
            {
               orderGuid = new Guid(tempOrderGuid);
            }
            else
            {
               orderGuid = Guid.NewGuid();
               await HttpContext.Session.SetAsync("IyzipayTempOrderGuid", orderGuid.ToByteArray());
            }
        }

        var model = new PaymentInfoModel
        {
            DescriptionText = await _localizationService.GetResourceAsync("Plugins.Payments.Iyzipay.PaymentDescription"),
            PaymentOptionTitle = await _localizationService.GetResourceAsync("Plugins.Payments.Iyzipay.PaymentOptionTitle"),
            PaymentFormMode = _iyzipayPaymentSettings.PaymentFormMode,
            PaymentFormLanguage = _iyzipayPaymentSettings.PaymentFormLanguage,
            OrderGuid = orderGuid
        };

        return View("~/Plugins/Payments.Iyzipay/Views/PaymentInfo.cshtml", model);
    }

    #endregion
}
