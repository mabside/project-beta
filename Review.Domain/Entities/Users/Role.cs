using Microsoft.AspNetCore.Identity;
using Byhands.Abstractions.Entities;
using Byhands.Entities.Validators;
using Byhands.Extensions;
using Byhands.Models.Bases;

namespace Byhands.Domain.Entities.Users;

public class Role : IdentityRole, IDomainEntity
{
    public int UserCount { get; private set; } = 0;
    public string Description { get; private set; } = null!;

    private Role() { }

    private Role(string name, string description)
    {
        Name = FormatName(name);
        Description = description;
    }

    public static Result<Role> Create(string name, string description)
    {
        var result = Result<Role>.Create(
            new Role(name, description))
            .Validate(RequiredField.Create(name))
            .Validate(RequiredField.Create(description));

        if (result.HasError)
            return result.Error;

        return result;
    }

    private string FormatName(string name)
    {
        if (string.IsNullOrEmpty(name))
            return string.Empty;

        return name.Trim().Replace(" ", "-");
    }
}
