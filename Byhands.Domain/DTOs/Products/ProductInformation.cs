using Byhands.Domain.DTOs.Feedbacks;

namespace Byhands.Domain.DTOs.Products;

public record ProductInformation(
    Guid Id,
    string Description,
    string ImageUrl,
    string CategoryName,
    string BusinessName,
    IReadOnlyCollection<FeedbackInformation> Feedbacks);
