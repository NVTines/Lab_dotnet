using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore.Storage;

public class UnitOfWork : IUnitOfWork
{
    private readonly BookStoreDbContext _context;
    private IDbContextTransaction _currentTransaction;
    public IBookRepository bookRepository { get; }
    public IGenreRepository genreRepository { get; }
    public IUserRepository userRepository { get; }
    public ICartRepository cartRepository { get; }
    public ICartItemRepository cartItemRepository { get; }
    //public IOrderRepository orderRepository { get; }
    //public IOrderItemRepository orderItemRepository { get; }
    public UnitOfWork(
        BookStoreDbContext context, IBookRepository bookRepository, 
        IGenreRepository genreRepository, IUserRepository userRepository, 
        ICartRepository cartRepository, ICartItemRepository cartItemRepository)
        //IOrderRepository orderRepository, IOrderItemRepository orderItemRepository
    {
        _context = context;
        this.bookRepository = bookRepository;
        this.genreRepository = genreRepository;
        this.userRepository = userRepository;
        this.cartRepository = cartRepository;
        this.cartItemRepository = cartItemRepository;
        //this.orderRepository = orderRepository;
        //this.orderItemRepository = orderItemRepository;
    }

    public async Task BeginTransactionAsync()
    {
        if (_currentTransaction != null)
        {
            throw new InvalidOperationException("A transaction is already in progress.");
        }

        _currentTransaction = await _context.Database.BeginTransactionAsync();
    }

    public async Task<int> CommitAsync()
    {
        if (_currentTransaction == null)
        {
            throw new InvalidOperationException("No transaction in progress to commit.");
        }

        try
        {
            var result = await _context.SaveChangesAsync();
            await _currentTransaction.CommitAsync();
            return result;
        }
        catch (Exception)
        {
            await RollbackAsync(); 
            throw;
        }
        finally
        {
            DisposeTransaction();
        }
    }

    public async Task RollbackAsync()
    {
        if (_currentTransaction != null)
        {
            await _currentTransaction.RollbackAsync();
            DisposeTransaction();
        }
    }

    private void DisposeTransaction()
    {
        _currentTransaction?.Dispose();
        _currentTransaction = null;
    }

    public void Dispose()
    {
        DisposeTransaction();
        _context.Dispose();
    }
}

//public class UnitOfWork<TContext> : IUnitOfWork<TContext> where TContext : DbContext
//{
//    private readonly TContext _context;
//    public UnitOfWork(TContext context)
//    {
//        _context = context;
//    }
//    public async Task<int> CommitAsync()
//    {
//        return await _context.SaveChangesAsync();
//    }
//    public void Dispose()
//    {
//        _context.Dispose();
//    }
//}
