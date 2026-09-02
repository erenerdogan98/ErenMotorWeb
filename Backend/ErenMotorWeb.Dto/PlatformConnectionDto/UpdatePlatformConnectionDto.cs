using ErenMotorWeb.Dto.Enums;

namespace ErenMotorWeb.Dto.PlatformConnectionDto
{
    public class UpdatePlatformConnectionDto
    {
        public int Id { get; set; }

        public PlatformTypeDto PlatformType { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? ApiKey { get; set; }

        public string? ApiSecret { get; set; }

        public string? SupplierId { get; set; }

        public bool IsActive { get; set; }
    }
}
