using Review.Application.Interfaces;

namespace Review.DataAccess;

public interface IUnitOfWork
{
    IItemRepository ItemRepository();
    IItemCategoryRepository ItemCategoryRepository();
    IBusinessRepository BusinessRepository();
    IBusinessCategoryRepository BusinessCategoryRepository();
    ICustomerRepository CustomerRepository();
    ISpaceRepository SpaceRepository();
    Task<int> CommitAsync(CancellationToken cancellationToken);
}