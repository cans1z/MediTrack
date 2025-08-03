using MediTrack.DTO;
using MediTrack.Types;

namespace MediTrack.Services;

public class UserNotFoundException : Exception
{
    public UserNotFoundException() : base("User not found") {}
}
public class UserService
{
    public void RegisterUser(RegisterUserDto registerUserDto)
    {
        using (var db = new ApplicationContext())
        {
            var newUser = registerUserDto.CreateUser();
            db.Users.Add(newUser);
            db.SaveChanges();
                
        } 
    }

    public void DeleteUser(int userId)
    {
        using (var db = new ApplicationContext())
        {
            var userToDelete = db.Users.FirstOrDefault(u => u.Id == userId);
            if (userToDelete == null) 
                throw new UserNotFoundException();
            db.Users.Remove(userToDelete);
            db.SaveChanges();
        } 
    }

    public void UpdateUser(UpdateUserDto updateUserUserDto)
    {
        using (var db = new ApplicationContext())
        {
            var upUser = db.Users.FirstOrDefault(u => u.Email == updateUserUserDto.Email);
            if (upUser == null)
                throw new UserNotFoundException();
            if (!string.IsNullOrWhiteSpace(updateUserUserDto.NewEmail))
                upUser.Email = updateUserUserDto.NewEmail;
            if (!string.IsNullOrWhiteSpace(updateUserUserDto.NewPassword))
                upUser.Password = updateUserUserDto.NewPassword;
            if (updateUserUserDto.NewRole.HasValue)
                upUser.Role = updateUserUserDto.NewRole.Value;
            if (!string.IsNullOrWhiteSpace(updateUserUserDto.NewName))
                upUser.UserName = updateUserUserDto.NewName;
            if (updateUserUserDto.IsBanned.HasValue)
                upUser.IsBanned = updateUserUserDto.IsBanned.Value;
            db.Users.Update(upUser);
            db.SaveChanges();
        }
    }

    public List<GetUserDto> GetUsers()
    {
        using (var db = new ApplicationContext())
        {
            var users = db.Users.ToList();
            var result = users.Select(u => GetUserDto.FromUser(u)).ToList();
            return result;
        }
    }
}