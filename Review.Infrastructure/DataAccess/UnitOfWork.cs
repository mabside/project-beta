using Review.Application.Interfaces;
using Review.DataAccess;
using Review.Infrastructure.DataAccess.Repositories;

namespace Review.Infrastructure.DataAccess;

public class UnitOfWork : IUnitOfWork
{
    private readonly ReviewDbContext _databaseContext;
    private bool _disposed;

    public UnitOfWork(ReviewDbContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public IItemRepository ItemRepository()
    {
        return new ItemRepository(_databaseContext);
    }

    public IBusinessRepository BusinessRepository()
    {
        return new BusinessRepository(_databaseContext);
    }

    public ICustomerRepository CustomerRepository()
    {
        return new CustomerRepository(_databaseContext);
    }

    public IBusinessCategoryRepository BusinessCategoryRepository()
    {
        return new BusinessCategoryRepository(_databaseContext);
    }

    public ISpaceRepository SpaceRepository()
    {
        return new SpaceRepository(_databaseContext);
    }

    public IItemCategoryRepository ItemCategoryRepository()
    {
        return new ItemCategoryRepository(_databaseContext);
    }

    public Task<int> CommitAsync(CancellationToken cancellationToken)
    {
        return _databaseContext.SaveChangesAsync(cancellationToken);
    }

    ~UnitOfWork()
    {
        Dispose(false);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
            if (disposing)
                _databaseContext.Dispose();
        _disposed = true;
    }
}