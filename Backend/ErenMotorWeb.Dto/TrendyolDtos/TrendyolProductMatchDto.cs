namespace ErenMotorWeb.Dto.TrendyolDto
{
    public class TrendyolProductMatchDto
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; } = string.Empty;

        // Bizim veritabanımızdaki barkod
        public string Barcode { get; set; } = string.Empty;

        public bool IsMatched { get; set; }

        public string? TrendyolProductName { get; set; }

        // Trendyol'dan gelen barkod
        public string? TrendyolBarcode { get; set; }
    }
}