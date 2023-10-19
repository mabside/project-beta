using Microsoft.EntityFrameworkCore.Storage;
using Review.Application.Interfaces;
using Review.Models.Bases;

namespace Review.DataAccess;

public interface IUnitOfWork
{
    IItemRepository ItemRepository();
    IBusinessRepository BusinessRepository();
    IBusinessCategoryRepository BusinessCategoryRepository();
    Task<int> CommitAsync(CancellationToken cancellationToken);
}