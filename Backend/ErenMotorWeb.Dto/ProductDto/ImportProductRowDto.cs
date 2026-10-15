namespace ErenMotorWeb.Dto.ProductDto
{
    public class ImportProductRowDto
    {
        public int RowNumber { get; set; }

        public string ProductName { get; set; } = string.Empty;

        public string Barcode { get; set; } = string.Empty;

        public string? ModelCode { get; set; }
        public string? Sku { get; set; }
        public decimal Price { get; set; }

        public int Stock { get; set; }
    }
}