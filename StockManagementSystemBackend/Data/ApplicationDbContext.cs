using Microsoft.EntityFrameworkCore;
using StockManagementSystemBackend.Models;

namespace StockManagementSystemBackend.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<RoleMaster> RoleMaster { get; set; }

        public DbSet<UserMaster> UserMaster { get; set; }

        public DbSet<TenantMaster> TenantMaster { get; set; }

        public DbSet<CompanyMaster> CompanyMaster { get; set; }

        public DbSet<ProductMaster> ProductMaster { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TenantMaster>()
           .HasIndex(u => u.TenantName)
           .IsUnique();

            modelBuilder.Entity<RoleMaster>()
           .HasIndex(u => new { u.RoleName, u.TenantId })
           .IsUnique();

            modelBuilder.Entity<UserMaster>()
           .HasIndex(u => u.ContactNumber)
           .IsUnique();

            modelBuilder.Entity<UserMaster>()
           .HasIndex(u => u.Email)
           .IsUnique();

            modelBuilder.Entity<UserMaster>()
           .HasIndex(u => u.UserName)
           .IsUnique();

            modelBuilder.Entity<CompanyMaster>()
           .HasIndex(u => u.CompanyName)
           .IsUnique();

            modelBuilder.Entity<ProductMaster>()
          .HasIndex(u => u.ProductName)
          .IsUnique();


            var cascadeFKs = modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFKs)
                fk.DeleteBehavior = DeleteBehavior.Restrict;

            base.OnModelCreating(modelBuilder);
        }
    }
}
