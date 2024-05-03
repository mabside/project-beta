using Byhands.CQRS;
using Byhands.Models.Bases;
using MediatR;

namespace Byhands.Application.Usecases.Customers.SigninCustomer;

public record SigninCustomerCommand(
    Guid CommandId,
    string UserName,
    string Password) : CommandBase<SigninCustomerCommandResponse>(CommandId);

public record SigninCustomerCommandResponse(
    string UserId,
    string Token);