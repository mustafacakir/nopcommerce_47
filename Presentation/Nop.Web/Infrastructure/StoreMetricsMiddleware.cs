using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Nop.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Threading.Tasks;

// PEKIN_CUSTOM: Per-store metrics ve log context — Prometheus + Loki store bazlı filtreleme için
namespace Nop.Web.Infrastructure
{
    /// <summary>
    /// Her HTTP request'e store_id / store_name ekler:
    ///   - Log scope  → Loki'de store bazlı filtreleme
    ///   - Metrics    → Prometheus'ta store bazlı request sayısı ve süre
    /// </summary>
    public class StoreMetricsMiddleware
    {
        private static readonly Meter _meter = new("nopcommerce.stores", "1.0");

        private static readonly Counter<long> _requestCounter = _meter.CreateCounter<long>(
            "store_requests_total",
            description: "Total HTTP requests per store");

        private static readonly Counter<long> _errorCounter = _meter.CreateCounter<long>(
            "store_errors_total",
            description: "Total HTTP 5xx errors per store");

        private static readonly Histogram<double> _requestDuration = _meter.CreateHistogram<double>(
            "store_request_duration_ms",
            unit: "ms",
            description: "HTTP request duration per store");

        private readonly RequestDelegate _next;
        private readonly ILogger<StoreMetricsMiddleware> _logger;

        public StoreMetricsMiddleware(RequestDelegate next, ILogger<StoreMetricsMiddleware> logger)
        {
            _next   = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, IStoreContext storeContext)
        {
            // DB kurulmadan önce store context çalışmaz
            if (!Nop.Data.DataSettingsManager.IsDatabaseInstalled())
            {
                await _next(context);
                return;
            }

            var store = await storeContext.GetCurrentStoreAsync();
            var storeId   = store?.Id.ToString() ?? "0";
            var storeName = store?.Name ?? "unknown";

            var tags = new TagList
            {
                { "store_id",   storeId   },
                { "store_name", storeName }
            };

            // Log scope — tüm bu request'in logları store bilgisini taşır
            using (_logger.BeginScope(new Dictionary<string, object>
            {
                ["store_id"]   = storeId,
                ["store_name"] = storeName
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

                    if (context.Response.StatusCode >= 500)
                        _errorCounter.Add(1, tags);
                }
            }
        }
    }
}
