using Byhands.DataAccess;
using Byhands.Domain.Entities.Customers;

namespace Byhands.Application.Interfaces;

public interface ICustomerRepository
    : IWriteOnlyRepository<Customer, Guid>,
    IReadOnlyRepository<Customer, Guid>
{
}
