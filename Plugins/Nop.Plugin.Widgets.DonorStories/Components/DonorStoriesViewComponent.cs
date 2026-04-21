using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Plugin.Widgets.DonorStories.Models;
using Nop.Services.Configuration;
using Nop.Web.Framework.Components;

namespace Nop.Plugin.Widgets.DonorStories.Components;

public class DonorStoriesViewComponent : NopViewComponent
{
    private readonly ISettingService _settingService;
    private readonly IStoreContext _storeContext;

    public DonorStoriesViewComponent(ISettingService settingService, IStoreContext storeContext)
    {
        _settingService = settingService;
        _storeContext = storeContext;
    }

    public async Task<IViewComponentResult> InvokeAsync(string widgetZone, object? additionalData = null)
    {
        var store = await _storeContext.GetCurrentStoreAsync();
        var s = await _settingService.LoadSettingAsync<DonorStoriesSettings>(store.Id);

        var rawStories = new[]
        {
            (s.Story1Name, s.Story1Location, s.Story1Quote, s.Story1Avatar),
            (s.Story2Name, s.Story2Location, s.Story2Quote, s.Story2Avatar),
            (s.Story3Name, s.Story3Location, s.Story3Quote, s.Story3Avatar),
            (s.Story4Name, s.Story4Location, s.Story4Quote, s.Story4Avatar),
        };

        var stories = rawStories
            .Where(x => !string.IsNullOrWhiteSpace(x.Item1) && !string.IsNullOrWhiteSpace(x.Item3))
            .Select(x => new StoryItem
            {
                Name = x.Item1,
                Location = x.Item2,
                Quote = x.Item3,
                Avatar = x.Item4,
                Initials = GetInitials(x.Item1)
            })
            .ToList();

        return View("~/Plugins/Widgets.DonorStories/Views/Components/DonorStories/Default.cshtml",
            new PublicInfoModel { SectionTitle = s.SectionTitle, Stories = stories });
    }

    private static string GetInitials(string name)
    {
        var parts = name.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        return parts.Length >= 2
            ? $"{parts[0][0]}{parts[1][0]}"
            : parts.Length == 1 ? parts[0][0].ToString() : "?";
    }
}
