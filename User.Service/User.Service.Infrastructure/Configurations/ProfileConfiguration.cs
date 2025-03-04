using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using User.Service.Domain.Entities;

namespace User.Service.Infrastructure.Configurations;

public class ProfileConfiguration: IEntityTypeConfiguration<Profile>
{
    public void Configure(EntityTypeBuilder<Profile> builder)
    {
        builder.HasKey(r => r.UserId);
        builder.Property(r => r.ProfileName).IsRequired().HasMaxLength(100);
    }
}