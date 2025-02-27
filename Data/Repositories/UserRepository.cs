using Data.Contexts;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
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
            .Where(x => x.UserRoles.Any(ur => ur.Role.RoleName == "Projektledare"))
            .Include(x => x.UserRoles)
            .ThenInclude(x => x.Role)
            .ToListAsync();

        return entities;

    }

    public async Task<UserEntity?> UpdateRoleAsync(Expression<Func<UserEntity, bool>> expression, int roleId)
    {
        var entity = await _context.Users
            .Include(x => x.Role)
            .FirstOrDefaultAsync(expression);

        if (entity == null)
        {
            return null;
        }

        if (entity.Role == null)
        {
            entity.Role = new RoleEntity { Id = 0, RoleName = "Default" };
        }

        entity.RoleId = roleId;

        return entity;
    }

}
