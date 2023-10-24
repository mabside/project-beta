using FastEndpoints;
using Review.Application.Usecases.Spaces.GetBusinessSpaces;
using Review.Domain.DTOs.Spaces;
using Review.Models.Bases;

namespace Review.API.Endpoints.Spaces.GetBusinessSpaces;

internal sealed class Mapper : Mapper<Request, Result<IReadOnlyCollection<SpaceInformation>>, object>
{
    public static GetBusinessSpaceQuery AsQuery(Request request)
    {
        return request;
    }
}
