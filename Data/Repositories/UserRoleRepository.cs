using Data.Contexts;
using Data.Entities;

namespace Data.Repositories;

public class UserRoleRepository(DataContext context) : BaseRepository<UserRoleEntity>(context)
{
}
