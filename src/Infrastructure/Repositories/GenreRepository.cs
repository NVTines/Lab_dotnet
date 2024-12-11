using Domain.Interfaces;
using Domain.Entities;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class GenreRepository(BookStoreDbContext db) : Repository<Genre>(db), IGenreRepository{}
}
