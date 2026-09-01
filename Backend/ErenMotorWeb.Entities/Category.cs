using System.ComponentModel.DataAnnotations;

namespace ErenMotorWeb.Entities
{
    public class Category : IEntityBase
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public string Slug { get; set; } = string.Empty;

        public string? Description { get; set; }

        public int? ParentCategoryId { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        // Navigation Properties

        public Category? ParentCategory { get; set; }

        public ICollection<Category>? SubCategories { get; set; }  

        public ICollection<Product>? Products { get; set; } 
            
    }
}
