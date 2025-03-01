using Data.Contexts;
using Data.Entities;
using Data.Interfaces;

namespace Data.Repositories;

public class PostalCodeRepository(DataContext context) : BaseRepository<PostalCodeEntity>(context), IPostalCodeRepository
{
}
