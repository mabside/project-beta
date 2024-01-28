namespace Byhands.Domain.DTOs.Spaces;

public class SpaceInformation
{
    public Guid SpaceId { get; set; }
    public Guid BusinessId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int ProductCounts { get; set; } = 0;
}
