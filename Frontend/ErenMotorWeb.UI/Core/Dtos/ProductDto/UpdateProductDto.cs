namespace ErenMotorWeb.UI.Core.Dtos.ProductDto
{
    public class UpdateProductDto
    {
        public int Id { get; set; }

        public string ProductName { get; set; } = string.Empty;

        public string Slug { get; set; } = string.Empty;

        public string Barcode { get; set; } = string.Empty;

        public string? Sku { get; set; }
        public string ModelCode { get; set; } = string.Empty;

        public int Color { get; set; }
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
        public int SupplierId { get; set; }

        public string? SupplierName { get; set; }

        public int? OriginId { get; set; }
        public string? OriginName { get; set; }

        public string? OriginCode { get; set; }
    }
}
