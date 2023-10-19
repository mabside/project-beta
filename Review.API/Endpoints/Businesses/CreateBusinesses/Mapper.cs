using FastEndpoints;
using Review.Application.Usecases.Businesses.CeateBusinesses;
using Review.Application.Usecases.Items.GetItems;
using Review.Domain.DTOs.Businesses;
using Review.Domain.DTOs.Items;
using Review.Entities.QueryObjects;
using Review.Models.Bases;

namespace Review.API.Endpoints.Businesses.CreateBusinesses;

internal sealed class Mapper : Mapper<Request, Result<NewBusiness>, object>
{
    public static CreateBusinessCommand AsCommand(Request request)
    {
        return request;
    }
}
