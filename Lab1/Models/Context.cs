using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Lab1.Models
{
    public class Context : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Provider> Providers { get; set; }
        public DbSet<Buy> Buys { get; set; }
        public DbSet<BuyProduct> BuyProducts { get; set; }

        public Context(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BuyProduct>()
                .HasKey(bp => new { bp.BuyId, bp.ProductId });

            modelBuilder.Entity<BuyProduct>()
                .HasOne(bp => bp.Buy)
                .WithMany(b => b.BuyProducts)
                .HasForeignKey(bp => bp.BuyId);

            modelBuilder.Entity<BuyProduct>()
                .HasOne(bp => bp.Product)
                .WithMany(p => p.BuyProducts)
                .HasForeignKey(bp => bp.ProductId);
        }
    }
}
