using System.ComponentModel.DataAnnotations;

namespace ErenMotorWeb.Entities
{
    public class SyncLog : IEntityBase
    {
        [Key]
        public int Id { get; set; }

        public int? ProductId { get; set; }

        public int? PlatformConnectionId { get; set; }

        public  SyncType SyncType { get; set; }

        public  SyncStatus Status { get; set; }

        public int? StockBefore { get; set; }

        public int? StockAfter { get; set; }

        public string? ErrorMessage { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation Properties

        public virtual Product? Product { get; set; }

        public virtual PlatformConnection? PlatformConnection { get; set; }
    }
}
