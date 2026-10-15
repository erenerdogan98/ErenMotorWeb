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


        // Stok Senkronizasyonu DbSet'leri
        public DbSet<PlatformConnection> PlatformConnections { get; set; }
        public DbSet<ProductMapping> ProductMappings { get; set; }
        public DbSet<ProcessedOrder> ProcessedOrders { get; set; }
        public DbSet<StockMovement> StockMovements { get; set; }
        public DbSet<SyncLog> SyncLogs { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }

        public DbSet<Origin> Origins { get; set; }
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
            // PLATFORM CONNECTION - PRODUCT MAPPING
            // Bir PlatformConnection'ın (örn. Trendyol hesabı) birçok
            // ProductMapping'i olabilir.
            // Restrict: bağlı mapping'ler varken bağlantı yanlışlıkla
            // silinemesin - önce mapping'ler temizlenmeli.
            // =====================================================
            modelBuilder.Entity<PlatformConnection>()
                .HasMany(x => x.ProductMappings)
                .WithOne(x => x.PlatformConnection)
                .HasForeignKey(x => x.PlatformConnectionId)
                .OnDelete(DeleteBehavior.Restrict);

            // =====================================================
            // PRODUCT - PRODUCT MAPPING
            // Bir Product'ın birden çok platformda (Trendyol, Hepsiburada)
            // mapping'i olabilir.
            // Cascade: Product silinirse, o ürüne ait platform
            // eşlemelerinin de silinmesi mantıklı (tek yönlü cascade -
            // yukarıdaki PlatformConnection tarafı Restrict olduğu için
            // SQL Server'da "multiple cascade paths" hatası oluşmaz).
            // =====================================================
            modelBuilder.Entity<Product>()
                .HasMany(x => x.ProductMappings)
                .WithOne(x => x.Product)
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            // =====================================================
            // PLATFORM CONNECTION - PROCESSED ORDER
            // PRODUCT - PROCESSED ORDER
            // Sipariş/idempotency kayıtları denetim amaçlı - Product ya da
            // PlatformConnection silinse bile bu geçmiş korunmalı, bu yüzden
            // her iki taraf da Restrict.
            // =====================================================
            modelBuilder.Entity<PlatformConnection>()
                .HasMany<ProcessedOrder>()
                .WithOne(x => x.PlatformConnection)
                .HasForeignKey(x => x.PlatformConnectionId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Product>()
                .HasMany<ProcessedOrder>()
                .WithOne(x => x.Product)
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            // =====================================================
            // PRODUCT - STOCK MOVEMENT
            // Stok denetim/audit geçmişi - Product silinse bile korunmalı.
            // =====================================================
            modelBuilder.Entity<Product>()
                .HasMany<StockMovement>()
                .WithOne(x => x.Product)
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            // =====================================================
            // PRODUCT - SYNC LOG
            // PLATFORM CONNECTION - SYNC LOG
            // ProductId ve PlatformConnectionId nullable (bazı sync
            // kayıtları tüm ürünleri kapsayan genel job çalıştırmaları
            // olabilir), bu yüzden ilişkiler de opsiyonel.
            // =====================================================
            modelBuilder.Entity<Product>()
                .HasMany<SyncLog>()
                .WithOne(x => x.Product)
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PlatformConnection>()
                .HasMany<SyncLog>()
                .WithOne(x => x.PlatformConnection)
                .HasForeignKey(x => x.PlatformConnectionId)
                .OnDelete(DeleteBehavior.Restrict);

            // =====================================================
            // UNIQUE INDEXES
            // =====================================================
            modelBuilder.Entity<Product>()
                .HasIndex(x => x.Barcode)
                .IsUnique();

            modelBuilder.Entity<Product>()
                .HasIndex(x => x.Slug);

            modelBuilder.Entity<Brand>()
                .HasIndex(x => x.Slug)
                .IsUnique();

            modelBuilder.Entity<Category>()
                .HasIndex(x => x.Slug)
                .IsUnique();

            // Aynı ürün, aynı platform bağlantısına iki kez mapping'lenemesin
            modelBuilder.Entity<ProductMapping>()
                .HasIndex(x => new { x.ProductId, x.PlatformConnectionId })
                .IsUnique();

            // Idempotency: aynı platform siparişi + aynı ürün satırı
            // iki kez işlenemesin (webhook tekrarına karşı en kritik kısıtlama)
            modelBuilder.Entity<ProcessedOrder>()
                .HasIndex(x => new { x.PlatformConnectionId, x.PlatformOrderId, x.ProductId })
                .IsUnique();

            // SUPPLIER - PRODUCT
            // Bir Supplier'ın birçok Product'ı olabilir.
            // Bir Product yalnızca bir Supplier'a bağlıdır.
            // =====================================================
            modelBuilder.Entity<Supplier>()
                .HasMany(x => x.Products)
                .WithOne(x => x.Supplier)
                .HasForeignKey(x => x.SupplierId)
                .OnDelete(DeleteBehavior.Restrict);

            // ORIGIN - PRODUCT
            // Bir Origin'in birçok Product'ı olabilir.
            // Bir Product'ın bir Origin'i olabilir.
            // OriginId nullable olduğu için ilişki opsiyoneldir.
            // =====================================================
            modelBuilder.Entity<Origin>()
                .HasMany<Product>()
                .WithOne(x => x.Origin)
                .HasForeignKey(x => x.OriginId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}