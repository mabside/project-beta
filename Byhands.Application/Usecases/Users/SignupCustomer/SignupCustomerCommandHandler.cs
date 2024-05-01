using Byhands.Application.Extensions;
using Byhands.Domain.Entities.Users;
using Byhands.Entities.Errors;
using Byhands.Models.Bases;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Byhands.Application.Usecases.Users.SignupCustomer;

internal sealed class SignupCustomerCommandHandler : IRequestHandler<SignupCustomerCommand, Result>
{
    private readonly UserManager<User> userManager;

    public SignupCustomerCommandHandler(UserManager<User> userManager)
    {
        this.userManager = userManager;
    }

    public async Task<Result> Handle(SignupCustomerCommand command, CancellationToken cancellationToken)
    {
        var isEmailValid = command.UserName.IsEmail();

        if (!isEmailValid) return new BadRequestError("Invalid email");

        var userResult = User.Signup(
            username: command.UserName,
            email: isEmailValid ? command.UserName : null,
            phoneNumber: !isEmailValid ? command.UserName : null,
            userId: command.UserId.ToString());

        if (userResult.HasError)
            return userResult.Error;

        // @TODO: might want to cache this
        var getUserResult = await UserManagerHelper.GetUserAsync(
            userManager,
            command.UserName,
            cancellationToken);

        if (getUserResult.Value is not null)
            return new Error($"A customer with {command.UserName} already exists", "", false);

        var user = userResult.Value;

        var signupResult = await userManager.CreateAsync(user, password: command.Password);

        if (!signupResult.Succeeded)
            return new Error(string.Join(", ", signupResult.Errors.Select(e => e.Description)), "", false);

        return new Success();
    }
}
