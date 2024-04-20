using Byhands.Domain.DTOs.Feedbacks;

namespace Byhands.Domain.Entities.Feedbacks;

public partial class Feedback
{
    public static implicit operator FeedbackInformation(Feedback feedback)
    {
        return new FeedbackInformation
        {
            Note = feedback.Note,
            ImageUrl = feedback.ImageUrl,
            AudioUrl = feedback.AudioUrl,
            VideoUrl = feedback.VideoUrl,
            Star = feedback.Star,
            FeedbackType = feedback.FeedbackType,
            ProductId = feedback.ProductId,
        };
    }
}
