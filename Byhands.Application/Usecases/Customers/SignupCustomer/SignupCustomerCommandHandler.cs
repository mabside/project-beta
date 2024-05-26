using Byhands.Contract;
using Byhands.Contract.Interfaces.Auth;
using Byhands.CQRS.Interfaces;
using Byhands.Domain.DTOs.Customers;
using Byhands.Models.Bases;
using DotNetCore.CAP;

namespace Byhands.Application.Usecases.Customers.SignupCustomer;

public class SignupCustomerCommandHandler : ICommandHandler<SignupCustomerCommand, NewCustomerResponse>
{
    private readonly ICapPublisher capPublisher;
    private readonly IAuthService authService;
    private readonly IUnitOfWork unitOfWork;

    public SignupCustomerCommandHandler(
        ICapPublisher capPublisher,
        IAuthService authService,
        IUnitOfWork unitOfWork)
    {
        this.capPublisher = capPublisher;
        this.authService = authService;
        this.unitOfWork = unitOfWork;
    }

    public async Task<Result<NewCustomerResponse>> Handle(SignupCustomerCommand command, CancellationToken cancellationToken)
    {
        using var transaction = unitOfWork.Begin(capPublisher);

        var newCustomerResult = await authService.CreateNewCustomerAsync(command, cancellationToken);

        if (newCustomerResult.HasError)
            return newCustomerResult.Error;

        var newCustomerId = newCustomerResult.Value;

        var commitResult = await unitOfWork.CommitAsync(transaction, cancellationToken);
        if (commitResult.HasError)
            return commitResult.Error;

        return new NewCustomerResponse(newCustomerId);
    }
}
