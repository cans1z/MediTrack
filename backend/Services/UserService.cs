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

    public void UpdateUser(UpdateUserDto updateUserUserDto, int userId)
    {
        using (var db = new ApplicationContext())
        {
            var upUser = db.Users.FirstOrDefault(u => u.Id == userId);
            if (upUser == null)
                throw new UserNotFoundException();
            if (!string.IsNullOrWhiteSpace(updateUserUserDto.Email))
                upUser.Email = updateUserUserDto.Email;
            if (!string.IsNullOrWhiteSpace(updateUserUserDto.Password))
                upUser.Password = updateUserUserDto.Password;
            if (updateUserUserDto.Role.HasValue)
                upUser.Role = updateUserUserDto.Role.Value;
            if (!string.IsNullOrWhiteSpace(updateUserUserDto.Name))
                upUser.UserName = updateUserUserDto.Name;
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
    public User GetUser(int userId)
    {
        using (var db = new ApplicationContext())
        {
            var user = db.Users.FirstOrDefault(x => x.Id == userId);
            if (user == null)
                throw new MedicationNotFoundException();
            return user;
        }
    }
}