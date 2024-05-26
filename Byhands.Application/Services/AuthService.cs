using Byhands.Application.Usecases.Customers.SigninCustomer;
using Byhands.Application.Usecases.Customers.SignupCustomer;
using Byhands.Application.Utils;
using Byhands.Contract.Interfaces;
using Byhands.Contract.Interfaces.Auth;
using Byhands.Contract.Interfaces.Users;
using Byhands.Domain.DTOs.Auth;
using Byhands.Domain.Entities.Customers;
using Byhands.Domain.Entities.Users;
using Byhands.Entities.Errors;
using Byhands.Models.Bases;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Byhands.Application.Services;

public class AuthService : IAuthService
{
    private readonly ICustomerRepository customerRepository;
    private readonly IUserService userService;
    private readonly ITokenService tokenService;
    private readonly UserManager<User> userManager;
    private readonly SignInManager<User> signInManager;

    public AuthService(
        ICustomerRepository customerRepository,
        IUserService userService,
        ITokenService tokenService,
        UserManager<User> userManager,
        SignInManager<User> signInManager)
    {
        this.customerRepository = customerRepository;
        this.userService = userService;
        this.tokenService = tokenService;
        this.userManager = userManager;
        this.signInManager = signInManager;
    }

    public async Task<Result<Guid>> CreateNewCustomerAsync(
        SignupCustomerCommand command,
        CancellationToken cancellationToken)
    {
        var result = Customer.Create(
            firstname: command.Firstname,
            lastname: command.Lastname,
        email: command.Email,
            phoneNumber: command.PhoneNumber);

        if (result.HasError)
            return result.Error;
        var newCustomer = result.Value;

        var existingCustomer = await customerRepository.ExistsAsync(
        c => c.Email == command.Email);

        if (!existingCustomer)
        {
            await customerRepository.AddAsync(newCustomer);
        }
        else
        {
            newCustomer = await customerRepository.FirstOrDefaultAsync(
                c => c.Email == command.Email);
        }

        var createIdentityResult = await userService.CreateIdentityAsync(
            customerId: newCustomer!.Id,
            userName: newCustomer.Email,
            password: command.Password,
            commandId: command.CommandId,
            cancellationToken: cancellationToken);

        if (createIdentityResult.HasError)
            return createIdentityResult.Error;

        return newCustomer.Id;
    }

    public async Task<Result<SigninCustomerResponse>> SignInCustomerAsync(
    SigninCustomerCommand request,
        CancellationToken cancellationToken)
    {
        var isEmailValid = request.UserName.IsEmail();

        if (!isEmailValid) return new BadRequestError("Invalid email");

        var user = await userManager.FindByEmailAsync(request.UserName);
        if (user == null)
            return new Error("Invalid Credentials", "", false);
        var signInResult = await signInManager.PasswordSignInAsync(request.UserName, request.Password, false, false);

        if (!signInResult.Succeeded)
            return new Error("Invalid Credentials", "", false);

        var token = GenerateJWTToken(user);

        //@Todo: Save last login - User.SaveLastLogin() -> throw event maybe...

        return new SigninCustomerResponse(user.Id, token.Token);
    }

    private AuthToken GenerateJWTToken(User user)
    {
        return tokenService.GenerateJWTToken(user);
    }
}
