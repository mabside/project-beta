using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Byhands.Domain.Entities.Links;

namespace Byhands.Infrastructure.Configurations;

public class LinksConfiguration : IEntityTypeConfiguration<Link>
{
    public void Configure(EntityTypeBuilder<Link> builder)
    {
        builder.HasKey(s => s.Id);

        builder.ToTable(nameof(Link), ConfigurationSettings.BusinessDbschema);
    }
}