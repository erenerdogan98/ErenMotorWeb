namespace ErenMotorWeb.Dto.ProductDto
{
    public class CreateProductDto
    {
        public string ProductName { get; set; } = string.Empty;

        public string Slug { get; set; } = string.Empty;

        public string ModelCode { get; set; } = string.Empty;

        public string? ShortDescription { get; set; }

        public string? Description { get; set; }

        public bool IsActive { get; set; } = true;

        public bool IsFeatured { get; set; } = false;

        public int CategoryId { get; set; }

        public int BrandId { get; set; }

        public int SupplierId { get; set; }

        public int? OriginId { get; set; }

        public List<CreateProductVariantDto> Variants { get; set; }
            = [];
    }
}