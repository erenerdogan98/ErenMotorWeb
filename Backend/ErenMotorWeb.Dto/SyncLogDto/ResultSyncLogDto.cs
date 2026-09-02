using ErenMotorWeb.Dto.Enums;

namespace ErenMotorWeb.Dto.SyncLogDto
{
    public class ResultSyncLogDto
    {
        public int Id { get; set; }

        public int? ProductId { get; set; }

        public string? ProductName { get; set; }

        public int? PlatformConnectionId { get; set; }

        public string? PlatformName { get; set; }

        public SyncTypeDto SyncType { get; set; }

        public SyncStatusDto Status { get; set; }

        public int? StockBefore { get; set; }

        public int? StockAfter { get; set; }

        public string? ErrorMessage { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}