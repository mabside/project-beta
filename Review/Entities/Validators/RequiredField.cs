using System.Runtime.CompilerServices;
using Review.Domains;
using Review.Models.Bases;

namespace Review.Entities.Validators;

public sealed class RequiredField : ValueObject<string>
{
    public RequiredField(string value) : base(value) { }

    public static Result<RequiredField> Create(
        string value,
        [CallerArgumentExpression(nameof(value))] string parameterName = default!)
    {
        if (string.IsNullOrWhiteSpace(value))
            return new Errors.DomainValidationError(ErrorValidators.ValueIsRequired(parameterName).Message);

        return new RequiredField(value);
    }
}
