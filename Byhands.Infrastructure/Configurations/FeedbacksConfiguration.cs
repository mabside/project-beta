using Byhands.Domain.Entities.Feedbacks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Byhands.Infrastructure.Configurations;

public class FeedbacksConfiguration : IEntityTypeConfiguration<Feedback>
{
    public void Configure(EntityTypeBuilder<Feedback> builder)
    {
        builder.HasKey(x => x.Id);

        builder.ToTable(nameof(Feedback), ConfigurationSettings.FeedbackDbschema);
    }
}
