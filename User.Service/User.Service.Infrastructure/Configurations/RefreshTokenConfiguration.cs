using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using User.Service.Domain.Entities;

namespace User.Service.Infrastructure.Configurations;

public class RefreshTokenConfiguration: IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.HasKey(r => r.UserId);
        builder.HasOne(rt => rt.User) 
            .WithOne() 
            .HasForeignKey<RefreshToken>(rt => rt.UserId);
        builder.Property(rt => rt.Token).IsRequired();
        builder.Property(rt => rt.ExpiresAt).IsRequired();
    }
}