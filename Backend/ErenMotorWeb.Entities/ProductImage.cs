
using System.ComponentModel.DataAnnotations;

namespace ErenMotorWeb.Entities
{
    public class ProductImage : IEntityBase
    {
        [Key]
        public int Id { get; set; }

        public int ProductId { get; set; }

        public string ImageUrl { get; set; } = string.Empty;

        public string? AltText { get; set; }

        public int SortOrder { get; set; }

        public bool IsMain { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation Property

        public virtual Product? Product { get; set; } 
    }
}
