using Byhands.Contract.Interfaces;
using Byhands.DataAccess;
using Byhands.Domain.Entities.Customers;

namespace Byhands.Infrastructure.DataAccess.Repositories;

internal sealed class CustomerRepository
    : EFContextRepositoryBase<ByhandsDbContext, Customer, Guid>, ICustomerRepository
{
    public CustomerRepository(ByhandsDbContext context) : base(context)
    {
    }
}
