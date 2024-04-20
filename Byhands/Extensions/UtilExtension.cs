using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using Byhands.Entities.Validators;
using Npgsql;

namespace Byhands.Extensions;

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

    public static bool IsRequiredTenacyHeader(this string path)
    {
        var pattern = RegexConstants.REQUIRED_TEANAT_HEADER_URL_PATTERN;
        var isMatch = Regex.IsMatch(path, pattern, RegexConstants.OPTIONS);

        return isMatch;
    }

    public static string ErrorMessage(this PostgresException exception)
    {
        var detail = exception.Detail;

        if (String.IsNullOrEmpty(detail))
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
}

public enum TitleCase
{
    First,
    All
}