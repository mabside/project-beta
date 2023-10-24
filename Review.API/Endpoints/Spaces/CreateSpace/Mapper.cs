using FastEndpoints;
using Review.Application.Usecases.Spaces.CreateSpaces;
using Review.Domain.DTOs.Spaces;
using Review.Models.Bases;

namespace Review.API.Endpoints.Spaces.CreateSpace;

internal sealed class Mapper : Mapper<Request, Result<NewSpace>, object>
{
    public static CreateSpaceCommand AsCommand(Request request)
    {
        return request;
    }
}