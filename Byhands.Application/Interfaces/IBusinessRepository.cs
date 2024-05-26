using Byhands.DataAccess;
using Byhands.Domain.Entities.Businesses;

namespace Byhands.Contract.Interfaces;

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