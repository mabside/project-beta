using Byhands.Domain.Entities.Feedbacks;
using Microsoft.AspNetCore.Http;

namespace Byhands.Application.Usecases.Feedbacks.CreateFeedbacks;

public record CreateFeedbackCommand(
    string Note,
    IFormFile ImageFile,
    IFormFile AudioFile,
    IFormFile VideoFile,
    int Star,
    FeedbackType FeedbackType,
    Guid SpaceId);