using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Review.Domain.Entities.Items;
using Review.Domain.Entities.Spaces;

namespace Review.Infrastructure.Configurations;

public class ItemCategoriesConfiguration : IEntityTypeConfiguration<ItemCategory>
{
    public void Configure(EntityTypeBuilder<ItemCategory> builder)
    {
        builder.HasKey(i => i.Id);

        builder.Property(i => i.Name)
            .HasMaxLength(50);

        builder.Property(i => i.Description)
            .HasMaxLength(225);

        builder.ToTable(nameof(ItemCategory), ConfigurationSettings.BusinessDbschema);
    }
}