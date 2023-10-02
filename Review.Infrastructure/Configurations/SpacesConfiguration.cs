using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Review.Domain.Entities.Spaces;

namespace Review.Infrastructure.Configurations;

public class SpacesConfiguration : IEntityTypeConfiguration<Space>
{
    public void Configure(EntityTypeBuilder<Space> builder)
    {
        builder.HasKey(s => s.Id);

        builder.Property(s => s.Name)
            .HasMaxLength(50);

        builder.Property(s => s.Description)
            .HasMaxLength(225);

        builder.ToTable(nameof(Space), ConfigurationSettings.SpaceDbschema);
    }
}