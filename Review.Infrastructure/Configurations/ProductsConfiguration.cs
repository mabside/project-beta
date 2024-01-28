using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Byhands.Domain.Entities.Products;

namespace Byhands.Infrastructure.Configurations;

public class ProductsConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(i => i.Id);

        builder.Property(i => i.Name)
            .HasMaxLength(50);

        builder.Property(i => i.Description)
            .HasMaxLength(225);

        builder.ToTable(nameof(Product), ConfigurationSettings.BusinessDbschema);
    }
}