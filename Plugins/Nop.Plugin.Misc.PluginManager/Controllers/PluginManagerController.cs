using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Plugin.Misc.PluginManager.Models;
using Nop.Services.Customers;
using Nop.Services.Plugins;
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
    private readonly IPluginService _pluginService;
    private readonly IWorkContext _workContext;
    private readonly ICustomerService _customerService;
    private readonly IStoreContext _storeContext;

    public PluginManagerController(
        IWebHostEnvironment env,
        IStoreService storeService,
        IPluginService pluginService,
        IWorkContext workContext,
        ICustomerService customerService,
        IStoreContext storeContext)
    {
        _env = env;
        _storeService = storeService;
        _pluginService = pluginService;
        _workContext = workContext;
        _customerService = customerService;
        _storeContext = storeContext;
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

    public async Task<IActionResult> StoreWidgets()
    {
        var customer = await _workContext.GetCurrentCustomerAsync();
        var isAdmin  = await _customerService.IsAdminAsync(customer);
        var currentStore = await _storeContext.GetCurrentStoreAsync();

        var descriptors = await _pluginService.GetPluginDescriptorsAsync<IPlugin>(
            LoadPluginsMode.InstalledOnly, customer);

        var stores = await _storeService.GetAllStoresAsync();

        var widgets = new List<StoreWidgetEntry>();
        foreach (var d in descriptors.OrderBy(d => d.Group).ThenBy(d => d.FriendlyName))
        {
            // Non-admin kullanıcılar sadece kendi mağazalarına tanımlı widgetları görür
            if (!isAdmin && !d.LimitedToStores.Contains(currentStore.Id))
                continue;

            var configUrl = d.Instance<IPlugin>()?.GetConfigurationPageUrl();
            if (string.IsNullOrEmpty(configUrl))
                continue;

            var storeNames = d.LimitedToStores.Any()
                ? stores.Where(s => d.LimitedToStores.Contains(s.Id)).Select(s => s.Name).ToList()
                : new List<string> { "Tüm Mağazalar" };

            widgets.Add(new StoreWidgetEntry
            {
                SystemName   = d.SystemName,
                FriendlyName = d.FriendlyName,
                Group        = d.Group,
                Description  = d.Description,
                ConfigureUrl = configUrl,
                StoreNames   = storeNames
            });
        }

        return View("~/Plugins/Misc.PluginManager/Views/StoreWidgets.cshtml", widgets);
    }
}
