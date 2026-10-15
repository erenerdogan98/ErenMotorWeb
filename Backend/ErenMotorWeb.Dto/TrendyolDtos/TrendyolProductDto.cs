namespace ErenMotorWeb.Dto.TrendyolDtos
{
    public class TrendyolProductDto
    {
        public long ContentId { get; set; }

        public string? ProductMainId { get; set; }
        public string Barcode { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public TrendyolBrandDto? Brand { get; set; }

        public TrendyolCategoryDto? Category { get; set; }

        public List<TrendyolImageDto> Images { get; set; } = [];

        public List<TrendyolAttributeDto> Attributes { get; set; } = [];

        public List<TrendyolVariantDto> Variants { get; set; } = [];
    }
}
