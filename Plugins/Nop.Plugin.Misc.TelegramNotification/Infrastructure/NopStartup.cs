using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nop.Core.Infrastructure;
using Nop.Plugin.Misc.TelegramNotification.Services;

namespace Nop.Plugin.Misc.TelegramNotification.Infrastructure;

public class NopStartup : INopStartup
{
    public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient();
        services.AddScoped<TelegramService>();
    }

    public void Configure(IApplicationBuilder application) { }

    public int Order => 3002;
}
