using System.Linq.Expressions;
using Review.Abstractions.Entities;

namespace Review.DataAccess;

public interface IReadOnlyRepository<TEntity, TId>
        where TEntity : IEntity<TId>
        where TId : notnull
{
    bool Contains(TId id);
    Task<bool> ContainsAsync(TId id);
    bool Exists(Expression<Func<TEntity, bool>> predicate);
    Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate);
    int Count(Expression<Func<TEntity, bool>>? predicate = null);
    Task<int> CountAsync(Expression<Func<TEntity, bool>>? predicate = null);
    IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, params string[] includes);
    IEnumerable<TResult> Find<TResult>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TResult>> selector);
    Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, params string[] includes);
    Task<IEnumerable<TResult>> FindAsync<TResult>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TResult>> selector);
    TEntity? Get(TId id, bool asNoTracking = false, params string[] includes);
    TResult? Get<TResult>(TId id, Expression<Func<TEntity, TResult>> selector, bool asNoTracking = false);
    Task<TEntity?> GetAsync(TId id, bool asNoTracking = false, params string[] includes);
    Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>>? predicate, bool asNoTracking = false, params string[] includes);
    Task<TResult?> GetAsync<TResult>(TId id, Expression<Func<TEntity, TResult>> selector, bool asNoTracking = false, params string[] includes);
    IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>>? predicate = null, params string[] includes);
    IEnumerable<TResult> GetAll<TResult>(Expression<Func<TEntity, TResult>> selector, Expression<Func<TEntity, bool>>? predicate = null);
    Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? predicate = null, params string[] includes);
    Task<IEnumerable<TResult>> GetAllAsync<TResult>(Expression<Func<TEntity, TResult>> selector, Expression<Func<TEntity, bool>>? predicate = null);
}