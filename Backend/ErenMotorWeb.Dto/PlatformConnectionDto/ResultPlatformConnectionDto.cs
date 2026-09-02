using ErenMotorWeb.Dto.Enums;

namespace ErenMotorWeb.Dto.PlatformConnectionDto
{
    public class ResultPlatformConnectionDto
    {
        public int Id { get; set; }

        public PlatformTypeDto PlatformType { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? SupplierId { get; set; }

        public bool IsActive { get; set; }

        public DateTime? LastOrderFetchAt { get; set; }

        public DateTime? LastSyncAt { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}