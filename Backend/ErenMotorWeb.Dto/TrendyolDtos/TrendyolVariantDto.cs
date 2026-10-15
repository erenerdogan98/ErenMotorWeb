namespace ErenMotorWeb.Dto.TrendyolDtos
{
    public class TrendyolVariantDto
    {
        public long VariantId { get; set; }

        public string? Barcode { get; set; }

        public string? StockCode { get; set; }

        public bool OnSale { get; set; }

        public TrendyolStockDto? Stock { get; set; }

        public TrendyolPriceDto? Price { get; set; }

        public List<TrendyolAttributeDto> Attributes { get; set; } = [];
    }
}
