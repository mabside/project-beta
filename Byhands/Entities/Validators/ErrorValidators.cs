using Byhands.Entities.Errors;
using Byhands.Models.Bases;

namespace Byhands.Entities.Validators;

public static class ErrorValidators
{
    public static Error ValueIsInvalid(string propertyName)
        => new DomainValidationError($"{propertyName.SplitCamelCase().ToLower()} is invalid");

    public static Error ValueIsRequired(string propertyName)
        => new DomainValidationError($"{propertyName.SplitCamelCase().ToLower()} is required");

    public static Error ValueIsNotValidEnum(string propertyName, string enumType)
        => new DomainValidationError($"{propertyName} is not valid enum type of '{enumType}'");
}