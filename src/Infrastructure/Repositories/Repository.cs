using Domain.Entities;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System;
using Domain.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{

    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly BookStoreDbContext _db;
        internal DbSet<T> dbSet;

        public Repository(BookStoreDbContext db)
        {
            _db = db;
            dbSet = db.Set<T>();
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (includeProperties.Length > 0)
            {
                query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            }
            return query.AsEnumerable();
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges = false) =>
            !trackChanges ? dbSet.Where(expression).AsNoTracking() : dbSet.Where(expression);

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges = false, params Expression<Func<T, object>>[] includeProperties) =>
            includeProperties.Aggregate(FindByCondition(expression, trackChanges), (current, includeProperty) => current.Include(includeProperty));

        public async Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter, bool tracked = false, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = dbSet;
            if (!tracked)
            {
                query = dbSet.AsNoTracking();
            }
            query = query.Where(filter);
            if (includeProperties.Length > 0)
            {
                query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            }
            return await query.FirstOrDefaultAsync();
        }

        public async Task AddAsync(T entity) => await _db.AddAsync(entity);

        public async Task AddRangeAsync(List<T> entities) => await dbSet.AddRangeAsync(entities);

        public async Task UpdateAsync(T entity)
        {
            dbSet.Update(entity);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateRangeAsync(IEnumerable<T> entities)
        {
            dbSet.UpdateRange(entities);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            dbSet.Remove(entity);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteRangeAsync(IEnumerable<T> entities)
        {
            dbSet.RemoveRange(entities);
            await Task.CompletedTask;
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> filter) => await dbSet.AnyAsync(filter);

        public T? GetFirstOrDefault(Expression<Func<T, bool>> filter, bool tracked = false, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = dbSet;

            if (!tracked)
            {
                query = query.AsNoTracking();
            }

            query = query.Where(filter);

            if (includeProperties.Length > 0)
            {
                query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            }

            return query.FirstOrDefault();
        }

        public void Add(T entity) => dbSet.Add(entity);

        public void AddRange(List<T> entities) => dbSet.AddRange(entities);

        public void Update(T entity) => dbSet.Update(entity);

        public void UpdateRange(IEnumerable<T> entities) => dbSet.UpdateRange(entities);

        public void Delete(T entity) => dbSet.Remove(entity);

        public void DeleteRange(IEnumerable<T> entities) => dbSet.RemoveRange(entities);

        public bool Any(Expression<Func<T, bool>> filter) => dbSet.Any(filter);
    }

    //public class RepositoryQueryBase<T, K, TContext> : IRepositoryQueryBase<T, K, TContext>
    //    where T : EntityBase<K>
    //    where TContext : DbContext
    //{
    //    private readonly TContext _dbContext;

    //    public RepositoryQueryBase(TContext dbContext)
    //    {
    //        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    //    }

    //    public IQueryable<T> FindAll(bool trackChanges = false) =>
    //        !trackChanges ? _dbContext.Set<T>().AsNoTracking() :
    //            _dbContext.Set<T>();

    //    public IQueryable<T> FindAll(bool trackChanges = false, params Expression<Func<T, object>>[] includeProperties)
    //    {
    //        var items = FindAll(trackChanges);
    //        items = includeProperties.Aggregate(items, (current, includeProperty) => current.Include(includeProperty));
    //        return items;
    //    }

    //    public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges = false) =>
    //        !trackChanges
    //            ? _dbContext.Set<T>().Where(expression).AsNoTracking()
    //            : _dbContext.Set<T>().Where(expression);

    //    public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges = false, params Expression<Func<T, object>>[] includeProperties)
    //    {
    //        var items = FindByCondition(expression, trackChanges);
    //        items = includeProperties.Aggregate(items, (current, includeProperty) => current.Include(includeProperty));
    //        return items;
    //    }

    //    public async Task<T?> GetByIdAsync(K id) =>
    //        await FindByCondition(x => x.Id.Equals(id))
    //            .FirstOrDefaultAsync();

    //    public async Task<T?> GetByIdAsync(K id, params Expression<Func<T, object>>[] includeProperties) =>
    //        await FindByCondition(x => x.Id.Equals(id), trackChanges: false, includeProperties)
    //            .FirstOrDefaultAsync();
    //}

    //public class RepositoryBase<T, K, TContext> : RepositoryQueryBase<T, K, TContext>,
    // IRepositoryBase<T, K, TContext>
    // where T : EntityBase<K>
    // where TContext : DbContext
    //{
    //    private readonly TContext _dbContext;
    //    private readonly IUnitOfWork<TContext> _unitOfWork;

    //    public RepositoryBase(TContext dbContext, IUnitOfWork<TContext> unitOfWork) : base(dbContext)
    //    {
    //        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    //        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    //    }

    //    public Task<IDbContextTransaction> BeginTransactionAsync() => _dbContext.Database.BeginTransactionAsync();

    //    public async Task EndTransactionAsync()
    //    {
    //        await SaveChangesAsync();
    //        await _dbContext.Database.CommitTransactionAsync();
    //    }

    //    public Task RollbackTransactionAsync() => _dbContext.Database.RollbackTransactionAsync();

    //    public void Create(T entity) => _dbContext.Set<T>().Add(entity);

    //    public async Task<K> CreateAsync(T entity)
    //    {
    //        await _dbContext.Set<T>().AddAsync(entity);
    //        await SaveChangesAsync();
    //        return entity.Id;
    //    }

    //    public IList<K> CreateList(IEnumerable<T> entities)
    //    {
    //        _dbContext.Set<T>().AddRange(entities);
    //        return entities.Select(x => x.Id).ToList();
    //    }

    //    public async Task<IList<K>> CreateListAsync(IEnumerable<T> entities)
    //    {
    //        await _dbContext.Set<T>().AddRangeAsync(entities);
    //        await SaveChangesAsync();
    //        return entities.Select(x => x.Id).ToList();
    //    }

    //    public void Update(T entity)
    //    {
    //        if (_dbContext.Entry(entity).State == EntityState.Unchanged) return;

    //        T exist = _dbContext.Set<T>().Find(entity.Id);
    //        _dbContext.Entry(exist).CurrentValues.SetValues(entity);
    //    }

    //    public async Task<int> UpdateAsync(T entity)
    //    {
    //        if (_dbContext.Entry(entity).State == EntityState.Unchanged) return 0;

    //        T exist = _dbContext.Set<T>().Find(entity.Id);
    //        _dbContext.Entry(exist).CurrentValues.SetValues(entity);
    //        return await SaveChangesAsync();
    //    }

    //    public void UpdateList(IEnumerable<T> entities) => _dbContext.Set<T>().AddRange(entities);

    //    public async Task UpdateListAsync(IEnumerable<T> entities)
    //    {
    //        await _dbContext.Set<T>().AddRangeAsync(entities);
    //        await SaveChangesAsync();
    //    }

    //    public void Delete(T entity) => _dbContext.Set<T>().Remove(entity);

    //    public async Task DeleteAsync(T entity)
    //    {
    //        _dbContext.Set<T>().Remove(entity);
    //        await SaveChangesAsync();
    //    }

    //    public void DeleteList(IEnumerable<T> entities) => _dbContext.Set<T>().RemoveRange(entities);

    //    public async Task DeleteListAsync(IEnumerable<T> entities)
    //    {
    //        _dbContext.Set<T>().RemoveRange(entities);
    //        await SaveChangesAsync();
    //    }

    //    public async Task<int> SaveChangesAsync() => await _unitOfWork.CommitAsync();
    //}
}