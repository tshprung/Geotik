using Microsoft.EntityFrameworkCore;

namespace Geotik.Entities
{
    public class GeotikContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Account> Accounts { get; set; }

        public DbSet<Loan> Loans { get; set; }

        public GeotikContext(DbContextOptions<GeotikContext> options)
    : base(options)
        {
            // Database.Migrate();
            Database.EnsureCreated();
        }
    }
}
