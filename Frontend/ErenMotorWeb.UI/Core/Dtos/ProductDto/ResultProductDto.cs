using ErenMotorWeb.UI.Core.Dtos.ProductImageDto;

namespace ErenMotorWeb.UI.Core.Dtos.ProductDto
{
    public class ResultProductDto
    {
        public int Id { get; set; }

        public string ProductName { get; set; } = string.Empty;

        public string Slug { get; set; } = string.Empty;

        public string Barcode { get; set; } = string.Empty;

        public string? Sku { get; set; }

        public string? ShortDescription { get; set; }

        public string? Description { get; set; }

        public decimal? Price { get; set; }

        public int Stock { get; set; }

        public bool IsActive { get; set; }

        public bool IsFeatured { get; set; }

        public int CategoryId { get; set; }

        public string? CategoryName { get; set; }

        public int BrandId { get; set; }

        public string? BrandName { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public ICollection<ResultProductImageDto> Images { get; set; }
            = [];
    }
}
