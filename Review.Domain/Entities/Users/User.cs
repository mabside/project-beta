using Microsoft.AspNetCore.Identity;
using Review.Abstractions.Entities;
using Review.Entities.Validators;
using Review.Extensions;
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
        var result = Result<User>.Create(
            new User(
                email: email,
                isActive: true,
                userId: userId,
                username: username,
                phoneNumber: phoneNumber))
            .Validate(RequiredField.Create(username))
            .Validate(OptionalField.Create(email, RegexConstants.EMAIL_PATTERN))
            .Validate(OptionalField.Create(phoneNumber, RegexConstants.PHONE_PATTERN));

        if (result.HasError)
            return result.Error;

        return result;
    }

    internal void ChangePhoneNumber(string phoneNumber)
    {
        PhoneNumber = phoneNumber;

        if (!UserName!.IsEmail())
            UserName = phoneNumber;
    }

    internal void SetPhoneNumberConfirmed(bool status)
    {
        PhoneNumberConfirmed = status;
    }

    internal void UpdateLastLoginDate()
    {
        AccessFailedCount = 0;
        LastLoginDate = DateTimeOffset.UtcNow;
    }

    private User(string username, bool isActive, string? email, string? phoneNumber, string userId)
    {
        Email = email;
        UserId = userId;
        IsActive = isActive;
        UserName = username;
        PhoneNumber = phoneNumber;
    }
}
