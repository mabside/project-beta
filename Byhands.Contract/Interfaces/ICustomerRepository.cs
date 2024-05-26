namespace Byhands.Contract.Interfaces;

public interface ICustomerRepository
    : IWriteOnlyRepository<Customer, Guid>,
    IReadOnlyRepository<Customer, Guid>
{
}
