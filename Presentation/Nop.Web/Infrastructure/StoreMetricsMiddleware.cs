using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Nop.Core;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Threading.Tasks;

// PEKIN_CUSTOM: Per-store metrics ve log context — Prometheus + Loki store bazlı filtreleme için
namespace Nop.Web.Infrastructure
{
    public class StoreMetricsMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<StoreMetricsMiddleware> _logger;

        private static Counter<long>     _requestCounter;
        private static Counter<long>     _errorCounter;
        private static Histogram<double> _requestDuration;
        private static bool _initialized = false;
        private static readonly object _lock = new();

        // Statik dosyaları metriklere dahil etme (yüksek kardinalite önleme)
        private static readonly string[] _staticPrefixes = ["/css", "/js", "/images", "/fonts", "/lib", "/content", "/bundles"];
        private static readonly string[] _staticExtensions = [".ico", ".png", ".jpg", ".jpeg", ".gif", ".svg", ".woff", ".woff2", ".ttf", ".eot", ".map", ".webp"];

        public StoreMetricsMiddleware(RequestDelegate next, ILogger<StoreMetricsMiddleware> logger)
        {
            _next   = next;
            _logger = logger;
        }

        private static void EnsureInitialized(System.IServiceProvider services)
        {
            if (_initialized) return;
            lock (_lock)
            {
                if (_initialized) return;
                var factory = services.GetRequiredService<IMeterFactory>();
                var meter   = factory.Create("nopcommerce.stores", "1.0");
                _requestCounter  = meter.CreateCounter<long>("store_requests_total",        description: "Total HTTP requests per store");
                _errorCounter    = meter.CreateCounter<long>("store_errors_total",          description: "Total HTTP 4xx/5xx errors per store");
                _requestDuration = meter.CreateHistogram<double>("store_request_duration_ms", unit: "ms", description: "HTTP request duration per store");
                _initialized = true;
            }
        }

        public async Task InvokeAsync(HttpContext context, IStoreContext storeContext)
        {
            if (!Nop.Data.DataSettingsManager.IsDatabaseInstalled())
            {
                await _next(context);
                return;
            }

            var path = context.Request.Path.Value ?? "/";

            // Statik dosyalar için metrics kaydı yapma
            if (IsStaticFile(path))
            {
                await _next(context);
                return;
            }

            EnsureInitialized(context.RequestServices);

            var store     = await storeContext.GetCurrentStoreAsync();
            var storeId   = store?.Id.ToString() ?? "0";
            var storeName = store?.Name ?? "unknown";
            var method    = context.Request.Method;

            // Path normalizasyonu: query string olmadan, küçük harf
            var normalizedPath = path.ToLowerInvariant().TrimEnd('/');
            if (string.IsNullOrEmpty(normalizedPath)) normalizedPath = "/";

            var tags = new TagList
            {
                { "store_id",   storeId        },
                { "store_name", storeName      },
                { "method",     method         },
                { "path",       normalizedPath }
            };

            using (_logger.BeginScope(new Dictionary<string, object>
            {
                ["store_id"]   = storeId,
                ["store_name"] = storeName,
                ["path"]       = normalizedPath,
                ["method"]     = method
            }))
            {
                _requestCounter.Add(1, tags);
                var sw = Stopwatch.StartNew();
                try
                {
                    await _next(context);
                }
                catch
                {
                    _errorCounter.Add(1, tags);
                    throw;
                }
                finally
                {
                    sw.Stop();
                    _requestDuration.Record(sw.Elapsed.TotalMilliseconds, tags);

                    var status = context.Response.StatusCode;
                    if (status >= 400)
                        _errorCounter.Add(1, tags);
                }
            }
        }

        private static bool IsStaticFile(string path)
        {
            foreach (var prefix in _staticPrefixes)
                if (path.StartsWith(prefix, System.StringComparison.OrdinalIgnoreCase))
                    return true;

            foreach (var ext in _staticExtensions)
                if (path.EndsWith(ext, System.StringComparison.OrdinalIgnoreCase))
                    return true;

            return false;
        }
    }
}
