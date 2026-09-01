using AutoMapper;
using ErenMotorWeb.BLL.Abstract;
using ErenMotorWeb.DAL.Abstract;
using ErenMotorWeb.Dto.ProductImageDto;
using ErenMotorWeb.Entities;

namespace ErenMotorWeb.BLL.Concrete
{
    public class ProductImageManager(IProductImageDAL ProductImageDAL, IMapper mapper) : GenericManager<ProductImage, CreateProductImageDto, UpdateProductImageDto, ResultProductImageDto>(ProductImageDAL, mapper), IProductImageService
    {
    }
}
