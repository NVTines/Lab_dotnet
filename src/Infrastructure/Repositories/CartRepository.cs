using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class CartRepository(BookStoreDbContext db) : Repository<Cart>(db), ICartRepository
    {
    }
}
