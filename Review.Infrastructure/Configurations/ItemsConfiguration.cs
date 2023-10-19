using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Review.Domain.Entities.Items;

namespace Review.Infrastructure.Configurations;

public class ItemsConfiguration : IEntityTypeConfiguration<Item>
{
    public void Configure(EntityTypeBuilder<Item> builder)
    {
        builder.HasKey(i => i.Id);

        builder.Property(i => i.Name)
            .HasMaxLength(50);

        builder.Property(i => i.Description)
            .HasMaxLength(225);

        builder.ToTable(nameof(Item), ConfigurationSettings.BusinessDbschema);
    }
}