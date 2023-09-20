using System.Runtime.CompilerServices;
using Review.Domains;
using Review.Models.Bases;

namespace Review.Entities.Validators;

public sealed class AlphaNumericValidator : ValueObject<string>
{
    public AlphaNumericValidator(string value) : base(value) { }

    public static Result<AlphaNumericValidator> Create(string value,
        [CallerArgumentExpression(nameof(value))] string parameterName = default!)
    {
        var result = new string(value
            .Where(c => !char.IsLetterOrDigit(c)).ToArray());
        if (!string.IsNullOrEmpty(result))
            return new Errors.DomainValidationError(ErrorValidators.ValueIsInvalid(parameterName).Message);

        return new AlphaNumericValidator(value);
    }
}