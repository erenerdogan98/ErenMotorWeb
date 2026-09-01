using ErenMotorWeb.Entities;
using ErenMotorWeb.Entities.Enums;

namespace ErenMotorWeb.DAL.Abstract
{
    public interface IPlatformConnectionDAL : IGenericDAL<PlatformConnection>
    {
        Task<IEnumerable<PlatformConnection>> GetActiveConnectionsAsync();

        Task<PlatformConnection?> GetByPlatformTypeAsync(PlatformType platformType);
    }
}
