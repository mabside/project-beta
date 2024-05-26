using Byhands.Contract.Interfaces;
using Byhands.DataAccess;
using Byhands.Domain.Entities.Businesses;

namespace Byhands.Infrastructure.DataAccess.Repositories;

internal sealed class BusinessRepository
    : EFContextRepositoryBase<ByhandsDbContext, Business, Guid>, IBusinessRepository
{
    public BusinessRepository(ByhandsDbContext context) : base(context)
    {
    }
}

internal sealed class BusinessCategoryRepository
    : EFContextRepositoryBase<ByhandsDbContext, BusinessCategory, Guid>, IBusinessCategoryRepository
{
    public BusinessCategoryRepository(ByhandsDbContext context) : base(context)
    {
    }
}