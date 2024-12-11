using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ICartItemService
    {
        Task<List<CartItemModel>> GetAllAsync();
        Task<CartItemModel?> GetByIdAsync(int id);
        Task<CartItemModel?> GetByCartIdAsync(int id);
        Task<CartItemModel> CreateAsync(CartItemModel item);
        Task<CartItemModel> UpdateAsync(int id, CartItemModel item);
        Task DeleteAsync(int id);
    }
}
