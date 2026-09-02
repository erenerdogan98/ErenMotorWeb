namespace ErenMotorWeb.Dto.ProductMappingDto
{
    public class ResultProductMappingDto
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public string? ProductName { get; set; }

        public int PlatformConnectionId { get; set; }

        public string? PlatformName { get; set; }

        public string PlatformProductId { get; set; } = string.Empty;

        public string? PlatformBarcode { get; set; }

        public int AllocatedStock { get; set; }

        public int TargetStock { get; set; }

        public int? CurrentStockOnPlatform { get; set; }

        public bool IsActive { get; set; }

        public DateTime? LastSyncedAt { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
