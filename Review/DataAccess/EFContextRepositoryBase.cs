using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Byhands.Abstractions.Entities;

namespace Byhands.DataAccess;

public abstract class EFContextRepositoryBase<TContext, TEntity, TId>
    : IWriteOnlyRepository<TEntity, TId>,
    IReadOnlyRepository<TEntity, TId>
    where TEntity : class, IEntity<TId> where TId : notnull
    where TContext : DbContext
{
    protected readonly TContext _dbContext;

    protected EFContextRepositoryBase(TContext context)
    {
        _dbContext = context;
    }

    public virtual void Add(TEntity entity)
    {
        _dbContext.Add(entity);
    }

    public virtual async Task AddAsync(TEntity entity)
    {
        await _dbContext.AddAsync(entity);
    }

    public bool Contains(TId id)
    {
        return Count(x => x.Id.Equals(id)) > 0;
    }

    public async Task<bool> ContainsAsync(TId id)
    {
        return await CountAsync(x => x.Id.Equals(id)) > 0;
    }

    public int Count(Expression<Func<TEntity, bool>>? predicate = null)
    {
        return BuildQuery(predicate).Count();
    }

    public async Task<int> CountAsync(Expression<Func<TEntity, bool>>? predicate = null)
    {
        return await BuildQuery(predicate).CountAsync();
    }

    public void Delete(TEntity entity)
    {
        _dbContext.Remove(entity);
    }

    public async Task DeleteAsync(TEntity entity)
    {
        await Task.Run(() => Delete(entity));
    }

    public bool Exists(Expression<Func<TEntity, bool>> predicate)
    {
        return Count(predicate) > 0;
    }

    public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await CountAsync(predicate) > 0;
    }

    public virtual IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, params string[] includes)
    {
        return BuildQuery(predicate, includes).ToList();
    }

    public virtual IEnumerable<TResult> Find<TResult>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TResult>> selector)
    {
        return BuildQuery(predicate).Select(selector).ToList();
    }

    public virtual async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, bool asNoTracking = false, params string[] includes)
    {
        return await BuildQuery(predicate, includes, asNoTracking: asNoTracking).ToListAsync();
    }

    public virtual async Task<IEnumerable<TResult>> FindAsync<TResult>(
        Expression<Func<TEntity, bool>> predicate,
        Expression<Func<TEntity, TResult>> selector)
    {
        return await BuildQuery(predicate).Select(selector).ToListAsync();
    }

    public virtual TEntity? Get(TId id, bool asNoTracking = false, params string[] includes)
    {
        return BuildQuery(includes: includes, asNoTracking: asNoTracking).FirstOrDefault(x => x.Id.Equals(id));
    }

    public virtual TResult? Get<TResult>(TId id, Expression<Func<TEntity, TResult>> selector, bool asNoTracking = false)
    {
        return BuildQuery(asNoTracking: asNoTracking)
                .Where(x => x.Id.Equals(id))
                .Select(selector)
                .FirstOrDefault();
    }

    public virtual IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>>? predicate = null, params string[] includes)
    {
        return BuildQuery(predicate, includes)
                    .AsNoTracking()
                    .ToList();
    }

    public virtual IEnumerable<TResult> GetAll<TResult>(
        Expression<Func<TEntity, TResult>> selector,
        Expression<Func<TEntity, bool>>? predicate = null)
    {
        return BuildQuery(predicate)
                    .AsNoTracking()
                    .Select(selector)
                    .ToList();
    }

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? predicate = null, params string[] includes)
    {
        return await BuildQuery(predicate, includes)
                        .AsNoTracking()
                        .ToListAsync();
    }

    public virtual async Task<IEnumerable<TResult>> GetAllAsync<TResult>(
        Expression<Func<TEntity, TResult>> selector,
        Expression<Func<TEntity, bool>>? predicate = null)
    {
        return await BuildQuery(predicate)
                        .AsNoTracking()
                        .Select(selector)
                        .ToListAsync();
    }

    public virtual async Task<TEntity?> GetAsync(TId id, bool asNoTracking = false, params string[] includes)
    {
        return await BuildQuery(includes: includes, asNoTracking: asNoTracking)
                        .FirstOrDefaultAsync(x => x.Id.Equals(id));
    }

    public virtual async Task<TResult?> GetAsync<TResult>(
        TId id,
        Expression<Func<TEntity, TResult>> selector,
        bool asNoTracking = false, params string[] includes)
    {
        return await BuildQuery(x => x.Id.Equals(id), includes, asNoTracking)
                        .Select(selector)
                        .FirstOrDefaultAsync();
    }

    public virtual async Task<TEntity?> FirstOrDefaultAsync(
        Expression<Func<TEntity, bool>>? predicate,
        bool asNoTracking = false, params string[] includes)
    {
        return await BuildQuery(predicate, includes, asNoTracking)
                        .FirstOrDefaultAsync();
    }

    public void Update(TEntity entity)
    {
        _dbContext.Update(entity);
    }

    [Obsolete("Use Update instead")]
    public async Task UpdateAsync(TEntity entity)
    {
        await Task.Run(() => Update(entity));
    }

    private IQueryable<TEntity> BuildQuery(Expression<Func<TEntity, bool>>? predicate = null, string[]? includes = null, bool asNoTracking = false)
    {
        IQueryable<TEntity> dbContext = _dbContext.Set<TEntity>();

        if (asNoTracking)
            dbContext = dbContext.AsNoTracking();

        if (predicate is not null)
            dbContext = dbContext.Where(predicate);

        if (includes is not null)
        {
            foreach (var include in includes)
            {
                dbContext = dbContext.Include(include);
            }
        }

        return dbContext;
    }
}