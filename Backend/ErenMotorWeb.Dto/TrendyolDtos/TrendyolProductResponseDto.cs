namespace ErenMotorWeb.Dto.TrendyolDtos
{
    public class TrendyolProductResponseDto
    {
        public int TotalElements { get; set; }

        public int TotalPages { get; set; }

        public int Page { get; set; }

        public int Size { get; set; }

        public string? NextPageToken { get; set; }

        public List<TrendyolProductDto> Content { get; set; } = new();
    }
}