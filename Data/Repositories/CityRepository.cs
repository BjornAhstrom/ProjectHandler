using Data.Contexts;
using Data.Entities;
using Data.Interfaces;

namespace Data.Repositories;

public class CityRepository(DataContext context) : BaseRepository<CityEntity>(context), ICityRepository
{
}
