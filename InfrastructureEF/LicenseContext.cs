using System.Diagnostics;
using System.Security.Policy;
using Microsoft.EntityFrameworkCore;
using MySql.EntityFrameworkCore.Extensions;

namespace InfrastructureEF;

public class LicenseContext : DbContext
{
    public DbSet<LicenseModels.User> User { get; set; }

    public DbSet<LicenseModels.Customer> Customer { get; set; }

    public DbSet<LicenseModels.License> License { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        
// #if DEBUG
//         optionsBuilder.LogTo(Console.WriteLine);
         optionsBuilder.UseMySQL("server=localhost;database=Panel;user=root;password=mysql"); // local
// #endif

        //optionsBuilder.UseMySQL("server=145.220.74.142;database=Panel;user=root;password=P@$$W0RD"); // dev extern
        //optionsBuilder.UseMySQL("server=172.16.2.33:3306;database=Panel;user=root;password=mysql"); // dev
        //optionsBuilder.UseMySQL("server=172.16.2.53:3306;database=Panel;user=root;password=mysql"); // acceptation
        //optionsBuilder.UseMySQL("server=172.16.2.43:3306;database=Panel;user=root;password=mysql"); // test
        //optionsBuilder.UseMySQL("server=172.16.2.11:3306;database=Panel;user=root;password=mysql"); // live

        // 172.16.2.33 - DEV
        // 172.16.2.53 - ACCEP
        // 172.16.2.43 - TEST
        // 172.16.2.11 - LIVE
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
            entity.Property(e => e.Adress).IsRequired();
            entity.Property(e => e.Country).IsRequired();
        });

        modelBuilder.Entity<LicenseModels.License>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired();
            entity.Property(e => e.SkuPartNumber).IsRequired();
            entity.Property(e => e.Status).IsRequired();
        });
    }
}