using Data.Contexts;
using Data.Entities;
using Data.Interfaces;

namespace Data.Repositories;

public class OrderStatusRepository(DataContext context) : BaseRepository<OrderStatusEntity>(context), IOrderStatusRepository
{
}
