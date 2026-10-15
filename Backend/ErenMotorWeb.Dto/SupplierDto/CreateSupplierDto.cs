namespace ErenMotorWeb.Dto.SupplierDto
{
    public class CreateSupplierDto
    {
        public string CompanyName { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public string EmailOrKep { get; set; } = string.Empty;
    }
}
