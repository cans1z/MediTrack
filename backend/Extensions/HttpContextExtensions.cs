using MediTrack.Services;
using MediTrack.Types;

namespace MediTrack.Extensions;

public static class HttpContextExtensions
{
    public static string GetToken(this HttpContext context)
    {
        return context.Request.Headers.Authorization
            .ToString()
            .Replace("Bearer ", "");
    }

    public static bool IsAdmin(this HttpContext context, AuthService authService, out User user)
    {
        var token = context.GetToken();
        user = authService.ValidateToken(token);
        return user.Role == UserRole.Administrator;
    }
}