using Review.DataAccess;
using Review.Domain.Entities.Businesses;

namespace Review.Application.Interfaces;

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