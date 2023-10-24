using Review.Application.Interfaces;
using Review.DataAccess;
using Review.Domain.Entities.Spaces;

namespace Review.Infrastructure.DataAccess.Repositories;

internal sealed class SpaceRepository
    : EFContextRepositoryBase<ReviewDbContext, Space, Guid>, ISpaceRepository
{
    public SpaceRepository(ReviewDbContext context) : base(context)
    {
    }
}
