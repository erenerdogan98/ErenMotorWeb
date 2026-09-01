using ErenMotorWeb.Entities;

namespace ErenMotorWeb.DAL.Abstract
{
    public interface IProcessedOrderDAL : IGenericDAL<ProcessedOrder>
    {
        // Idempotency kontrolü - OrderProcessingService bir siparişi işlemeden
        // önce burayı sorar; true dönerse aynı sipariş daha önce işlenmiş demektir
        Task<bool> ExistsAsync(int platformConnectionId, string platformOrderId, int productId);

        Task<ProcessedOrder?> GetByOrderAsync(int platformConnectionId, string platformOrderId, int productId);
    }
}
