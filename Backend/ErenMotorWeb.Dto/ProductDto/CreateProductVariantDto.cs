using ErenMotorWeb.Dto.Enums;

namespace ErenMotorWeb.Dto.ProductDto
{
    public class CreateProductVariantDto
    {
        public ProductColors Color { get; set; }

        public string Barcode { get; set; } = string.Empty;

        public string? Sku { get; set; }

        public decimal? Price { get; set; }

        public int Stock { get; set; }
    }
}