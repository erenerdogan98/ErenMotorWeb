using ErenMotorWeb.Entities.Enums;
using System.ComponentModel.DataAnnotations;


namespace ErenMotorWeb.Entities
{
    public class Product : IEntityBase
    {
        [Key]
        public int Id { get; set; }

        public string ProductName { get; set; } = string.Empty;

        public string Slug { get; set; } = string.Empty;

        public string Barcode { get; set; } = string.Empty;

        public string? Sku { get; set; }

        public string? ShortDescription { get; set; }

        public string? Description { get; set; }

        public decimal? Price { get; set; }

        public int Stock { get; set; }

        public bool IsActive { get; set; } = true;

        public bool IsFeatured { get; set; } = false;
        public bool IsDeleted { get; set; } = false;

        public int CategoryId { get; set; }

        public int BrandId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        public int SafetyStock { get; set; } = 0;

        public ProductColors Color { get; set; }
        public string ModelCode { get; set; } = string.Empty;

        [Timestamp]
        public byte[]? RowVersion { get; set; }

        // Navigation Properties

        public virtual Category? Category { get; set; } 

        public virtual Brand? Brand { get; set; } 

        public ICollection<ProductImage>? Images { get; set; }

        public ICollection<ProductMapping>? ProductMappings { get; set; }

        public int SupplierId { get; set; }

        public int OriginId { get; set; }

        public virtual Supplier? Supplier { get; set; }
        public virtual Origin? Origin { get; set; }

    }
}
