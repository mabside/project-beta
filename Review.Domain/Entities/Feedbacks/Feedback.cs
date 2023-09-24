using Review.Domain.Entities.Businesses;
using Review.Models.Bases;

namespace Review.Domain.Entities.Feedbacks;

public class Feedback : BaseEntity<Guid>
{
    public string Note { get; private set; }
    public string? ImageUrl { get; private set; }
    public string? AudioUrl { get; private set; }
    public string? VideoUrl { get; private set; }
    public int Star { get; private set; }
    public FeedbackType FeedbackType { get; set; }

    public Guid BusinessId { get; private set; }

    public virtual Business Business { get; private set; }

    private Feedback() { }

    private Feedback(
        string note,
        string? imageUrl,
        string? audioUrl,
        string? videoUrl,
        int star,
        FeedbackType reviewType,
        Guid businessId)
    {
        Note = note;
        ImageUrl = imageUrl;
        AudioUrl = audioUrl;
        VideoUrl = videoUrl;
        Star = star;
        FeedbackType = reviewType;
        BusinessId = businessId;
    }

    public static Result<Feedback> Create(
        string note,
        string? imageUrl,
        string? audioUrl,
        string? videoUrl,
        int star,
        FeedbackType reviewType,
        Guid businessId)
    {
        var result = Result<Feedback>.Create(
            new Feedback(
                note: note,
                imageUrl: imageUrl,
                audioUrl: audioUrl,
                videoUrl: videoUrl,
                star: star,
                reviewType: reviewType,
                businessId: businessId));

        if (result.HasError)
            return result.Error;

        return result.Value;
    }
}
