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
    
    public string Login(string username, string password)
    {
        using var db = new ApplicationContext();

        var user = db.Users.FirstOrDefault(i => i.UserName == username && i.Password == password);
        if (user == null || user.IsBanned) throw new UnauthorizedException();

        var oldSessions = db.Sessions.Where(s => s.UserId == user.Id && s.IsActive);
        foreach (var s in oldSessions)
            s.IsActive = false;

        var newSession = new Session
        {
            UserId = user.Id,
            Token = Guid.NewGuid().ToString(),
            IsActive = true,
            AuthDate = DateTime.UtcNow
        };

        db.Sessions.Add(newSession);
        db.SaveChanges();

        return newSession.Token;
    }

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
}