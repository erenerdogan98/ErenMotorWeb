namespace ErenMotorWeb.UI.Core.Dtos.ProductDto
{
    public class CreateProductVariantDto
    {
        public int Color { get; set; }
        public string Barcode { get; set; } = string.Empty;
        public string? Sku { get; set; }
        public decimal? Price { get; set; }
        public int Stock { get; set; }
    }
}
