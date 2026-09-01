using AutoMapper;
using ErenMotorWeb.BLL.Abstract;
using ErenMotorWeb.DAL.Abstract;
using ErenMotorWeb.Dto.BrandDto;
using ErenMotorWeb.Entities;

namespace ErenMotorWeb.BLL.Concrete
{
    public class BrandManager(IBrandDAL BrandDAL, IMapper mapper) : GenericManager<Brand, CreateBrandDto, UpdateBrandDto, ResultBrandDto>(BrandDAL, mapper), IBrandService
    {
    }
}
