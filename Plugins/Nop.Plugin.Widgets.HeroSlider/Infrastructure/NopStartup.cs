using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nop.Core.Infrastructure;
using Nop.Plugin.Widgets.HeroSlider.Services;

namespace Nop.Plugin.Widgets.HeroSlider.Infrastructure;

public class NopStartup : INopStartup
{
    public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        => services.AddScoped<IHeroSliderService, HeroSliderService>();

    public void Configure(IApplicationBuilder app) { }
    public int Order => 3000;
}
