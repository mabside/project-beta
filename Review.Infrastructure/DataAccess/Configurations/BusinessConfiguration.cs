using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Review.Domain.Entities.Businesses;

namespace Review.Infrastructure.DataAccess.Configurations;

class BusinessConfiguration : IEntityTypeConfiguration<Business>
{
    public void Configure(EntityTypeBuilder<Business> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name)
            .HasMaxLength(50);

        builder.Property(c => c.Description)
            .HasMaxLength(225);

        builder.Property(c => c.Email)
            .HasMaxLength(100);

        builder.Property(c => c.LogoUrl)
            .HasMaxLength(255);

        builder.Property(c => c.BannerUrl)
            .HasMaxLength(225);

        builder.Property(c => c.WebsiteUrl)
            .HasMaxLength(225);

        builder.Property(c => c.Location)
            .HasColumnType("jsonb");

        builder.Property(c => c.SocialHandles)
            .HasColumnType("jsonb");

        builder.ToTable(nameof(Business), ConfigurationSettings.BusinessDbschema);
    }
}
