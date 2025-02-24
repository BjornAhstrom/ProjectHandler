using Data.Contexts;
using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class UserRepository(DataContext context) : BaseRepository<UserEntity>(context)
{
    public override async Task<IEnumerable<UserEntity>> GetAllAsync()
    {
        var entities = await _context.Users
            .Include(x => x.Role)
            .Include(x =>x.UserRoles)
            .ThenInclude(x => x.Role)
            .Select(x => new UserEntity
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                Role = new RoleEntity
                {
                    RoleName = x.Role.RoleName
                },
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


}
