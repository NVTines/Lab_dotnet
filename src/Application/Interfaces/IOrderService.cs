using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IOrderService
    {
        Task<List<OrderModel>> GetAllAsync();
        Task<OrderModel?> GetByIdAsync(int id);
        Task<OrderModel?> GetByUserIdAsync(int id);
        Task PayAsync(int userId, int bookId, int quantity);
        Task<OrderModel> UpdateAsync(int orderId, int userId, int orderItemId, int quantity);
        Task DeleteAsync(int id);
    }
}