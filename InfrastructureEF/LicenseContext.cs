using System.Diagnostics;
using System.Security.Policy;
using Microsoft.EntityFrameworkCore;
using MySql.EntityFrameworkCore.Extensions;

namespace InfrastructureEF;

public class LicenseContext : DbContext
{
    private readonly string _connectionString;

    public LicenseContext(string connectionString)
    {
        _connectionString = connectionString;
    }
    
    public DbSet<LicenseModels.User> User { get; set; }

    public DbSet<LicenseModels.Customer> Customer { get; set; }

    public DbSet<LicenseModels.License> License { get; set; }
    
    public DbSet<LicenseModels.ServiceStatus> ServiceStatus { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
#if DEBUG
        optionsBuilder.LogTo(Console.WriteLine);
#endif
        optionsBuilder.UseMySQL(_connectionString); // local
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<LicenseModels.User>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired();
            entity.Property(e => e.Email).IsRequired();
        });

        modelBuilder.Entity<LicenseModels.Customer>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired();
            entity.Property(e => e.Type).IsRequired();
            entity.Property(e => e.Adress).IsRequired();
            entity.Property(e => e.Country).IsRequired();
        });

        modelBuilder.Entity<LicenseModels.License>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.SkuId).IsRequired();
            entity.Property(e => e.Status).IsRequired();
            entity.Property(e => e.CreatedDate).IsRequired();
        });
        
        modelBuilder.Entity<LicenseModels.ServiceStatus>(entity =>
        {
            entity.HasKey(e => e.Id);
        });
    }
}