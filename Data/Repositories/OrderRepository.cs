using Data.Contexts;
using Data.Entities;
using Data.Interfaces;

namespace Data.Repositories;

public class OrderRepository(DataContext context) : BaseRepository<OrderEntity>(context), IOrderRepository
{
}
