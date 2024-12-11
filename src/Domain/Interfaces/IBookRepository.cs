using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IBookRepository : IRepository<Book>
    {
        Task<List<Book>> GetBooksAsync();
        Task<Book> GetBookByIdAsync(int id);
        Task<Book> GetBookByNameAsync(string name);
        Task CreateAsync(Book book);
        Task UpdateAsync(Book book);
        Task DeleteBookAsync(Book book);
    }

    //public interface IBookRepository : IRepository<Book>
    //{
        //Task<IEnumerable<Book>> GetBooksAsync();
        //Task<Book?> GetBookByIdAsync(int id);
        //Task<Book?> GetBookByNameAsync(string name);
        //Task<int> CreateBookAsync(Book Book);
        //Task<int> UpdateBookAsync(Book Book);
        //Task DeleteBookAsync(Book Book);
    //}
}