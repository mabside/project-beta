using Byhands.Application.Interfaces.Users;
using Byhands.Application.Utils;
using Byhands.CQRS.Interfaces;
using Byhands.Domain.Entities.Users;
using Byhands.Entities.Errors;
using Byhands.Models.Bases;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Byhands.Application.Usecases.Customers.SigninCustomer;

internal sealed class SigninCustomerCommandHandler : ICommandHandler<SigninCustomerCommand, SigninCustomerCommandResponse>
{
    private readonly IAuthService authService;
    private readonly UserManager<User> userManager;
    private readonly SignInManager<User> signInManager;
    private readonly JWTOptions jwtOptions;

    public SigninCustomerCommandHandler(
        IAuthService authService,
        UserManager<User> userManager,
        SignInManager<User> signInManager,
        IOptions<JWTOptions> jwtOptions)
    {
        this.authService = authService;
        this.userManager = userManager;
        this.signInManager = signInManager;
        this.jwtOptions = jwtOptions.Value;
    }

    public async Task<Result<SigninCustomerCommandResponse>> Handle(SigninCustomerCommand command, CancellationToken cancellationToken)
    {
        var isEmailValid = command.UserName.IsEmail();

        if (!isEmailValid) return new BadRequestError("Invalid email");

        var user = await userManager.FindByEmailAsync(command.UserName);

        if (user == null)
            return new Error("Invalid Credentials", "", false);

        var signInResult = await signInManager.PasswordSignInAsync(command.UserName, command.Password, false, false);

        if (!signInResult.Succeeded)
            return new Error("Invalid Credentials", "", false);

        var token = authService.GenerateJWTToken(user, jwtOptions.Secret);

        return new SigninCustomerCommandResponse(user.UserId, token.Token);
    }
}
