using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Tabtaba.Presentation.Middlewares;

public class SanitizationMiddleware
{
    private readonly RequestDelegate _next;

    public SanitizationMiddleware(RequestDelegate next)
        => _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
        //  POST و PUT
        if (context.Request.Method is "POST" or "PUT" &&
            context.Request.ContentType?.Contains("application/json") == true)
        {
            context.Request.EnableBuffering();

            var body = await new StreamReader(context.Request.Body, Encoding.UTF8,
                leaveOpen: true).ReadToEndAsync();

            context.Request.Body.Position = 0;

            if (!string.IsNullOrWhiteSpace(body))
            {
                var sanitized = SanitizeJson(body);

                var bytes = Encoding.UTF8.GetBytes(sanitized);
                context.Request.Body = new MemoryStream(bytes);
                context.Request.ContentLength = bytes.Length;
            }
        }

        await _next(context);
    }

    private static string SanitizeJson(string json)
    {
        try
        {
            using var doc = JsonDocument.Parse(json);
            var sanitizedDict = SanitizeElement(doc.RootElement);
            return JsonSerializer.Serialize(sanitizedDict);
        }
        catch
        {
            return json;
        }
    }

    private static object? SanitizeElement(JsonElement element)
    {
        return element.ValueKind switch
        {
            JsonValueKind.Object => element.EnumerateObject()
                .ToDictionary(p => p.Name, p => SanitizeElement(p.Value)),

            JsonValueKind.Array => element.EnumerateArray()
                .Select(SanitizeElement).ToList(),

            JsonValueKind.String => SanitizeString(element.GetString() ?? string.Empty),

            _ => JsonSerializer.Deserialize<object>(element.GetRawText())
        };
    }

    private static string SanitizeString(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return input;

        //  XSS
        input = Regex.Replace(input, @"<.*?>", string.Empty);
        input = input.Replace("&", "&amp;")
                     .Replace("<", "&lt;")
                     .Replace(">", "&gt;")
                     .Replace("\"", "&quot;")
                     .Replace("'", "&#x27;");

        //   SQL Injection
        var sqlKeywords = new[]
        {
            "SELECT", "INSERT", "UPDATE", "DELETE", "DROP",
            "UNION", "EXEC", "EXECUTE", "--", ";--", "/*", "*/"
        };

        foreach (var keyword in sqlKeywords)
        {
            input = Regex.Replace(input, keyword, string.Empty,
                RegexOptions.IgnoreCase);
        }

        return input.Trim();
    }
}