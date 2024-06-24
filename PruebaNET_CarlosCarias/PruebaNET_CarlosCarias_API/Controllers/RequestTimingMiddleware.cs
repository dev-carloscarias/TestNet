using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace PruebaNET_CarlosCarias_API.Controllers
{
    public class RequestTimingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestTimingMiddleware> _logger;

        public RequestTimingMiddleware(RequestDelegate next, ILogger<RequestTimingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            context.Response.OnStarting(() =>
            {
                stopwatch.Stop();
                var processingTime = stopwatch.ElapsedMilliseconds;
                var logMessage = $"[{DateTime.Now}] {context.Request.Method} {context.Request.Path} responded in {processingTime}ms";

                File.AppendAllText("logs/request_timing.log", logMessage + Environment.NewLine);
                _logger.LogInformation(logMessage);

                return Task.CompletedTask;
            });
            await _next(context);
        }
    }
}
