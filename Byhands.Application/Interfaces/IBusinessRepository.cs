using Byhands.DataAccess;
using Byhands.Domain.Entities.Businesses;

namespace Byhands.Application.Interfaces;

public interface IBusinessRepository
    : IWriteOnlyRepository<Business, Guid>,
    IReadOnlyRepository<Business, Guid>
{
}

public interface IBusinessCategoryRepository
    : IWriteOnlyRepository<BusinessCategory, Guid>,
    IReadOnlyRepository<BusinessCategory, Guid>
{
}