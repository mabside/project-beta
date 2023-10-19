using Review.Domain.Entities.Businesses;
using Review.Domain.Entities.Items;
using Review.Models.Bases;

namespace Review.Domain.Entities.Feedbacks;

public partial class Feedback : BaseEntity<Guid>
{
    public string Note { get; private set; }
    public string? ImageUrl { get; private set; }
    public string? AudioUrl { get; private set; }
    public string? VideoUrl { get; private set; }
    public int Star { get; private set; }
    public string FeedbackType { get; private set; }

    public Guid BusinessId { get; private set; }
    public Guid ItemId { get; private set; }

    public virtual Business Business { get; private set; }
    public virtual Item Item { get; private set; }

    private Feedback() { }

    private Feedback(
        string note,
        int star,
        FeedbackType reviewType,
        Guid businessId,
        string? imageUrl = null,
        string? audioUrl = null,
        string? videoUrl = null)
    {
        Note = note;
        Star = star;
        FeedbackType = reviewType.ToString();
        BusinessId = businessId;
        ImageUrl = imageUrl;
        AudioUrl = audioUrl;
        VideoUrl = videoUrl;
    }

    public static Result<Feedback> Create(
        string note,
        int star,
        FeedbackType reviewType,
        Guid businessId,
        string? imageUrl = null,
        string? audioUrl = null,
        string? videoUrl = null)
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
