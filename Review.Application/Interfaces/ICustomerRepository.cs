using Review.DataAccess;
using Review.Domain.Entities.Customers;

namespace Review.Application.Interfaces;

public interface ICustomerRepository
    : IWriteOnlyRepository<Customer, Guid>,
    IReadOnlyRepository<Customer, Guid>
{
}
