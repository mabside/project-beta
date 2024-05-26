using Byhands.Contract;
using Byhands.Contract.Interfaces.Auth;
using Byhands.CQRS.Interfaces;
using Byhands.Models.Bases;
using DotNetCore.CAP;

namespace Byhands.Application.Usecases.Customers.SigninCustomer;

internal sealed class SigninCustomerCommandHandler : ICommandHandler<SigninCustomerCommand, SigninCustomerResponse>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly ICapPublisher capPublisher;
    private readonly IAuthService authService;

    public SigninCustomerCommandHandler(
        IUnitOfWork unitOfWork,
        ICapPublisher capPublisher,
        IAuthService authService)
    {
        this.unitOfWork = unitOfWork;
        this.capPublisher = capPublisher;
        this.authService = authService;
    }

    public async Task<Result<SigninCustomerResponse>> Handle(SigninCustomerCommand command, CancellationToken cancellationToken)
    {
        using var transaction = unitOfWork.Begin(capPublisher);

        var signInResult = await authService.SignInCustomerAsync(command, cancellationToken);

        if (signInResult.HasError)
            return signInResult.Error;

        var signInResponse = signInResult.Value;

        var commitResult = await unitOfWork.CommitAsync(transaction, cancellationToken);
        if (commitResult.HasError)
            return commitResult.Error;

        return signInResponse;
    }
}
