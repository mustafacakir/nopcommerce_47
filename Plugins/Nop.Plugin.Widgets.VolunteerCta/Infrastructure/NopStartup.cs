using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nop.Core.Infrastructure;
using Nop.Plugin.Widgets.VolunteerCta.Services;

namespace Nop.Plugin.Widgets.VolunteerCta.Infrastructure;

public class NopStartup : INopStartup
{
    public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<GonulluService>();
    }

    public void Configure(IApplicationBuilder application) { }

    public int Order => 3002;
}
