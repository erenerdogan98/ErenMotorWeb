namespace ErenMotorWeb.Dto.ProcessedOrderDto
{
    public class ResultProcessedOrderDto
    {
        public int Id { get; set; }

        public int PlatformConnectionId { get; set; }

        public string? PlatformName { get; set; }

        public string PlatformOrderId { get; set; } = string.Empty;

        public string? PlatformOrderLineId { get; set; }

        public int ProductId { get; set; }

        public string? ProductName { get; set; }

        public int Quantity { get; set; }

        public string OrderStatus { get; set; } = string.Empty;

        public bool StockDeducted { get; set; }

        public bool StockRestored { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}