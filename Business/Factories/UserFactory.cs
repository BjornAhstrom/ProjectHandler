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

    public static UserEntity Update(UserUpdateForm form)
    {
        var entity = new UserEntity
        {
            Id = form.Id,
            FirstName = form.FirstName,
            LastName = form.LastName,
            Email = form.Email,
            RoleId = form.RoleId
        };

        return entity;
    }

    public static UserEntity Create(User user)
    {
        var entity = new UserEntity
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
        };

        return entity;
    }
}