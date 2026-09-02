
using ErenMotorWeb.Dto.Enums;

namespace ErenMotorWeb.Dto.StockMovementDto
{
    public class ResultStockMovementDto
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public string? ProductName { get; set; }

        public int PreviousStock { get; set; }

        public int NewStock { get; set; }

        public int Change { get; set; }

        public StockChangeSourceDto Source { get; set; }

        public string? Note { get; set; }

        public int? RelatedProcessedOrderId { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}