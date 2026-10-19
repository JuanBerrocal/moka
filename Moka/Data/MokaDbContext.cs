using Microsoft.EntityFrameworkCore;
using Moka.Models;

namespace Moka.Data
{
    public class MokaDbContext : DbContext
    {

        public DbSet<Store> Stores => Set<Store>();
        public DbSet<Machine> Machines => Set<Machine>();
        public DbSet<Transaction> Transactions => Set<Transaction>();

        public MokaDbContext(DbContextOptions<MokaDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Store>().HasIndex(s => s.SapCode);

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Machine)
                .WithMany()
                .HasForeignKey(t => t.MachineId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Origin)
                .WithMany()
                .HasForeignKey(t => t.OriginId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Destination)
                .WithMany()
                .HasForeignKey(t => t.DestinationId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
