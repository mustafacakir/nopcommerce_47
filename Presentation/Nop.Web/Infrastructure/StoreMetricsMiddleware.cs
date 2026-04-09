using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Diagnostics.Metrics;
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
    public class StoreMetricsMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<StoreMetricsMiddleware> _logger;
        private readonly Counter<long> _requestCounter;
        private readonly Counter<long> _errorCounter;
        private readonly Histogram<double> _requestDuration;

        public StoreMetricsMiddleware(
            RequestDelegate next,
            ILogger<StoreMetricsMiddleware> logger,
            IMeterFactory meterFactory)
        {
            _next   = next;
            _logger = logger;

            var meter = meterFactory.Create("nopcommerce.stores", "1.0");

            _requestCounter = meter.CreateCounter<long>(
                "store_requests_total",
                description: "Total HTTP requests per store");

            _errorCounter = meter.CreateCounter<long>(
                "store_errors_total",
                description: "Total HTTP 5xx errors per store");

            _requestDuration = meter.CreateHistogram<double>(
                "store_request_duration_ms",
                unit: "ms",
                description: "HTTP request duration per store");
        }

        public async Task InvokeAsync(HttpContext context, IStoreContext storeContext)
        {
            if (!Nop.Data.DataSettingsManager.IsDatabaseInstalled())
            {
                await _next(context);
                return;
            }

            var store     = await storeContext.GetCurrentStoreAsync();
            var storeId   = store?.Id.ToString() ?? "0";
            var storeName = store?.Name ?? "unknown";

            var tags = new TagList
            {
                { "store_id",   storeId   },
                { "store_name", storeName }
            };

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
