using Autofac.Extensions.DependencyInjection;
using Nop.Core.Configuration;
using Nop.Core.Infrastructure;
using Nop.Web.Framework.Infrastructure.Extensions;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace Nop.Web;

public partial class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Configuration.AddJsonFile(NopConfigurationDefaults.AppSettingsFilePath, true, true);
        if (!string.IsNullOrEmpty(builder.Environment?.EnvironmentName))
        {
            var path = string.Format(NopConfigurationDefaults.AppSettingsEnvironmentFilePath, builder.Environment.EnvironmentName);
            builder.Configuration.AddJsonFile(path, true, true);
        }
        builder.Configuration.AddEnvironmentVariables();

        //load application settings
        builder.Services.ConfigureApplicationSettings(builder);

        var appSettings = Singleton<AppSettings>.Instance;
        var useAutofac = appSettings.Get<CommonConfig>().UseAutofac;

        if (useAutofac)
            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
        else
            builder.Host.UseDefaultServiceProvider(options =>
            {
                //we don't validate the scopes, since at the app start and the initial configuration we need 
                //to resolve some services (registered as "scoped") through the root container
                options.ValidateScopes = false;
                options.ValidateOnBuild = true;
            });

        //add services to the application and configure service provider
        builder.Services.ConfigureApplicationServices(builder);

        // PEKIN_CUSTOM: JSON console logging — Promtail store_id label'ını parse edebilsin
        builder.Logging.AddJsonConsole(opts =>
        {
            opts.IncludeScopes   = true;
            opts.TimestampFormat = "yyyy-MM-ddTHH:mm:ss.fffZ";
        });

        // PEKIN_CUSTOM: OpenTelemetry metrikleri Prometheus için eklendi
        builder.Services.AddMetrics(); // IMeterFactory'yi DI'a kayıt et
        //add OpenTelemetry
        builder.Services.AddOpenTelemetry()
            .ConfigureResource(r => r.AddService("nopcommerce"))
            .WithMetrics(metrics => metrics
                .AddAspNetCoreInstrumentation()
                .AddHttpClientInstrumentation()
                .AddRuntimeInstrumentation()
                .AddMeter("nopcommerce.stores")   // per-store custom metrics
                .AddPrometheusExporter())
            .WithTracing(tracing => tracing
                .AddAspNetCoreInstrumentation()
                .AddHttpClientInstrumentation());

        var app = builder.Build();

        //configure the application HTTP request pipeline
        app.ConfigureRequestPipeline();
        await app.StartEngineAsync();

        // PEKIN_CUSTOM: /metrics endpoint Prometheus scraping için açıldı
        //expose Prometheus metrics endpoint
        app.MapPrometheusScrapingEndpoint("/metrics");

        await app.RunAsync();
    }
}