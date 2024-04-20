using Byhands.Application.Usecases.Businesses.CeateBusinesses;
using Byhands.Domain.DTOs.Businesses;
using Byhands.Models.Bases;
using FastEndpoints;

namespace Byhands.API.Endpoints.Businesses.CreateBusinesses;

internal sealed class Mapper : Mapper<Request, Result<NewBusiness>, object>
{
    public static CreateBusinessCommand AsCommand(Request request)
    {
        return request;
    }
}
