namespace Byhands;

public class CapSettings
{
    public const int FAILED_RETRY_COUNT = 5;
    public const int FAILED_RETRY_INTERVAL_SECONDS = 60 * 2; //2 minutes
    public const int SUCCEED_MESSAGE_EXPIRES_AFTER = 60 * 60 * 24; //1 day
    public const int FAILED_MESSAGE_EXPIRES_AFTER = 60 * 60 * 24 * 2; //2 day
}