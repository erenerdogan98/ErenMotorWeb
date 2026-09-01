using System.ComponentModel.DataAnnotations;

namespace ErenMotorWeb.Entities
{
    public class ProductMapping : IEntityBase
    {
        [Key]
        public int Id { get; set; }

        public int ProductId { get; set; }

        public int PlatformConnectionId { get; set; }

        // Trendyol/Hepsiburada tarafındaki ürünü tanımlayan id/barkod eşleşmesi
        public string PlatformProductId { get; set; } = string.Empty;

        public string? PlatformBarcode { get; set; }

        // O kanalda şu an aktif olarak push edilen miktar
        public int AllocatedStock { get; set; }

        // Bu kanal için hedeflenen normal tahsis miktarı - kanal 0'a inince buna göre doldurulur
        public int TargetStock { get; set; }

        // Platformun bize en son bildirdiği/senkronize ettiğimiz değer (reconciliation için referans)
        public int? CurrentStockOnPlatform { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime? LastSyncedAt { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        // Navigation Properties

        public virtual Product? Product { get; set; }

        public virtual PlatformConnection? PlatformConnection { get; set; }
    }
}
