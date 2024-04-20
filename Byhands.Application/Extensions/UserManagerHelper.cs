

using Byhands.Domain.Entities.Users;
using Byhands.Entities.Errors;
using Byhands.Extensions;
using Byhands.Models.Bases;
using Microsoft.AspNetCore.Identity;

namespace Byhands.Application.Extensions;

internal static class UserManagerHelper
{
    public static async Task<Result<User>> GetUserAsync(
        this UserManager<User> userManager,
        string username,
        CancellationToken cancellationToken = default)
    {
        var isEmail = username.IsEmail();

        var user = isEmail ?
            await userManager.FindByEmailAsync(username) :
            await userManager.FindByNameAsync(username);

        if (user == null)
            return new UserNotFoundError($"{username} does not exist");

        return user;
    }

    public static async Task<Result<User>> SignupAsync(
        this UserManager<User> userManager,
        User user,
        CancellationToken cancellationToken)
    {
        var result = await userManager.CreateAsync(user);

        if (!result.Succeeded)
            return new Error(string.Join(", ", result.Errors.Select(e => e.Description)), "", false);

        return user;
    }

    public static async Task<bool> HasRoleAsync(
        this UserManager<User> userManager,
        User user)
    {
        var roles = await userManager.GetRolesAsync(user);

        return !roles.IsNullOrEmpty();
    }
}