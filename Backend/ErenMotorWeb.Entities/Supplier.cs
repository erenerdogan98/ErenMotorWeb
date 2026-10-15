namespace ErenMotorWeb.Entities
{
    public class Supplier : IEntityBase
    {
        public int Id { get; set; }

        public string CompanyName { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public string EmailOrKep { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        public ICollection<Product>? Products { get; set; }
    }
}
