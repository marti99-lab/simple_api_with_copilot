
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;

public class LoggingMiddleware
{
    private readonly RequestDelegate _next;
    public LoggingMiddleware(RequestDelegate next) => _next = next;

    public async Task Invoke(HttpContext context)
    {
        Debug.WriteLine($"Request: {context.Request.Method} {context.Request.Path}");
        foreach (var header in context.Request.Headers)
        {
            Debug.WriteLine($"Header: {header.Key} = {header.Value}");
        }
        await _next(context);
        Debug.WriteLine($"Response: {context.Response.StatusCode}");
    }
}
    