using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Review.Abstractions.Entities;
using Review.Entities.Validators;
using Review.Extensions;
using Review.Models.Bases;

namespace Review.Domain.Entities.Users;

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
