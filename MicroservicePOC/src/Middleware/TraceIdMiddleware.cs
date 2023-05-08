using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using OpenTelemetry.Trace;

namespace MicroservicePOC.Middleware
{
    public class TraceIdMiddleware
    {
        private readonly RequestDelegate _next;

        public TraceIdMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Get the current activity (represents the ongoing trace)
            var activity = Activity.Current;

            // If there is an activity, set the response header with the trace ID
            if (activity != null)
            {
                context.Response.OnStarting(() =>
                {
                    // trace ID
                    context.Response.Headers["X-Trace-Id"] = activity.TraceId.ToString();
                    // baggage
                    foreach (var baggageItem in activity.Baggage)
                    {
                        context.Response.Headers[$"X-Trace-{baggageItem.Key}"] = baggageItem.Value;
                    }
                    return Task.CompletedTask;
                });
            }

            // Call the next middleware in the pipeline
            await _next(context);
        }
    }
}
