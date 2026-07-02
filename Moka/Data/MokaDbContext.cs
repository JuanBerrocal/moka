using Microsoft.EntityFrameworkCore;
using Moka.Models;

namespace Moka.Data
{
    public class MokaDbContext : DbContext
    {

        public DbSet<Store> Stores => Set<Store>();

        public MokaDbContext(DbContextOptions<MokaDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Store>().HasIndex(s => s.SapCode).IsUnique();
        }
    }
}
