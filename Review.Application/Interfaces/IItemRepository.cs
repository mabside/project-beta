using Review.DataAccess;
using Review.Domain.Entities.Items;

namespace Review.Application.Interfaces;

public interface IItemRepository
    : IWriteOnlyRepository<Item, Guid>,
    IReadOnlyRepository<Item, Guid>
{
}
