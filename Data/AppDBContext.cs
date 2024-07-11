using Microsoft.EntityFrameworkCore;
using SalesSystemApp.Models;

namespace SalesSystemApp.Data
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<SaleDetail> SaleDetails { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Product configuration
            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("product");
                entity.HasKey(p => p.ProductId);
                entity.Property(p => p.Name)
                      .IsRequired()
                      .HasMaxLength(100);
                entity.Property(p => p.Price)
                      .IsRequired()
                      .HasColumnType("decimal(18,2)");
                entity.Property(p => p.Description)
                      .IsRequired()
                      .HasMaxLength(500);
            });

            // Sale configuration
            modelBuilder.Entity<Sale>(entity =>
            {
                entity.ToTable("sale");
                entity.HasKey(s => s.SaleId);
                entity.HasOne(s => s.User)
                      .WithMany(u => u.Sales)
                      .HasForeignKey(s => s.UserId);
                entity.Property(s => s.TotalBeforeTax)
                      .IsRequired()
                      .HasColumnType("decimal(18,2)");
                entity.Property(s => s.TotalAfterTax)
                      .IsRequired()
                      .HasColumnType("decimal(18,2)");
            });

            // SaleDetail configuration
            modelBuilder.Entity<SaleDetail>(entity =>
            {
                entity.ToTable("sale_detail");
                entity.HasKey(sd => sd.SaleDetailId);
                entity.HasOne(sd => sd.Sale)
                      .WithMany(s => s.SaleDetails)
                      .HasForeignKey(sd => sd.SaleId);
                entity.HasOne(sd => sd.Product)
                      .WithMany(p => p.SaleDetails)
                      .HasForeignKey(sd => sd.ProductId);
                entity.Property(sd => sd.Price)
                      .IsRequired()
                      .HasColumnType("decimal(18,2)");
            });

            // User configuration
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");
                entity.HasKey(u => u.UserId);
                entity.Property(u => u.Name)
                      .IsRequired()
                      .HasMaxLength(100);
                entity.Property(u => u.Email)
                      .IsRequired()
                      .HasMaxLength(100);
                entity.Property(u => u.Password)
                      .IsRequired()
                      .HasMaxLength(100);
                entity.Property(u => u.BirthDate)
                      .IsRequired();
                entity.Property(u => u.Phone)
                      .IsRequired()
                      .HasMaxLength(15);
            });
        }

    }
}
