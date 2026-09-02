using ErenMotorWeb.Dto.Enums;

namespace ErenMotorWeb.Dto.StockMovementDto
{
    public class CreateStockMovementDto
    {
        public int ProductId { get; set; }

        public StockChangeSourceDto Source { get; set; }

        public string? Note { get; set; }

        public int? RelatedProcessedOrderId { get; set; }
    }
}