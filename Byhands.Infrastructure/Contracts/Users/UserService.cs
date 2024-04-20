using Byhands.Application.Interfaces.Users;
using Byhands.Application.Usecases.Users.SignupCustomer;
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

    public async Task<Result> CreateIdentityAsync(
        Guid customerId,
        string userName,
        string password,
        Guid commandId,
        CancellationToken cancellationToken)
    {
        var command = new SignupCustomerCommand(
            customerId,
            commandId,
            userName,
            password);

        using var scope = serviceProvider.CreateScope();
        var sender = scope.ServiceProvider.GetService<ISender>();

        var result = await sender!.Send(command, cancellationToken);

        if (result.HasError)
            return result.Error;

        return result;
    }
}
