using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nop.Core.Infrastructure;

namespace Nop.Plugin.Misc.SimpleCheckout.Infrastructure;

public class NopStartup : INopStartup
{
    public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<SimpleCheckoutRedirectFilter>();
        services.Configure<MvcOptions>(options =>
        {
            options.Filters.AddService<SimpleCheckoutRedirectFilter>();
        });
    }

    public void Configure(IApplicationBuilder application) { }

    public int Order => 3000;
}
