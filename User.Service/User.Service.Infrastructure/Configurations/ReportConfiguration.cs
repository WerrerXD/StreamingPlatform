using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using User.Service.Domain.Entities;

namespace User.Service.Infrastructure.Configurations;

public class ReportConfiguration: IEntityTypeConfiguration<Report>
{
    public void Configure(EntityTypeBuilder<Report> builder)
    {
        builder.HasKey(r => r.Id);
        builder.HasOne(r => r.Reporter)
            .WithMany()
            .HasForeignKey(r => r.ReporterId);
        builder.HasOne(r => r.Reported)
            .WithMany()
            .HasForeignKey(r => r.ReportedId);
        builder.Property(r => r.Reason)
            .IsRequired();
        builder.Property(r => r.Status)
            .IsRequired()
            .HasMaxLength(20);
        builder.Property(r => r.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");
    }
}