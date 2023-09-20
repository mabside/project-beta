using System.Runtime.CompilerServices;
using Review.Domains;
using Review.Entities.Errors;
using Review.Models.Bases;

namespace Review.Entities.Validators;

public sealed class CurrencyCode : ValueObject<string>
{
    public CurrencyCode(string value) : base(value) { }

    public static Result<CurrencyCode> Create(
        string value,
        [CallerArgumentExpression(nameof(value))] string parameterName = default!)
    {
        var result = RequiredField.Create(value, parameterName);
        if (result.HasError)
            return result.Error;

        // TODO: if Currency is a valid currency code
        if (string.IsNullOrEmpty(value))
            return new DomainValidationError(ErrorValidators.ValueIsInvalid(parameterName).Message);

        return new CurrencyCode(value);
    }
}
