using ErenMotorWeb.UI.Core.Dtos.BrandDto;

namespace ErenMotorWeb.UI.Core.Services.Abstract
{
    public interface IBrandService : IGenericService<CreateBrandDto, UpdateBrandDto, ResultBrandDto, int>
    {
    } 
}
