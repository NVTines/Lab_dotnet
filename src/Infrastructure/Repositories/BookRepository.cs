using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class BookRepository(BookStoreDbContext db) : Repository<Book>(db), IBookRepository
    {

        public async Task<List<Book>> GetBooksAsync() =>
            await dbSet.ToListAsync();

        public async Task<Book> GetBookByIdAsync(int id) =>
            await dbSet.FindAsync(id);

        public async Task<Book> GetBookByNameAsync(string name) =>
            await dbSet.FirstOrDefaultAsync(b => b.Title == name);

        public async Task CreateAsync(Book book) =>
            await dbSet.AddAsync(book);

        public async Task UpdateAsync(Book book) =>
            dbSet.Update(book);

        public async Task DeleteBookAsync(Book book) =>
            dbSet.Remove(book);
    }

    //public class BookRepository : RepositoryBase<Book, int, BookStoreDbContext>, IBookRepository
    //{
    //    public BookRepository(BookStoreDbContext dbContext, IUnitOfWork<BookStoreDbContext> unitOfWork) : base(dbContext, unitOfWork)
    //    {
    //    }

    //    public Task<int> CreateBookAsync(Book Book) => CreateAsync(Book);

    //    public Task DeleteBookAsync(Book Book) =>  DeleteAsync(Book);

    //    public Task<Book?> GetBookByIdAsync(int id)=>
    //        FindByCondition(h => h.Id.Equals(id)).SingleOrDefaultAsync();

    //    public Task<Book?> GetBookByNameAsync(string name)=>
    //        FindByCondition(h => h.Title.Equals(name)).SingleOrDefaultAsync();

    //    public async Task<IEnumerable<Book>> GetBooksAsync()=>
    //        await FindAll().OrderByDescending(r => r.Id).ToListAsync();

    //    public Task<int> UpdateBookAsync(Book Book) => UpdateAsync(Book);
    //}
}
