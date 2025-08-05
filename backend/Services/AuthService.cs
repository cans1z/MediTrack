using MediTrack.DTO;
using MediTrack.Types;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace MediTrack.Services;

public class UnauthorizedException : Exception
{
    public UnauthorizedException() : base("authorization error. token is invalid") {}
}

public class AuthService
{
    public User ValidateToken(string token)
    {
        using (var db = new ApplicationContext())
        {
            var session = db.Sessions
                .Where(i => i.Token == token)
                .Include(i => i.User)
                .FirstOrDefault();
            if (session == null || !session.IsActive) throw new UnauthorizedException();
            return session.User;
        }
    }
    public bool TryValidateAdmin(string token, out User user, out string? errorMessage)
    {
        try
        {
            user = ValidateToken(token);

            if (user.Role != UserRole.Administrator)
            {
                errorMessage = "You don't have permission to perform this action.";
                return false;
            }

            errorMessage = null;
            return true;
        }
        catch (UnauthorizedException ex)
        {
            user = null!;
            errorMessage = ex.Message;
            return false;
        }
    }
    
    
    
}