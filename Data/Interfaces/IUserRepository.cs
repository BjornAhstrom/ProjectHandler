using Data.Entities;
using System.Linq.Expressions;

namespace Data.Interfaces;

public interface IUserRepository : IBaseRepository<UserEntity>
{
    Task<IEnumerable<UserEntity>> GetProjectManagersAsync();
    Task<UserEntity?> UpdateRoleAsync(Expression<Func<UserEntity, bool>> expression, int roleId);
}
