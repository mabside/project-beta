using Review.Domain.DTOs.Feedbacks;

namespace Review.Domain.DTOs.Items;

public record ItemInformation(
    Guid Id,
    string Description,
    string ImageUrl,
    string LinkCode,
    string CategoryName,
    string SpaceName,
    IReadOnlyCollection<FeedbackInformation> Feedbacks);
