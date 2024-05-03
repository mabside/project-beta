using Byhands.Application.Interfaces.Users;
using Byhands.Application.Usecases.Customers.SigninCustomer;
using Byhands.Application.Usecases.Users.CreateUserIdentity;
using Byhands.Models.Bases;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Byhands.Infrastructure.Contracts.Users;

public class UserService : IUserService
{
    private readonly IServiceProvider serviceProvider;

    public UserService(IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;
    }

    public async Task<Result<CreateUserIdentityCommandResponse>> CreateIdentityAsync(
        Guid customerId,
        string userName,
        string password,
        Guid commandId,
        CancellationToken cancellationToken)
    {
        var command = new CreateUserIdentityCommand(
            customerId,
            commandId,
            userName,
            password);

        using var scope = serviceProvider.CreateScope();
        var sender = scope.ServiceProvider.GetService<ISender>();

        var result = await sender!.Send(command, cancellationToken);

        if (result.HasError)
            return result.Error;

        return result.Value;
    }

    public async Task<Result<SigninCustomerCommandResponse>> SignInAsync(
        string username,
        string password,
        Guid CommandId,
        CancellationToken cancellationToken)
    {
        var command = new SigninCustomerCommand(CommandId, username, password);

        using var scope = serviceProvider.CreateScope();
        var sender = scope.ServiceProvider.GetService<ISender>();

        var result = await sender!.Send(command, cancellationToken);

        if (result.HasError)
            return result.Error;

        return result.Value;
    }
}
