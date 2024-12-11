using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IUnitOfWork
    {
        public IBookRepository bookRepository { get; }
        public IGenreRepository genreRepository { get; }
        public IUserRepository userRepository { get; }
        public ICartRepository cartRepository { get; }
        public ICartItemRepository cartItemRepository { get; }
        //public IOrderRepository orderRepository { get; }
        //public IOrderItemRepository orderItemRepository { get; }
        public Task BeginTransactionAsync();
        public Task<int> CommitAsync();       
        public Task RollbackAsync();        
    }

    //public interface IUnitOfWork : IDisposable
    //{
    //    Task<int> CommitAsync();
    //}

    //public interface IUnitOfWork<TContext> : IUnitOfWork where TContext : DbContext
    //{
    //}
}
