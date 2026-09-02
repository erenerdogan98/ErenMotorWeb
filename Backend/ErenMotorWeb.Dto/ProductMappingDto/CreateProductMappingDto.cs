namespace ErenMotorWeb.Dto.ProductMappingDto
{
    public class CreateProductMappingDto
    {
        public int ProductId { get; set; }

        public int PlatformConnectionId { get; set; }

        public string PlatformProductId { get; set; } = string.Empty;

        public string? PlatformBarcode { get; set; }

        public int AllocatedStock { get; set; }

        public int TargetStock { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
