using Review.Application.Interfaces;
using Review.DataAccess;
using Review.Domain.Entities.Items;

namespace Review.Infrastructure.DataAccess.Repositories;

internal sealed class ItemRepository
    : EFContextRepositoryBase<ReviewDbContext, Item, Guid>, IItemRepository
{
    public ItemRepository(ReviewDbContext context) : base(context)
    {
    }
}

internal sealed class ItemCategoryRepository
    : EFContextRepositoryBase<ReviewDbContext, ItemCategory, Guid>, IItemCategoryRepository
{
    public ItemCategoryRepository(ReviewDbContext context) : base(context)
    {
    }
}
