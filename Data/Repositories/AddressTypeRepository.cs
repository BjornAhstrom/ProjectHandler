using Data.Contexts;
using Data.Entities;
using Data.Interfaces;

namespace Data.Repositories;

public class AddressTypeRepository(DataContext context) : BaseRepository<AddressTypeEntity>(context), IAddressTypeRepository
{
}
