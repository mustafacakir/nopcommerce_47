using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Nop.Plugin.Misc.PluginManager.Models;
using Nop.Services.Stores;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc.Filters;
using System.Text.Json;

namespace Nop.Plugin.Misc.PluginManager.Controllers;

[AuthorizeAdmin]
[Area(AreaNames.ADMIN)]
public class PluginManagerController : BasePluginController
{
    private readonly IWebHostEnvironment _env;
    private readonly IStoreService _storeService;

    public PluginManagerController(IWebHostEnvironment env, IStoreService storeService)
    {
        _env = env;
        _storeService = storeService;
    }

    public async Task<IActionResult> Index()
    {
        var pluginsPath = Path.Combine(_env.ContentRootPath, "Plugins");
        var plugins = new List<PluginStoreEntry>();

        foreach (var file in Directory.GetFiles(pluginsPath, "plugin.json", SearchOption.AllDirectories))
        {
            try
            {
                var json = await System.IO.File.ReadAllTextAsync(file);
                var descriptor = JsonSerializer.Deserialize<PluginDescriptorRaw>(json);
                if (descriptor == null) continue;

                plugins.Add(new PluginStoreEntry
                {
                    SystemName      = descriptor.SystemName,
                    FriendlyName    = descriptor.FriendlyName,
                    Group           = descriptor.Group,
                    LimitedToStores = descriptor.LimitedToStores ?? new(),
                    FilePath        = file
                });
            }
            catch { }
        }

        var stores = await _storeService.GetAllStoresAsync();
        var model = new PluginManagerIndexModel
        {
            Plugins = plugins.OrderBy(p => p.Group).ThenBy(p => p.FriendlyName).ToList(),
            Stores  = stores.Select(s => new StoreEntry { Id = s.Id, Name = s.Name }).ToList()
        };

        return View("~/Plugins/Misc.PluginManager/Views/Index.cshtml", model);
    }

    [HttpPost]
    [IgnoreAntiforgeryToken]
    public async Task<IActionResult> SavePlugin([FromBody] SavePluginRequest request)
    {
        if (string.IsNullOrEmpty(request?.FilePath))
            return BadRequest();

        var pluginsPath = Path.Combine(_env.ContentRootPath, "Plugins");
        var fullPath = Path.GetFullPath(request.FilePath);

        if (!fullPath.StartsWith(pluginsPath, StringComparison.OrdinalIgnoreCase))
            return BadRequest("Invalid path");

        var json = await System.IO.File.ReadAllTextAsync(fullPath);
        var options = new JsonSerializerOptions { WriteIndented = true };
        var descriptor = JsonSerializer.Deserialize<PluginDescriptorRaw>(json);
        if (descriptor == null)
            return BadRequest();

        descriptor.LimitedToStores = request.StoreIds ?? new();
        await System.IO.File.WriteAllTextAsync(fullPath, JsonSerializer.Serialize(descriptor, options));

        return Ok(new { success = true });
    }
}
