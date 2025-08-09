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
        // todo: так то норм, но лучше загугли что такое ExecuteDelete
        using (var db = new ApplicationContext())
        {
            var userToDelete = db.Users.FirstOrDefault(u => u.Id == userId);
            if (userToDelete == null) 
                throw new UserNotFoundException();
            db.Users.Remove(userToDelete);
            db.SaveChanges();
        } 
    }

    // TODO: аналогично для того что ты делал в UpdatePrescription.
    public void UpdateUser(UpdateUserDto updateUserUserDto, int userId)
    {
        using (var db = new ApplicationContext())
        {
            var upUser = db.Users.FirstOrDefault(u => u.Id == userId);
            if (upUser == null)
                throw new UserNotFoundException();
            upUser.Email = updateUserUserDto.Email;
            upUser.Password = updateUserUserDto.Password;
            upUser.Role = updateUserUserDto.Role;
            upUser.UserName = updateUserUserDto.Name;
            upUser.IsBanned = updateUserUserDto.IsBanned;
            db.Users.Update(upUser);
            db.SaveChanges();
        }
    }

    public List<GetUserDto> GetUsers()
    {
        using (var db = new ApplicationContext())
        {
            var users = db.Users.ToList();
            var result = users
                .Select(GetUserDto.FromUser) // todo: было "u => GetUserDto.FromUser(u)", но лучше так пиши, так короче и красивее
                .ToList();
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