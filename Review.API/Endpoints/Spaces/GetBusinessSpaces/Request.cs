using Review.Application.Usecases.Spaces.GetBusinessSpaces;

namespace Review.API.Endpoints.Spaces.GetBusinessSpaces;

public class Request
{
    public Guid BusinessId { get; set; }

    public static implicit operator GetBusinessSpaceQuery(Request request)
    {
        return new GetBusinessSpaceQuery(BusinessId: request.BusinessId);
    }
}
