using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Plugin.Widgets.JoinCta.Models;
using Nop.Services.Configuration;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc.Filters;

namespace Nop.Plugin.Widgets.JoinCta.Controllers;

[Area(AreaNames.ADMIN)]
[AuthorizeAdmin]
[AutoValidateAntiforgeryToken]
public class JoinCtaAdminController : BasePluginController
{
    private readonly ISettingService _settingService;
    private readonly IStoreContext _storeContext;

    public JoinCtaAdminController(ISettingService settingService, IStoreContext storeContext)
    {
        _settingService = settingService;
        _storeContext = storeContext;
    }

    public async Task<IActionResult> Configure()
    {
        var store = await _storeContext.GetCurrentStoreAsync();
        var s = await _settingService.LoadSettingAsync<JoinCtaSettings>(store.Id);
        var model = new ConfigurationModel
        {
            Title             = s.Title,
            BankSectionTitle  = s.BankSectionTitle,
            Bank1Currency     = s.Bank1Currency, Bank1Name = s.Bank1Name, Bank1Iban = s.Bank1Iban,
            Bank2Currency     = s.Bank2Currency, Bank2Name = s.Bank2Name, Bank2Iban = s.Bank2Iban,
            Bank3Currency     = s.Bank3Currency, Bank3Name = s.Bank3Name, Bank3Iban = s.Bank3Iban,
            DonateTitle       = s.DonateTitle,   DonateDescription = s.DonateDescription,
            DonateLinkText    = s.DonateLinkText, DonateUrl = s.DonateUrl,
            ContactTitle      = s.ContactTitle,   ContactDescription = s.ContactDescription,
            ContactLinkText   = s.ContactLinkText
        };
        return View("~/Plugins/Widgets.JoinCta/Views/Configure.cshtml", model);
    }

    [HttpPost]
    public async Task<IActionResult> Configure(ConfigurationModel model)
    {
        var store = await _storeContext.GetCurrentStoreAsync();
        var s = await _settingService.LoadSettingAsync<JoinCtaSettings>(store.Id);
        s.Title             = model.Title;
        s.BankSectionTitle  = model.BankSectionTitle;
        s.Bank1Currency     = model.Bank1Currency; s.Bank1Name = model.Bank1Name; s.Bank1Iban = model.Bank1Iban;
        s.Bank2Currency     = model.Bank2Currency; s.Bank2Name = model.Bank2Name; s.Bank2Iban = model.Bank2Iban;
        s.Bank3Currency     = model.Bank3Currency; s.Bank3Name = model.Bank3Name; s.Bank3Iban = model.Bank3Iban;
        s.DonateTitle       = model.DonateTitle;   s.DonateDescription = model.DonateDescription;
        s.DonateLinkText    = model.DonateLinkText; s.DonateUrl = model.DonateUrl;
        s.ContactTitle      = model.ContactTitle;   s.ContactDescription = model.ContactDescription;
        s.ContactLinkText   = model.ContactLinkText;
        await _settingService.SaveSettingAsync(s, store.Id);
        ViewBag.Saved = true;
        return await Configure();
    }
}
