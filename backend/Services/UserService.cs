using MediTrack.DTO;
using MediTrack.Types;

namespace MediTrack.Services;

public class UserNotFoundException : Exception
{
    public UserNotFoundException() : base("User not found") {}
}
public class UserService
{
    public void RegisterUser(RegisterDto registerUserDto)
    {
        using (var db = new ApplicationContext())
        {
            var newUser = registerUserDto.CreateUser();
            db.Users.Add(newUser);
            db.SaveChanges();
                
        } 
    }

    public void DeleteUser(DeleteDto deleteUserDto)
    {
        using (var db = new ApplicationContext())
        {
            var delUser = db.Users.FirstOrDefault(u => u.Email == deleteUserDto.Email);
            if (delUser == null) 
                throw new UserNotFoundException();
            db.Users.Remove(delUser);
            db.SaveChanges();
        } 
    }

    public void UpdateUser(UpdateDto updateUserDto)
    {
        using (var db = new ApplicationContext())
        {
            var upUser = db.Users.FirstOrDefault(u => u.Email == updateUserDto.Email);
            if (upUser == null)
                throw new UserNotFoundException();
            if (!string.IsNullOrWhiteSpace(updateUserDto.NewEmail))
                upUser.Email = updateUserDto.NewEmail;
            if (!string.IsNullOrWhiteSpace(updateUserDto.NewPassword))
                upUser.Password = updateUserDto.NewPassword;
            if (updateUserDto.NewRole.HasValue)
                upUser.Role = updateUserDto.NewRole.Value;
            if (!string.IsNullOrWhiteSpace(updateUserDto.NewName))
                upUser.UserName = updateUserDto.NewName;
            if (updateUserDto.IsBanned.HasValue)
                upUser.IsBanned = updateUserDto.IsBanned.Value;
            db.Users.Update(upUser);
            db.SaveChanges();
        }
    }

    public List<GetUserDto> GetUser()
    {
        using (var db = new ApplicationContext())
        {
            var users = db.Users.ToList();
            var result = users.Select(u => GetUserDto.FromUser(u)).ToList();
            return result;
        }
    }
}