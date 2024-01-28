using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Byhands.Domain.Entities.Feedbacks;

namespace Byhands.Infrastructure.Configurations;

public class FeedbacksConfiguration : IEntityTypeConfiguration<Feedback>
{
    public void Configure(EntityTypeBuilder<Feedback> builder)
    {
        builder.HasKey(x => x.Id);

        builder.ToTable(nameof(Feedback), ConfigurationSettings.FeedbackDbschema);
    }
}
