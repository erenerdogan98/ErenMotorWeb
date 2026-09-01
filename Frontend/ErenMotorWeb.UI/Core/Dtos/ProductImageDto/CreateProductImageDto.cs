namespace ErenMotorWeb.UI.Core.Dtos.ProductImageDto
{
    public class CreateProductImageDto
    {
        public int ProductId { get; set; }

        public string ImageUrl { get; set; } = string.Empty;

        public string? AltText { get; set; }

        public int SortOrder { get; set; }

        public bool IsMain { get; set; }
    }
}
