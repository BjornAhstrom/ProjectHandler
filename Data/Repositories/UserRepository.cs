using Data.Contexts;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Data.Repositories;

public class UserRepository(DataContext context) : BaseRepository<UserEntity>(context), IUserRepository
{
    public override async Task<IEnumerable<UserEntity>> GetAllAsync()
    {
        var entities = await _context.Users
            .Include(x => x.Role)
            .Include(x => x.UserRoles)
            .ThenInclude(x => x.Role)
            .Select(x => new UserEntity
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                RoleId = x.RoleId,
                Role = x.Role,
                UserRoles = x.UserRoles.Select(ur => new UserRoleEntity
                {
                    Role = new RoleEntity
                    {
                        RoleName = ur.Role.RoleName
                    }
                }).ToList()

            }).ToListAsync();

        return entities;
    }

    public override async Task<UserEntity?> GetAsync(Expression<Func<UserEntity, bool>> expression)
    {
        var entity = await _context.Users
            .Include(x => x.Role)
            .Include(x => x.UserRoles)
            .ThenInclude(x => x.Role)
            .Select(x => new UserEntity
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                RoleId = x.RoleId,
                Role = x.Role,
                UserRoles = x.UserRoles.Select(ur => new UserRoleEntity
                {
                    Role = new RoleEntity
                    {
                        RoleName = ur.Role.RoleName
                    }
                }).ToList()

            })
            .FirstOrDefaultAsync(expression);

        return entity;
    }

    public async Task<IEnumerable<UserEntity>> GetProjectManagersAsync()
    {
        var entities = await _context.Users
            .Include(x => x.UserRoles)
            .ThenInclude(x => x.Role)
            .Where(x => x.UserRoles.Any(ur => ur.Role.RoleName == "Projektledare"))
            .ToListAsync();

        return entities;

    }

    public override async Task<bool?> RemoveAsync(UserEntity entity)
    {
        try
        {
            var userRoles = _context.UserRoles
            .Where(x => x.UserId == entity.Id)
            .ToList();

            _context.UserRoles.RemoveRange(userRoles);
            _context.Users.Remove(entity);
            await _context.SaveChangesAsync();

            return true;
        } catch (Exception ex) 
        {
            Debug.WriteLine($"Error :: {ex.Message}");
            return false;
        }
    }

}
