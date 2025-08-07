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
    public static User GetUser(this HttpContext context, AuthService authService)
    {
        var token = context.GetToken();
        return authService.ValidateToken(token);
    }
    public static bool IsAdmin(this HttpContext context, AuthService authService, out User user)
    {
        var token = context.GetToken();
        user = authService.ValidateToken(token);
        return user.Role == UserRole.Administrator;
    }
    public static bool IsDoctor(this HttpContext context, AuthService authService, out User user)
    {
        var token = context.GetToken();
        user = authService.ValidateToken(token);
        return user.Role == UserRole.Doctor;
    }
    public static bool IsPatient(this HttpContext context, AuthService authService, out User user)
    {
        user = context.GetUser(authService);
        return user.Role == UserRole.Patient;
    }
}