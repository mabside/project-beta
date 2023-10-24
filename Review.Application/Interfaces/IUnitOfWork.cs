using Review.Application.Interfaces;

namespace Review.DataAccess;

public interface IUnitOfWork
{
    IItemRepository ItemRepository();
    IBusinessRepository BusinessRepository();
    IBusinessCategoryRepository BusinessCategoryRepository();
    ICustomerRepository CustomerRepository();
    ISpaceRepository SpaceRepository();
    Task<int> CommitAsync(CancellationToken cancellationToken);
}