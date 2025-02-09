
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using System.Text.Json;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlingMiddleware(RequestDelegate next) => _next = next;

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            context.Response.StatusCode = 500;
            var result = JsonSerializer.Serialize(new { error = "Internal server error" });
            await context.Response.WriteAsync(result);
        }
    }
}
    