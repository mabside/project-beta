using TimeZoneConverter;

namespace Byhands.Utilities;

public static class Clock
{
    private static DateTime? _customDate;
    private static string _countryCode = "KE";

    public static void SetCountryCode(string countryCode)
    {
        _countryCode = countryCode.ToUpperInvariant();
    }

    public static DateTime Now
    {
        get
        {
            if (_customDate.HasValue)
            {
                return _customDate.Value;
            }

            else
            {
                TimeZoneInfo tzi = TZConvert.GetTimeZoneInfo(CountryZone(_countryCode));
                DateTimeOffset currentLocalTime = TimeZoneInfo.ConvertTime(DateTimeOffset.UtcNow, tzi);
                return currentLocalTime.DateTime;
            }
        }
    }

    public static DateTime? Local
    {
        get
        {
            if (_customDate.HasValue)
                return _customDate.Value;
            else
            {
                TimeZoneInfo tzi = TZConvert.GetTimeZoneInfo("Africa/Nigeria");
                DateTimeOffset currentLocalTime = TimeZoneInfo.ConvertTime(DateTimeOffset.UtcNow, tzi);
                return currentLocalTime.DateTime;
            }
        }
    }

    public static void Set(DateTime customDate) => _customDate = customDate;

    public static void Reset() => _customDate = null;

    public static long TimeStamp
    {
        get
        {
            return new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
        }
    }

    private static string CountryZone(string countryCode)
    {
        return countryCode switch
        {
            CountryCodes.NG => "Africa/Nigeria",
            CountryCodes.KE => "Africa/Nairobi",
            CountryCodes.UG => "Africa/Kampala",
            CountryCodes.SS => "Africa/Juba",
            CountryCodes.CD => "Africa/Nairobi",
            CountryCodes.RW => "Africa/Kinshasa",
            CountryCodes.TZ => "Africa/Dar_es_Salaam",
            _ => "Africa/Nairobi",
        };
    }
}

public static class CountryCodes
{
    public const string NG = nameof(NG);
    public const string KE = nameof(KE);
    public const string UG = nameof(UG);
    public const string SS = nameof(SS);
    public const string CD = nameof(CD);
    public const string RW = nameof(RW);
    public const string TZ = nameof(TZ);
}