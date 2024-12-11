using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ICartService
    {
        Task<List<CartModel>> GetAllAsync();
        Task<CartModel?> GetByIdAsync(int id);
        Task<CartModel?> GetByUserIdAsync(int id);
        Task AddToCartAsync(int userId, int bookId, int quantity);
        Task<CartModel> UpdateAsync(int cartId, int userId, int cartItemId, int quantity);
        Task DeleteAsync(int id);
    }
}
