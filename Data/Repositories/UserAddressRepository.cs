using Data.Contexts;
using Data.Entities;
using Data.Interfaces;

namespace Data.Repositories;

public class UserAddressRepository(DataContext context) : BaseRepository<UserAddressEntity>(context), IUserAddressRepository
{
}
