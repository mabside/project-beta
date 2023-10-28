using Review.DataAccess;
using Review.Domain.Entities.Items;

namespace Review.Application.Interfaces;

public interface IItemRepository
    : IWriteOnlyRepository<Item, Guid>,
    IReadOnlyRepository<Item, Guid>
{
}

public interface IItemCategoryRepository
    : IWriteOnlyRepository<ItemCategory, Guid>,
    IReadOnlyRepository<ItemCategory, Guid>
{
}