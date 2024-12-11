using Domain.Models;

namespace Application.Interfaces
{
    public interface IBookService
    {
        Task<List<BookModel>> GetAllAsync();
        Task<BookModel?> GetByIdAsync(int id);
        Task<BookModel?> GetByNameAsync(string name);
        Task<BookModel> CreateAsync(BookModel item);
        Task<BookModel> UpdateAsync(int id, BookModel item);
        Task DeleteAsync(int id);
    }
}
