using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsumerIrongridC2System.Models;
using Microsoft.EntityFrameworkCore;

namespace ConsumerIrongridC2System.Data
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options) { }
        public DbSet<Unit> Units => Set<Unit>();
        public DbSet<Asset> Assets => Set<Asset>();
        public DbSet<AssetLiveStatus> AssetLiveStatuses => Set<AssetLiveStatus>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Asset>()
                .HasOne(e => e.Unit)
                .WithMany(e => e.Assets)
                .HasForeignKey(e => e.UnitId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Asset>()
                .HasOne(e => e.AssetLiveStatus)
                .WithOne(e => e.Asset)
                .HasForeignKey<AssetLiveStatus>(e => e.AssetId);
        }
    }
}
