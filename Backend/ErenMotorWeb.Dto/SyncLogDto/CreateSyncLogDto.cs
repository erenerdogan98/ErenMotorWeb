using ErenMotorWeb.Dto.Enums;

namespace ErenMotorWeb.Dto.SyncLogDto
{
    public class CreateSyncLogDto
    {
        public int? ProductId { get; set; }

        public int? PlatformConnectionId { get; set; }

        public SyncTypeDto SyncType { get; set; }

        public SyncStatusDto Status { get; set; }

        public int? StockBefore { get; set; }

        public int? StockAfter { get; set; }

        public string? ErrorMessage { get; set; }
    }
}