namespace ErenMotorWeb.UI.Core.Dtos.BrandDto
{
    public class CreateBrandDto
    {
        public string Name { get; set; } = string.Empty;

        public string Slug { get; set; } = string.Empty;

        public string? Description { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
