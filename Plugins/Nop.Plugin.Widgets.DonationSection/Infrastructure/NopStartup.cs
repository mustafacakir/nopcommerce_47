using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nop.Core.Infrastructure;
using Nop.Plugin.Widgets.DonationSection.Services;

namespace Nop.Plugin.Widgets.DonationSection.Infrastructure;

public class NopStartup : INopStartup
{
    public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IDonationSectionService, DonationSectionService>();
    }
    public void Configure(IApplicationBuilder app) { }
    public int Order => 3000;
}
