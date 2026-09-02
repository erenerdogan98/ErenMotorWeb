using ErenMotorWeb.Dto.Enums;

namespace ErenMotorWeb.Dto.PlatformConnectionDto
{
    public class CreatePlatformConnectionDto
    {
        public PlatformTypeDto PlatformType { get; set; }

        public string Name { get; set; } = string.Empty;

        public string ApiKey { get; set; } = string.Empty;

        public string ApiSecret { get; set; } = string.Empty;

        public string? SupplierId { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
