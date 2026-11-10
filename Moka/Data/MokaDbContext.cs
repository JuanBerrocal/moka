using Microsoft.EntityFrameworkCore;
using Moka.Models;

namespace Moka.Data
{
    public class MokaDbContext : DbContext
    {

        public DbSet<Store> Stores => Set<Store>();
        public DbSet<Machine> Machines => Set<Machine>();
        public DbSet<Transaction> Transactions => Set<Transaction>();
        public DbSet<TransactionLine> TransactionLines => Set<TransactionLine>();

        public MokaDbContext(DbContextOptions<MokaDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Store>().HasIndex(s => s.SapCode);

            modelBuilder.Entity<Machine>()
                .HasOne(t => t.Store)
                .WithMany()
                .HasForeignKey(t => t.StoreId)
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

            modelBuilder.Entity<TransactionLine>()
                .HasOne(tl => tl.Transaction)
                .WithMany(t => t.TransactionLines)
                .HasForeignKey(tl => tl.TransactionId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TransactionLine>()
                .HasOne(tl => tl.Machine)
                .WithMany()
                .HasForeignKey(tl => tl.MachineId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TransactionLine>()
                .HasKey(tl => new { tl.TransactionId, tl.LineId });
        }
    }
}
