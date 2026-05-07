using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Nop.Core;
using Nop.Services.Configuration;

namespace Nop.Plugin.Misc.SimpleCheckout.Infrastructure;

public class SimpleCheckoutRedirectFilter : IAsyncActionFilter
{
    private readonly IStoreContext _storeContext;
    private readonly ISettingService _settingService;

    public SimpleCheckoutRedirectFilter(IStoreContext storeContext, ISettingService settingService)
    {
        _storeContext = storeContext;
        _settingService = settingService;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var controller = context.RouteData.Values["controller"]?.ToString();
        var action = context.RouteData.Values["action"]?.ToString();

        if (string.Equals(controller, "Checkout", StringComparison.OrdinalIgnoreCase) &&
            string.Equals(action, "OnePageCheckout", StringComparison.OrdinalIgnoreCase))
        {
            var store = await _storeContext.GetCurrentStoreAsync();
            var settings = await _settingService.LoadSettingAsync<SimpleCheckoutSettings>(store.Id);

            if (IsDonationStore(settings, store.Id))
            {
                context.Result = new RedirectResult("/checkout/bagis");
                return;
            }
        }

        await next();
    }

    private static bool IsDonationStore(SimpleCheckoutSettings settings, int storeId)
    {
        if (string.IsNullOrWhiteSpace(settings.DonationStoreIds))
            return false;

        return settings.DonationStoreIds
            .Split(',', StringSplitOptions.RemoveEmptyEntries)
            .Select(s => s.Trim())
            .Any(id => id == storeId.ToString());
    }
}
