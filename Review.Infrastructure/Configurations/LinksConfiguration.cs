using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Review.Domain.Entities.Links;

namespace Review.Infrastructure.Configurations;

public class LinksConfiguration : IEntityTypeConfiguration<Link>
{
    public void Configure(EntityTypeBuilder<Link> builder)
    {
        builder.HasKey(s => s.Id);

        builder.ToTable(nameof(Link), ConfigurationSettings.BusinessDbschema);
    }
}