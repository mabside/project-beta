using Microsoft.AspNetCore.Identity;
using Review.Abstractions.Entities;
using Review.Models.Bases;

namespace Review.Domain.Entities.Users;

public class User : IdentityUser, IDomainEntity
{
    public bool IsActive { get; private set; }
    public string UserId { get; private set; }
    public DateTimeOffset? LastLoginDate { get; private set; }

    private User() { }

    public static Result<User> Signup(string username, string? email, string? phoneNumber, string userId)
    {
        return Result<User>.Create(
            new User()
        );
    }

    private User(bool isActive, string? email, string? phoneNumber, string userId)
    {
        Email = email;
        UserId = userId;
        IsActive = isActive;
    }
}
