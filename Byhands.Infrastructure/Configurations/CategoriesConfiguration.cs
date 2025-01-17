﻿using Byhands.Domain.Entities.Businesses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Byhands.Infrastructure.Configurations;

class CategoriesConfiguration : IEntityTypeConfiguration<BusinessCategory>
{
    public void Configure(EntityTypeBuilder<BusinessCategory> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name)
            .HasMaxLength(50);

        builder.Property(c => c.Description)
            .HasMaxLength(150);

        builder.ToTable(nameof(BusinessCategory), ConfigurationSettings.BusinessDbschema);
    }
}
