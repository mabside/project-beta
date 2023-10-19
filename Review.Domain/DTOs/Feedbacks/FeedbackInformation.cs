namespace Review.Domain.DTOs.Feedbacks;

public class FeedbackInformation
{
    public string Note { get; set; } = string.Empty;
    public string? ImageUrl { get; set; }
    public string? AudioUrl { get; set; }
    public string? VideoUrl { get; set; }
    public int Star { get; set; }
    public string FeedbackType { get; set; } = string.Empty;

    public Guid BusinessId { get; set; }
    public Guid ItemId { get; set; }
}
