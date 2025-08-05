using MediTrack.Services;
using Microsoft.EntityFrameworkCore;

namespace MediTrack.Middleware;

public class AuthMiddleware
{
    private readonly RequestDelegate _next;

    public AuthMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context, AuthService authService)
    {
        var token = context.Request.Query["token"].FirstOrDefault();

        if (!string.IsNullOrWhiteSpace(token))
        {
            try
            {
                var user = authService.ValidateToken(token);
                context.Items["User"] = user;
            }
            catch (UnauthorizedException)
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Unauthorized");
                return;
            }
        }

        await _next(context);
    }
}

