using Review.Application.Interfaces;
using Review.DataAccess;
using Review.Domain.Entities.Businesses;

namespace Review.Infrastructure.DataAccess.Repositories;

internal sealed class BusinessRepository
    : EFContextRepositoryBase<ReviewDbContext, Business, Guid>, IBusinessRepository
{
    public BusinessRepository(ReviewDbContext context) : base(context)
    {
    }
}

internal sealed class BusinessCategoryRepository
    : EFContextRepositoryBase<ReviewDbContext, BusinessCategory, Guid>, IBusinessCategoryRepository
{
    public BusinessCategoryRepository(ReviewDbContext context) : base(context)
    {
    }
}