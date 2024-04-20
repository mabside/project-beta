using Byhands.Models.Bases;
using MediatR;

namespace Byhands.Application.Usecases.Users.SignupCustomer;

public record SignupCustomerCommand(
    Guid UserId,
    Guid CommandId,
    string UserName,
    string Password
) : IRequest<Result>;

public record SignupCustomerCommandResponse(string UserId);