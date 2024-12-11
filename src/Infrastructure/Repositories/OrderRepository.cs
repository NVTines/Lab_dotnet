using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class OrderRepository(BookStoreDbContext db) : Repository<Order>(db), IOrderRepository
    {
    }
}
