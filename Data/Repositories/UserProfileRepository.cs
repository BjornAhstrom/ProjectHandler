using Data.Contexts;
using Data.Entities;
using Data.Interfaces;

namespace Data.Repositories;

public class UserProfileRepository(DataContext context) : BaseRepository<UserProfileEntity>(context), IUserProfileRepository
{
}
