using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Byhands.Domains;
using Byhands.Entities.Errors;
using Byhands.Models.Bases;

namespace Byhands.Entities.Validators;

public sealed class PhoneNumber : ValueObject<string>
{
    private PhoneNumber(string value) : base(value) { }

    public static Result<PhoneNumber> Create(
        string phoneNumber,
        [CallerArgumentExpression(nameof(phoneNumber))] string parameterName = default!)
    {
        var result = RequiredField.Create(phoneNumber, parameterName);
        if (result.HasError)
            return result.Error;

        if (!Regex.IsMatch(phoneNumber, RegexConstants.PHONE_PATTERN, RegexConstants.OPTIONS))
            return new DomainValidationError(ErrorValidators.ValueIsInvalid(parameterName).Message);

        return new PhoneNumber(phoneNumber);
    }
}