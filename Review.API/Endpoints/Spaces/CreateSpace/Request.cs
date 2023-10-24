using Review.Application.Usecases.Spaces.CreateSpaces;

namespace Review.API.Endpoints.Spaces.CreateSpace;

public class Request
{
    public Guid BusinessId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public static implicit operator CreateSpaceCommand(Request request)
    {
        return new CreateSpaceCommand(
            BusinessId: request.BusinessId,
            Name: request.Name,
            Description: request.Description);
    }
}
