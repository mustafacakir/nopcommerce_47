using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Plugin.Widgets.ProjectStories.Models;
using Nop.Services.Configuration;
using Nop.Web.Framework.Components;

namespace Nop.Plugin.Widgets.ProjectStories.Components;

public class ProjectStoriesViewComponent : NopViewComponent
{
    private readonly ISettingService _settingService;
    private readonly IStoreContext _storeContext;

    public ProjectStoriesViewComponent(ISettingService settingService, IStoreContext storeContext)
    {
        _settingService = settingService;
        _storeContext = storeContext;
    }

    public async Task<IViewComponentResult> InvokeAsync(string widgetZone, object? additionalData = null)
    {
        var store = await _storeContext.GetCurrentStoreAsync();
        var s = await _settingService.LoadSettingAsync<ProjectStoriesSettings>(store.Id);

        var raw = new[]
        {
            (s.Story1Title, s.Story1Tag, s.Story1Description, s.Story1ImageUrl, s.Story1VideoUrl, s.Story1LinkUrl),
            (s.Story2Title, s.Story2Tag, s.Story2Description, s.Story2ImageUrl, s.Story2VideoUrl, s.Story2LinkUrl),
            (s.Story3Title, s.Story3Tag, s.Story3Description, s.Story3ImageUrl, s.Story3VideoUrl, s.Story3LinkUrl),
            (s.Story4Title, s.Story4Tag, s.Story4Description, s.Story4ImageUrl, s.Story4VideoUrl, s.Story4LinkUrl),
        };

        var stories = raw
            .Where(x => !string.IsNullOrWhiteSpace(x.Item1))
            .Select(x => new StoryCard
            {
                Title = x.Item1,
                Tag = x.Item2,
                Description = x.Item3,
                ImageUrl = x.Item4,
                VideoUrl = x.Item5,
                LinkUrl = x.Item6
            })
            .ToList();

        return View("~/Plugins/Widgets.ProjectStories/Views/Components/ProjectStories/Default.cshtml",
            new PublicInfoModel
            {
                SectionTitle = s.SectionTitle,
                SectionSubtitle = s.SectionSubtitle,
                Stories = stories
            });
    }
}
