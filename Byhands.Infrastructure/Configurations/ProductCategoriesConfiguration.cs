using Byhands.Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Byhands.Infrastructure.Configurations;

public class ProductCategoriesConfiguration : IEntityTypeConfiguration<ProductCategory>
{
    public void Configure(EntityTypeBuilder<ProductCategory> builder)
    {
        builder.HasKey(i => i.Id);

        builder.Property(i => i.Name)
            .HasMaxLength(50);

        builder.Property(i => i.Description)
            .HasMaxLength(225);

        builder.ToTable(nameof(ProductCategory), ConfigurationSettings.BusinessDbschema);
    }
}