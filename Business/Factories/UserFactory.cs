using Business.Models.User;
using Data.Entities;

namespace Business.Factories;

public static class UserFactory
{
    public static UserEntity Create(UserRegistrationForm form)
    {
        var entity = new UserEntity
        {
            FirstName = form.FirstName,
            LastName = form.LastName,
            Email = form.Email,

        };

        return entity;
    }

    public static User Create(UserEntity entity)
    {
        var user = new User
        {
            Id = entity.Id,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            Email = entity.Email,
            Role = new UserRole
            {
                RoleId = entity.Role.Id,
                RoleName = entity.Role.RoleName
            }
        };

        return user;
    }
}
