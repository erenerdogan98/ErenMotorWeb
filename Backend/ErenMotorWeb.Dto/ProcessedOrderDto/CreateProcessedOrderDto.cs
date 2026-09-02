namespace ErenMotorWeb.Dto.ProcessedOrderDto
{
    public class CreateProcessedOrderDto
    {
        public int PlatformConnectionId { get; set; }

        public string PlatformOrderId { get; set; } = string.Empty;

        public string? PlatformOrderLineId { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public string OrderStatus { get; set; } = string.Empty;
    }
}