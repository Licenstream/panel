using Microsoft.EntityFrameworkCore;

namespace CloudPartners.WebUI.Models
{
    public class Db : DbContext
    {
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("MyDb");
        }
    }
}
