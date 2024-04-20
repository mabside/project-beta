using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using Byhands.Entities.Validators;
using Npgsql;

namespace Byhands;

public static partial class UtilExtension
{
    [GeneratedRegex("([A-Z])", RegexOptions.Compiled)]
    private static partial Regex Word();

    private static readonly CultureInfo ci = new("en-GB");

    public static string ToTitleCase(this string input, TitleCase titleCase = TitleCase.All)
    {
        if (string.IsNullOrEmpty(input))
            return input;

        if (titleCase == TitleCase.All)
            return ci.TextInfo.ToTitleCase(input);

        var strArray = input.Split(' ');
        strArray[0] = ci.TextInfo.ToTitleCase(strArray[0]);

        return string.Join(" ", strArray);
    }

    public static string SplitCamelCase(this string input, bool useLowerCase = true)
    {
        return useLowerCase ? Word().Replace(input, " $1").ToLower(CultureInfo.CurrentCulture).Trim() : Word().Replace(input, " $1").Trim();
    }

    public static string PrependCountryCode(this string phoneNumber, string countryCode)
    {
        if (phoneNumber.StartsWith('0'))
            phoneNumber = phoneNumber.Substring(1);

        if (countryCode.StartsWith('+'))
            countryCode = countryCode.Substring(1);

        return $"{countryCode}{phoneNumber}";
    }

    public static string RemovePlusPrefix(this string countryCode)
    {
        if (countryCode.StartsWith('+'))
            countryCode = countryCode[1..];

        return countryCode;
    }

    public static bool IsPhoneNumber(this string input)
    {
        var pattern = RegexConstants.PHONE_PATTERN;
        var isMatch = !Regex.IsMatch(input, pattern, RegexConstants.OPTIONS);

        return isMatch;
    }

    public static bool IsEmail(this string input)
    {
        var pattern = RegexConstants.EMAIL_PATTERN;
        var isMatch = Regex.IsMatch(input, pattern, RegexConstants.OPTIONS);

        return isMatch;
    }

    public static bool TenacyHeaderIsRequired(this string path)
    {
        var pattern = RegexConstants.REQUIRED_TEANAT_HEADER_URL_PATTERN;
        var isMatch = Regex.IsMatch(path, pattern, RegexConstants.OPTIONS);

        return isMatch && !path.Contains("images");
    }

    public static string ErrorMessage(this PostgresException exception)
    {
        var detail = exception.Detail;

        if (string.IsNullOrEmpty(detail))
            return string.Empty;

        var reason = detail!.Split(')').LastOrDefault();

        var pattern = @"(\([\w\s@_.-]+?\))";
        var input = detail!.Replace("\"", "");
        var match = Regex.Match(input, pattern, RegexOptions.IgnoreCase);
        int matchCount = 0;
        var s = new string[2];

        while (match.Success)
        {
            s[matchCount] = match.Value;
            ++matchCount;
            match = match.NextMatch();
        }

        StringBuilder sb = new();
        sb.AppendJoin(" ", s);
        sb.Append(reason);

        return sb.ToString().Replace("(", "").Replace(")", "");
    }

    public static string ToDisplayText(this string input, char oldValue = '-', char newValue = '-')
    {
        return input.Replace(oldValue, newValue);
    }

    public static string FullName(string? FirstName, string? LastName)
    {
        return string.Join(' ', new string?[] { FirstName, LastName }).Trim();
    }

    //public static ExceptionError ToFriendlyErrorMessage(this ExceptionError error)
    //{
    //    var errorMessage = error.Message;
    //    var details = errorMessage
    //        .Split(Environment.NewLine)
    //        .LastOrDefault();

    //    if (string.IsNullOrEmpty(details))
    //        return error;

    //    string message = string.Empty;
    //    //Sample => DETAIL: Key("Code") = (AZBBXNJ)already exists.
    //    if (details.Contains("DETAIL:"))
    //    {
    //        //Sample => Key("Code") = (AZBBXNJ)already exists.
    //        var content = details.Split(':').LastOrDefault();
    //        if (string.IsNullOrEmpty(content))
    //            return error;

    //        //Sample => Code: 'AZBBXNJ' already exists.;
    //        //NOTE: the replace order is important
    //         message = content
    //            .Replace("=", " ")
    //            .Replace("Key", string.Empty)
    //            .Replace("\")", string.Empty)
    //            .Replace("(\"", string.Empty)
    //            .Replace("\"", string.Empty)
    //            .Replace("(", "'")
    //            .Replace(")", "'")
    //            .Trim();

    //        if(string.IsNullOrEmpty(message))
    //            return error;
    //    }
    //    return new ExceptionError(message, error.Exception);
    //}

    public static (string Key, string Value) AsErrorTupple(this string errorMessage)
    {
        var contents = errorMessage.Split(',');
        if (contents.Any())
        {
            string key = contents.First();
            var str = contents.Skip(1);
            var value = str.FirstOrDefault();
            if (!string.IsNullOrEmpty(value))
            {
                return (key.Trim(), value.Trim());
            }
        }
        return (string.Empty, string.Empty);
    }

    //public static ExceptionError AsUploadExceptionError(
    //    this ExceptionError error, 
    //    string prefixMessage = "Bulk upload failed because,")
    //{
    //    string message = $"{prefixMessage} {error.Message}";
    //    return new ExceptionError(message, error.Exception);
    //}
}

public enum TitleCase
{
    First,
    All
}