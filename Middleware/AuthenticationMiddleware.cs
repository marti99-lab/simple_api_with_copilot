
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

public class AuthenticationMiddleware
{
    private readonly RequestDelegate _next;

    public AuthenticationMiddleware(RequestDelegate next) => _next = next;

    public async Task Invoke(HttpContext context)
    {
        if (!context.Request.Headers.ContainsKey("Authorization"))
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Unauthorized");
            return;
        }

        var token = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
        if (!IsValidToken(token))
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Invalid token");
            return;
        }

        await _next(context);
    }

    private bool IsValidToken(string token) => token == "valid_token"; // Replace with actual validation logic
}
    