using Domain.Interfaces;
using Domain.Entities;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class UserRepository(BookStoreDbContext db) : Repository<User>(db), IUserRepository{}
}
