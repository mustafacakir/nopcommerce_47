using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nop.Core.Infrastructure;
using Nop.Plugin.Misc.KurbanOrganization.Services;

namespace Nop.Plugin.Misc.KurbanOrganization.Infrastructure;

public class NopStartup : INopStartup
{
    public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient();
        services.AddScoped<KurbanService>();
    }

    public void Configure(IApplicationBuilder application) { }

    public int Order => 3001;
}
