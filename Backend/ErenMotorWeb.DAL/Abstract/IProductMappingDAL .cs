using ErenMotorWeb.Entities;

namespace ErenMotorWeb.DAL.Abstract
{
    public interface IProductMappingDAL : IGenericDAL<ProductMapping>
    {
        // Rebalance mantığı: bir üründeki tüm kanalları tek seferde tarar
        Task<IEnumerable<ProductMapping>> GetByProductIdAsync(int productId);

        // Tracked entity döner - OrderProcessingService üzerinde değişiklik
        // yapıp UpdateAsync çağıracağı için AsNoTracking KULLANILMIYOR
        Task<ProductMapping?> GetByProductAndPlatformAsync(int productId, int platformConnectionId);

        Task<IEnumerable<ProductMapping>> GetAllWithDetailsAsync();
    }
}
