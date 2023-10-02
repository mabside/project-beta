using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Review.Domain.Entities.Customers;

namespace Review.Infrastructure.Configurations;

internal class CustomerConfigurations : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Email)
            .HasMaxLength(100);

        builder.ToTable(nameof(Customer), ConfigurationSettings.CustomerDbschema);
    }
}
