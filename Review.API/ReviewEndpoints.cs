namespace Review.API
{
    public class ReviewEndpoints
    {
        public const string BASE = "api/v1";
        public const string ITEMPREFIX = $"{BASE}/items";
        public const string GETITEMS = $"{ITEMPREFIX}/{{businessId}}/{{spaceId}}";
    }
}
