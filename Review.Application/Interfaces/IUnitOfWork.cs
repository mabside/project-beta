using Byhands.Application.Interfaces;

namespace Byhands.DataAccess;

public interface IUnitOfWork
{
    IProductRepository ProductRepository();
    IProductCategoryRepository productCategoryRepository();
    IBusinessRepository BusinessRepository();
    IBusinessCategoryRepository BusinessCategoryRepository();
    ICustomerRepository CustomerRepository();
    Task<int> CommitAsync(CancellationToken cancellationToken);
}