namespace ErenMotorWeb.Dto.ProductDto
{
    public class ProductImportResultDto
    {
        public int TotalCount { get; set; }

        public int AddedCount { get; set; }

        public int ExistingCount { get; set; }

        public int ErrorCount { get; set; }

        public List<string> Errors { get; set; } = new();
    }
}