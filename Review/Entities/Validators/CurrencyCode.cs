using System.Runtime.CompilerServices;
using Byhands.Domains;
using Byhands.Entities.Errors;
using Byhands.Models.Bases;

namespace Byhands.Entities.Validators;

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
