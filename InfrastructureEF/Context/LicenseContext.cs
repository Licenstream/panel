using System;
using System.Collections.Generic;
using InfrastructureEF.LicenseModels;
using Microsoft.EntityFrameworkCore;

namespace InfrastructureEF.Context;

public partial class LicenseContext : DbContext
{
    private readonly string _connectionString;

    public LicenseContext(string connectionString)
    {
        _connectionString = connectionString;
    }

    public virtual DbSet<Customer> Customer { get; set; }

    public virtual DbSet<CustomerLicense> CustomerLicense { get; set; }

    public virtual DbSet<License> License { get; set; }

    public virtual DbSet<LicenseServiceStatus> LicenseServiceStatus { get; set; }

    public virtual DbSet<ProductNames> ProductNames { get; set; }

    public virtual DbSet<ServiceStatus> ServiceStatus { get; set; }

    public virtual DbSet<User> User { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseMySQL(_connectionString);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CustomerLicense>(entity => { entity.HasNoKey(); });
        modelBuilder.Entity<LicenseServiceStatus>(entity => { entity.HasNoKey(); });
        
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Adress).IsRequired();
            entity.Property(e => e.Country).IsRequired();
            entity.Property(e => e.Name).IsRequired();
            entity.Property(e => e.Type).IsRequired();
        });

        modelBuilder.Entity<License>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.SkuId).IsRequired();
            entity.Property(e => e.Status).IsRequired();
            entity.Property(e => e.CreatedDate).IsRequired();
        });

        modelBuilder.Entity<ProductNames>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.Guid).HasColumnName("GUID");
            entity.Property(e => e.ProductDisplayName).HasColumnName("Product_Display_Name");
            entity.Property(e => e.ServicePlanId).HasColumnName("Service_Plan_Id");
            entity.Property(e => e.ServicePlanName).HasColumnName("Service_Plan_Name");
            entity.Property(e => e.ServicePlansIncludedFriendlyNames)
                .HasColumnName("Service_Plans_Included_Friendly_Names");
            entity.Property(e => e.StringId).HasColumnName("String_Id");
        });

        modelBuilder.Entity<ServiceStatus>(entity => { entity.HasKey(e => e.Id); });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired();
            entity.Property(e => e.Email).IsRequired();
        });
    }
}