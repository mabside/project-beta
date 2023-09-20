using Review.Entities.Errors;
using Review.Extensions;
using Review.Models.Bases;

namespace Review.Entities.Validators;

public static class ErrorValidators
{
    public static Error ValueIsInvalid(string propertyName)
        => new DomainValidationError($"{propertyName.SplitCamelCase().ToLower()} is invalid");

    public static Error ValueIsRequired(string propertyName)
        => new DomainValidationError($"{propertyName.SplitCamelCase().ToLower()} is required");

    public static Error ValueIsNotValidEnum(string propertyName, string enumType)
        => new DomainValidationError($"{propertyName} is not valid enum type of '{enumType}'");
}