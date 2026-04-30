using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nop.Core.Infrastructure;
using Nop.Plugin.Widgets.KumbaraForm.Services;

namespace Nop.Plugin.Widgets.KumbaraForm.Infrastructure;

public class NopStartup : INopStartup
{
    public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IKumbaraService, KumbaraService>();
    }

    public void Configure(IApplicationBuilder application) { }

    public int Order => 3000;
}
