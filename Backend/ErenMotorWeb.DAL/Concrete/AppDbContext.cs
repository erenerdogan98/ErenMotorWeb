using ErenMotorWeb.DAL.Identity;
using ErenMotorWeb.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ErenMotorWeb.DAL.Concrete
{
    public class AppDbContext(
        DbContextOptions<AppDbContext> options)
        : IdentityDbContext<AppUser, AppRole, int>(options)
    {
        // DbSets

        public DbSet<Brand> Brands { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<ProductImage> ProductImages { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            // =====================================================
            // BRAND - PRODUCT
            // Bir Brand'in birçok Product'ı olabilir.
            // Bir Product yalnızca bir Brand'e bağlıdır.
            // =====================================================

            modelBuilder.Entity<Brand>()
                .HasMany(x => x.Products)
                .WithOne(x => x.Brand)
                .HasForeignKey(x => x.BrandId)
                .OnDelete(DeleteBehavior.Restrict);


            // =====================================================
            // CATEGORY - PRODUCT
            // Bir Category'nin birçok Product'ı olabilir.
            // Bir Product yalnızca bir Category'ye bağlıdır.
            // =====================================================

            modelBuilder.Entity<Category>()
                .HasMany(x => x.Products)
                .WithOne(x => x.Category)
                .HasForeignKey(x => x.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);


            // =====================================================
            // CATEGORY - CATEGORY
            // Self Reference
            //
            // Örnek:
            //
            // Bisiklet
            //    ├── Lastikler
            //    ├── İç Lastikler
            //    └── Fren Sistemleri
            //
            // ParentCategoryId nullable olduğu için
            // ana kategorinin parent'ı olmayabilir.
            // =====================================================

            modelBuilder.Entity<Category>()
                .HasMany(x => x.SubCategories)
                .WithOne(x => x.ParentCategory)
                .HasForeignKey(x => x.ParentCategoryId)
                .OnDelete(DeleteBehavior.Restrict);


            // =====================================================
            // PRODUCT - PRODUCT IMAGE
            // Bir Product'ın birçok resmi olabilir.
            // Bir ProductImage yalnızca bir Product'a aittir.
            // =====================================================

            modelBuilder.Entity<Product>()
                .HasMany(x => x.Images)
                .WithOne(x => x.Product)
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Cascade);


            // =====================================================
            // UNIQUE INDEXES
            // =====================================================

            modelBuilder.Entity<Product>()
                .HasIndex(x => x.Barcode)
                .IsUnique();

            modelBuilder.Entity<Product>()
                .HasIndex(x => x.Slug)
                .IsUnique();

            modelBuilder.Entity<Brand>()
                .HasIndex(x => x.Slug)
                .IsUnique();

            modelBuilder.Entity<Category>()
                .HasIndex(x => x.Slug)
                .IsUnique();
        }
    }
}