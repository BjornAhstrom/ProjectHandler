using Data.Contexts;
using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class CustomerRepository(DataContext context) : BaseRepository<CustomerEntity>(context)
{
    public override async Task<IEnumerable<CustomerEntity>> GetAllAsync()
    {
        var entites = await _context.Customers
            .Select(x => new CustomerEntity
            {
                Id = x.Id,
                CustomerName = x.CustomerName,
            }).ToListAsync();

        return entites;
    }
}
