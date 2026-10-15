using AutoMapper;
using ErenMotorWeb.BLL.Abstract;
using ErenMotorWeb.DAL.Abstract;
using ErenMotorWeb.Dto.OriginDto;
using ErenMotorWeb.Entities;

namespace ErenMotorWeb.BLL.Concrete
{
    public class OriginManager(IOriginDAL originDAL, IMapper mapper) : GenericManager<Origin, CreateOriginDto, UpdateOriginDto, ResultOriginDto>(originDAL, mapper), IOriginService
    {
    
    }
}
