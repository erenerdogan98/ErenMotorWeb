using System.ComponentModel.DataAnnotations;

namespace ErenMotorWeb.Entities
{
    public class ProcessedOrder : IEntityBase
    {
        [Key]
        public int Id { get; set; }

        public int PlatformConnectionId { get; set; }

        public string PlatformOrderId { get; set; } = string.Empty;

        public string? PlatformOrderLineId { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public string OrderStatus { get; set; } = string.Empty;

        public bool StockDeducted { get; set; } = false;

        public bool StockRestored { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        // Navigation Properties

        public virtual Product? Product { get; set; }

        public virtual PlatformConnection? PlatformConnection { get; set; }
    }
}
