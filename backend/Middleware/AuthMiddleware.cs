using MediTrack.Services;
using Microsoft.EntityFrameworkCore;

namespace MediTrack;

public class AuthMiddleware
{
    private readonly RequestDelegate _next;

    public AuthMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    private readonly AuthService _authService;

    public AuthMiddleware(AuthService authService)
    {
        _authService = authService;
    }
    public async Task Invoke(HttpContext context)
    {
        var path = context.Request.Path.Value?.ToLower();

        var allowedPaths = new[]
        {
            "/api/auth/login",
            "/api/auth/fetch",
            "/swagger",
            "/swagger/index.html",
            "/swagger/v1/swagger.json",
            "/favicon.ico",
            "/api/users/register"
        };

        if (allowedPaths.Any(p => path.StartsWith(p)))
        {
            await _next(context);
            return;
        }
        var token = context.Request.Headers["Authentication"].FirstOrDefault();
        if (string.IsNullOrEmpty(token))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("Missing authentication token");
            return;
        }
        try
        {
            var user = _authService.ValidateToken(token);
            context.Items["User"] = user; // сохраняем пользователя в контекст
        }
        catch (UnauthorizedException)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("Invalid token");
            return;
        }

    }
}
