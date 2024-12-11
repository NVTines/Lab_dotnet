using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class CartItemRepository(BookStoreDbContext db) : Repository<CartItem>(db), ICartItemRepository
    {
    }
}
