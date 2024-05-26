using Byhands.CQRS;

namespace Byhands.Application.Usecases.Customers.SigninCustomer;

public record SigninCustomerCommand(
    Guid CommandId,
    string UserName,
    string Password) : CommandBase<SigninCustomerResponse>(CommandId);

public record SigninCustomerResponse(
    string UserId,
    string Token);