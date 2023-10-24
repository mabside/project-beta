using Review.Application.Interfaces;
using Review.DataAccess;
using Review.Domain.Entities.Customers;

namespace Review.Infrastructure.DataAccess.Repositories;

internal sealed class CustomerRepository
    : EFContextRepositoryBase<ReviewDbContext, Customer, Guid>, ICustomerRepository
{
    public CustomerRepository(ReviewDbContext context) : base(context)
    {
    }
}
