using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Plugin.Widgets.JoinCta.Models;
using Nop.Services.Configuration;
using Nop.Web.Framework.Components;

namespace Nop.Plugin.Widgets.JoinCta.Components;

public class JoinCtaViewComponent : NopViewComponent
{
    private readonly ISettingService _settingService;
    private readonly IStoreContext _storeContext;

    public JoinCtaViewComponent(ISettingService settingService, IStoreContext storeContext)
    {
        _settingService = settingService;
        _storeContext = storeContext;
    }

    public async Task<IViewComponentResult> InvokeAsync(string widgetZone, object additionalData)
    {
        var store = await _storeContext.GetCurrentStoreAsync();
        var s = await _settingService.LoadSettingAsync<JoinCtaSettings>(store.Id);

        var bankAccounts = new List<BankAccountModel>();
        if (!string.IsNullOrWhiteSpace(s.Bank1Iban))
            bankAccounts.Add(new BankAccountModel { Currency = s.Bank1Currency, BankName = s.Bank1Name, Iban = s.Bank1Iban });
        if (!string.IsNullOrWhiteSpace(s.Bank2Iban))
            bankAccounts.Add(new BankAccountModel { Currency = s.Bank2Currency, BankName = s.Bank2Name, Iban = s.Bank2Iban });
        if (!string.IsNullOrWhiteSpace(s.Bank3Iban))
            bankAccounts.Add(new BankAccountModel { Currency = s.Bank3Currency, BankName = s.Bank3Name, Iban = s.Bank3Iban });

        var model = new PublicModel
        {
            Title             = s.Title,
            BankSectionTitle  = s.BankSectionTitle,
            BankAccounts      = bankAccounts,
            DonateTitle       = s.DonateTitle,
            DonateDescription = s.DonateDescription,
            DonateLinkText    = s.DonateLinkText,
            DonateUrl         = s.DonateUrl,
            ContactTitle      = s.ContactTitle,
            ContactDescription= s.ContactDescription,
            ContactLinkText   = s.ContactLinkText
        };

        return View("~/Plugins/Widgets.JoinCta/Views/Components/JoinCta/Default.cshtml", model);
    }
}
