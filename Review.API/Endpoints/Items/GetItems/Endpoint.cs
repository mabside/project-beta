using System.Xml.Schema;
using FastEndpoints;
using MediatR;
using Review.Domain.DTOs.Items;
using Review.Entities.QueryObjects;
using Review.Models.Bases;

namespace Review.API.Endpoints.Items.GetItems;

internal sealed class Endpoint : Endpoint<Request, Result<PaginatedResult<ItemInformation>>, Mapper>
{
    private readonly IMediator mediator;

    public Endpoint(IMediator mediator)
    {
        this.mediator = mediator;
    }

    public override void Configure()
    {
        Get("api/items/{businessId}/{spaceId}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var query = Mapper.AsQuery(req);
        var result = await this.mediator.Send(query, ct);

        await SendResultAsync(result, ct);
    }

    private async Task SendResultAsync(Result<PaginatedResult<ItemInformation>> result, CancellationToken c)
    {
        if (result.HasError)
        {
            AddError(result.Error.Message);
            await SendErrorsAsync(cancellation: c);
        }
        else
        {
            await SendAsync(result, cancellation: c);
        }
    }

    public int[] TwoSum(int[] nums, int target)
    {
        var result = new int[2];
        var arrayLength = nums.Length;
        var idx1 = 0;
        var idx2 = arrayLength - 1;

        bool isTargetFound = false;

        while (!isTargetFound && idx1 < idx2)
        {
            var total = nums[idx1] + nums[idx2];

            if (total > target)
            {
                idx2--;
            }
            else if (total < target)
            {
                idx1++;
            }
            else if (total == target)
            {
                result.Append(idx1);
                result.Append(idx2);

                isTargetFound = true;
            }
        }

        return result;
    }
}
