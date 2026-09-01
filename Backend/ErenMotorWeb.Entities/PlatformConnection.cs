using ErenMotorWeb.Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace ErenMotorWeb.Entities
{
    public class PlatformConnection : IEntityBase
    {
        [Key]
        public int Id { get; set; }

        public  PlatformType PlatformType { get; set; }

        public string Name { get; set; } = string.Empty;

        // ÖNEMLİ: düz metin saklama - IDataProtector ile şifrelenmiş halde tut
        public string ApiKeyEncrypted { get; set; } = string.Empty;

        public string ApiSecretEncrypted { get; set; } = string.Empty;

        public string? SupplierId { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime? LastOrderFetchAt { get; set; }

        public DateTime? LastSyncAt { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        // Navigation Properties

        public ICollection<ProductMapping>? ProductMappings { get; set; }
    }
}
