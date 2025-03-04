using Microsoft.EntityFrameworkCore;
using User.Service.Domain.Entities;
using User.Service.Infrastructure.Configurations;

namespace User.Service.Infrastructure;

public class ApplicationDbContext : DbContext
{
    public DbSet<AppUser> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Profile> Profiles { get; set; }
    public DbSet<Follow> Follows { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<StreamCategory> StreamCategories { get; set; }
    public DbSet<Report> Reports { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new RoleConfiguration());
        modelBuilder.ApplyConfiguration(new ProfileConfiguration());
        modelBuilder.ApplyConfiguration(new FollowConfiguration());
        modelBuilder.ApplyConfiguration(new RefreshTokenConfiguration());
        modelBuilder.ApplyConfiguration(new StreamCategoryConfiguration());
        modelBuilder.ApplyConfiguration(new ReportConfiguration());
    }
}