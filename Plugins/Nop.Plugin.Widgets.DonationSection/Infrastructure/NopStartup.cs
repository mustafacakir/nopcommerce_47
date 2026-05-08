using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nop.Core.Infrastructure;

namespace Nop.Plugin.Widgets.DonationSection.Infrastructure;

public class NopStartup : INopStartup
{
    public void ConfigureServices(IServiceCollection services, IConfiguration configuration) { }
    public void Configure(IApplicationBuilder app) { }
    public int Order => 3000;
}
