using Byhands.DataAccess;
using Byhands.Domain.Entities.Customers;

namespace Byhands.Contract.Interfaces;

public interface ICustomerRepository
    : IWriteOnlyRepository<Customer, Guid>,
    IReadOnlyRepository<Customer, Guid>
{
}
