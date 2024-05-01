using Byhands.Application.Interfaces;
using Byhands.Application.Interfaces.Users;
using Byhands.DataAccess;
using Byhands.Domain.DTOs.Customers;
using Byhands.Domain.Entities.Customers;
using Byhands.Models.Bases;
using MediatR;

namespace Byhands.Application.Usecases.Customers.SignupCustomer;

public class SignupCustomerCommandHandler : IRequestHandler<SignupCustomerCommand, Result<NewCustomer>>
{
    private readonly ICustomerRepository customerRepository;
    private readonly IUserService userService;
    private readonly IUnitOfWork unitOfWork;

    public SignupCustomerCommandHandler(
        ICustomerRepository customerRepository,
        IUserService userService,
        IUnitOfWork unitOfWork)
    {
        this.customerRepository = customerRepository;
        this.userService = userService;
        this.unitOfWork = unitOfWork;
    }

    public async Task<Result<NewCustomer>> Handle(SignupCustomerCommand command, CancellationToken cancellationToken)
    {
        var result = Customer.Create(
            firstname: command.firstname,
            lastname: command.lastname,
            email: command.email,
            phoneNumber: command.phoneNumber);

        if (result.HasError)
            return result.Error;

        var newCustomer = result.Value;

        var existingCustomer = await customerRepository.ExistsAsync(
            c => c.Email == command.email);

        if (!existingCustomer)
        {
            await customerRepository.AddAsync(newCustomer);
        }
        else
        {
            newCustomer = await customerRepository.FirstOrDefaultAsync(
                c => c.Email == command.email);
        }

        var createIdentityResult = await userService.CreateIdentityAsync(
            customerId: newCustomer!.Id,
            userName: newCustomer.Email,
            password: command.password,
            commandId: command.CommandId,
            cancellationToken: cancellationToken);

        if (createIdentityResult.HasError)
            return createIdentityResult.Error;

        await unitOfWork.CommitAsync(cancellationToken);
        return new NewCustomer(newCustomer.Id);
    }
}
