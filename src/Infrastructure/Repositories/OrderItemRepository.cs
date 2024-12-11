using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class OrderItemRepository(BookStoreDbContext db) : Repository<OrderItem>(db), IOrderItemRepository
    {
    }
}
