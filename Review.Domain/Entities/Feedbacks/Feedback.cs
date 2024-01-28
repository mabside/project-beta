using Byhands.Domain.Entities.Products;
using Byhands.Models.Bases;

namespace Byhands.Domain.Entities.Feedbacks;

public partial class Feedback : BaseEntity<Guid>
{
    public string Note { get; private set; }
    public string? ImageUrl { get; private set; }
    public string? AudioUrl { get; private set; }
    public string? VideoUrl { get; private set; }
    public int Star { get; private set; }
    public string FeedbackType { get; private set; }

    public Guid ProductId { get; private set; }

    public virtual Product Product { get; private set; }

    private Feedback() { }

    private Feedback(
        string note,
        int star,
        FeedbackType ByhandsType,
        string? imageUrl = null,
        string? audioUrl = null,
        string? videoUrl = null)
    {
        Note = note;
        Star = star;
        FeedbackType = ByhandsType.ToString();
        ImageUrl = imageUrl;
        AudioUrl = audioUrl;
        VideoUrl = videoUrl;
    }

    public static Result<Feedback> Create(
        string note,
        int star,
        FeedbackType ByhandsType,
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
                ByhandsType: ByhandsType));

        if (result.HasError)
            return result.Error;

        return result.Value;
    }
}
