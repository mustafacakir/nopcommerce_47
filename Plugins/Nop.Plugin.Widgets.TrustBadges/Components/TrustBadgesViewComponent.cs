using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Plugin.Widgets.TrustBadges.Models;
using Nop.Services.Configuration;
using Nop.Web.Framework.Components;

namespace Nop.Plugin.Widgets.TrustBadges.Components;

public class TrustBadgesViewComponent : NopViewComponent
{
    private readonly ISettingService _settingService;
    private readonly IStoreContext _storeContext;

    public TrustBadgesViewComponent(ISettingService settingService, IStoreContext storeContext)
    {
        _settingService = settingService;
        _storeContext = storeContext;
    }

    public async Task<IViewComponentResult> InvokeAsync(string widgetZone, object? additionalData = null)
    {
        var store = await _storeContext.GetCurrentStoreAsync();
        var s = await _settingService.LoadSettingAsync<TrustBadgesSettings>(store.Id);

        var badges = new List<BadgeItem>
        {
            new() { Icon = s.Badge1Icon, Title = s.Badge1Title, Subtitle = s.Badge1Subtitle },
            new() { Icon = s.Badge2Icon, Title = s.Badge2Title, Subtitle = s.Badge2Subtitle },
            new() { Icon = s.Badge3Icon, Title = s.Badge3Title, Subtitle = s.Badge3Subtitle },
            new() { Icon = s.Badge4Icon, Title = s.Badge4Title, Subtitle = s.Badge4Subtitle },
            new() { Icon = s.Badge5Icon, Title = s.Badge5Title, Subtitle = s.Badge5Subtitle },
        }.Where(b => !string.IsNullOrWhiteSpace(b.Title)).ToList();

        return View("~/Plugins/Widgets.TrustBadges/Views/Components/TrustBadges/Default.cshtml",
            new PublicInfoModel { Badges = badges });
    }
}
