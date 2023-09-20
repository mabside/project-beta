using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Review.Domains;
using Review.Entities.Errors;
using Review.Models.Bases;

namespace Review.Entities.Validators;

public sealed class WebsiteAddress : ValueObject<string>
{
    private WebsiteAddress(string value) : base(value) { }

    public static Result<WebsiteAddress> Create(
        string value,
        [CallerArgumentExpression(nameof(value))] string parameterName = default!)
    {
        var result = RequiredField.Create(value, parameterName);
        if (result.HasError)
            return result.Error!;

        if (!Regex.IsMatch(value, RegexConstants.URL_PATTERN, RegexConstants.OPTIONS))
            return new DomainValidationError(ErrorValidators.ValueIsInvalid(parameterName).Message);

        return new WebsiteAddress(value);
    }
}