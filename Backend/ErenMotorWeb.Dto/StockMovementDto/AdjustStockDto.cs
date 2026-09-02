namespace ErenMotorWeb.Dto.StockMovementDto
{
    public class AdjustStockDto
    {
        public int ProductId { get; set; }

        public int NewStock { get; set; }

        public string? Note { get; set; }
    }
}