using HomeWorkPronia.Migrations;
using HomeWorkPronia.Models;
using HomeWorkPronia.Models.Common;
using HomeWorkPronia.Models.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace HomeWorkPronia.Contexts
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public AppDbContext(DbContextOptions<AppDbContext> options,IHttpContextAccessor httpContextAccessor) : base(options) 
        {
            _contextAccessor = httpContextAccessor;
        }
        public DbSet<Card> Cards { get; set; } = null!;

        public DbSet<HomeWorkPronia.Models.Slider> Sliders { get; set; } = null!;
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
            string? userName = "Admin";
           var identity = _contextAccessor?.HttpContext?.User.Identity;
            if(identity is not null)
            {
                userName = identity.IsAuthenticated ? identity.Name : "Admin";

            }





            var entries = ChangeTracker.Entries<HomeWorkPronia.Models.Common.BaseSectionEntity>();
            foreach(var entry in entries)
            {
                
               switch(entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = userName;
                        entry.Entity.UpdatedBy = userName;
                        entry.Entity.CreatedDate = DateTime.UtcNow;
                        entry.Entity.UpdateTime = DateTime.UtcNow;
                        break;
                    case EntityState.Modified:
                        entry.Entity.UpdatedBy = userName;
                        entry.Entity.UpdateTime = DateTime.UtcNow;
                        break;
                        default: break;

                } 
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
