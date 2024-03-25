using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;

namespace Domain;

public class Db : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<License> Licenses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase("TestDB");
    }
}
