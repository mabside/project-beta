using FastEndpoints;
using Review.Application.Usecases.Businesses.CeateBusinesses;
using Review.Domain.DTOs.Businesses;
using Review.Models.Bases;

namespace Review.API.Endpoints.Businesses.CreateBusinesses;

internal sealed class Mapper : Mapper<Request, Result<NewBusiness>, object>
{
    public static CreateBusinessCommand AsCommand(Request request)
    {
        return request;
    }
}
