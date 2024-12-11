using Domain.Models;

namespace Application.Interfaces
{
    public interface IUserService
    {
        Task<List<UserModel>> GetAllAsync();
        Task<UserModel?> GetByIdAsync(int id);
        Task<UserModel?> GetByUsernameAsync(string username);
        Task<UserModel> CreateAsync(UserModel item);
        Task<UserModel> UpdateAsync(int id, UserModel item);
        Task DeleteAsync(int id);
    }

}
