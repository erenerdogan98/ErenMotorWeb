using ErenMotorWeb.Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace ErenMotorWeb.Entities
{
    public class StockMovement : IEntityBase
    {
        [Key]
        public int Id { get; set; }

        public int ProductId { get; set; }

        public int PreviousStock { get; set; }

        public int NewStock { get; set; }

        public int Change { get; set; }

        public virtual StockChangeSource Source { get; set; }

        public string? Note { get; set; }

        public int? RelatedProcessedOrderId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation Properties

        public virtual Product? Product { get; set; }
    }
}
