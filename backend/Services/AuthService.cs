using MediTrack.Types;
using Microsoft.EntityFrameworkCore;

namespace MediTrack.Services;

public class UnauthorizedException : Exception
{
    public UnauthorizedException() : base("authorization error. token is invalid") 
    {}
}
public class AuthService
{
    public static User ValidateToken(string token)
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
}