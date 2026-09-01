namespace ErenMotorWeb.UI.Core.Dtos.CategoryDto
{
    public class UpdateCategoryDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Slug { get; set; } = string.Empty;

        public string? Description { get; set; }

        public int? ParentCategoryId { get; set; }

        public bool IsActive { get; set; }
    }
}
