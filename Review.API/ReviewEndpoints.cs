namespace Byhands.API
{
    public class ByhandsEndpoints
    {
        public const string BASE = "api/v1";
        public const string ProductPREFIX = $"{BASE}/Products";
        public const string GETProductS = $"{ProductPREFIX}/{{businessId}}/{{spaceId}}";
    }
}
