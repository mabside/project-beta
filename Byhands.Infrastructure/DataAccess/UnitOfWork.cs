using Byhands.Application.Interfaces;
using Byhands.DataAccess;
using Byhands.Infrastructure.DataAccess.Repositories;

namespace Byhands.Infrastructure.DataAccess;

public class UnitOfWork : IUnitOfWork
{
    private readonly ByhandsDbContext _databaseContext;
    private bool _disposed;

    public UnitOfWork(ByhandsDbContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public IProductRepository ProductRepository()
    {
        return new ProductRepository(_databaseContext);
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

    public IProductCategoryRepository productCategoryRepository()
    {
        return new productCategoryRepository(_databaseContext);
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