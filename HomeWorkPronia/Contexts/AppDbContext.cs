using HomeWorkPronia.Models;
using HomeWorkPronia.Models.Common;
using Microsoft.EntityFrameworkCore;
using System;

namespace HomeWorkPronia.Contexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Card> Cards { get; set; } = null!;
        public DbSet<Slider> Sliders { get; set; } = null!;
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Card>().HasQueryFilter(s=>s.IsDeleted == false);
            modelBuilder.Entity<Product>().HasQueryFilter(p=>p.IsDeleted == false);
            modelBuilder.Entity<Category>().HasQueryFilter(c => c.IsDeleted == false);
            base.OnModelCreating(modelBuilder);
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries<BaseSectionEntity>();
            foreach(var entry in entries)
            {
               switch(entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = "Musa";
                        entry.Entity.UpdatedBy = "Musa";
                        entry.Entity.CreatedDate = DateTime.UtcNow;
                        entry.Entity.UpdateTime = DateTime.UtcNow;
                        break;
                    case EntityState.Modified:
                        entry.Entity.UpdatedBy = "Musa";
                        entry.Entity.UpdateTime = DateTime.UtcNow;
                        break;
                        default: break;

                } 
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
