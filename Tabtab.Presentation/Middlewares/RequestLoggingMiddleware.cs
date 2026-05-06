using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tabtaba.Presentation.Middlewares;

public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLoggingMiddleware> _logger;

    public RequestLoggingMiddleware(
        RequestDelegate next,
        ILogger<RequestLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var start = DateTime.UtcNow;

        await _next(context);

        var duration = DateTime.UtcNow - start;
        var statusCode = context.Response.StatusCode;

        //  Failed Login
        if (context.Request.Path.StartsWithSegments("/api/auth/login") &&
            statusCode == 401)
        {
            _logger.LogWarning(
                "Failed login attempt from IP: {IP} at {Time}",
                context.Connection.RemoteIpAddress,
                DateTime.UtcNow);
        }

        //  Rate Limit
        if (statusCode == 429)
        {
            _logger.LogWarning(
                "Rate limit exceeded from IP: {IP} at {Time}",
                context.Connection.RemoteIpAddress,
                DateTime.UtcNow);
        }

        //  Server Errors
        if (statusCode >= 500)
        {
            _logger.LogError(
                "Server error {StatusCode} on {Method} {Path} - Duration: {Duration}ms",
                statusCode,
                context.Request.Method,
                context.Request.Path,
                duration.TotalMilliseconds);
        }

        //  All Requests
        _logger.LogInformation(
            "{Method} {Path} → {StatusCode} ({Duration}ms)",
            context.Request.Method,
            context.Request.Path,
            statusCode,
            duration.TotalMilliseconds);
    }
}