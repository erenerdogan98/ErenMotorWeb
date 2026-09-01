namespace ErenMotorWeb.UI.Core.Dtos.ProductImageDto
{
    public class ResultProductImageDto
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public string ImageUrl { get; set; } = string.Empty;

        public string? AltText { get; set; }

        public int SortOrder { get; set; }

        public bool IsMain { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
