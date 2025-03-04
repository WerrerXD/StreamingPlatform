using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using User.Service.Domain.Entities;

namespace User.Service.Infrastructure.Configurations;

public class StreamCategoryConfiguration: IEntityTypeConfiguration<StreamCategory>
{
    public void Configure(EntityTypeBuilder<StreamCategory> builder)
    {
        builder.HasKey(sc => sc.Id);
        builder.Property(sc => sc.Name)
            .IsRequired()
            .HasMaxLength(50);
        builder.Property(r => r.Name).IsRequired().HasMaxLength(100);
    }
}