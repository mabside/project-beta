using Byhands.Contract.Interfaces;
using Byhands.DataAccess;
using Byhands.Domain.Entities.Customers;
using Byhands.Infrastructure.DataAccess;

namespace Byhands.Infrastructure.Repository;

public class CustomerRepository : EFContextRepositoryBase<ByhandsDbContext, Customer, Guid>, ICustomerRepository
{
    public CustomerRepository(ByhandsDbContext context) : base(context)
    {
    }
}
