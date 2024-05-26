using Byhands.CQRS;
using Byhands.Domain.DTOs.Customers;

namespace Byhands.Application.Usecases.Customers.SignupCustomer;

public record SignupCustomerCommand(
    Guid CommandId,
    string Firstname,
    string Lastname,
    string Email,
    string PhoneNumber,
    string Password
) : CommandBase<NewCustomerResponse>(CommandId);
