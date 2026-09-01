using AutoMapper;
using ErenMotorWeb.Dto.BrandDto;
using ErenMotorWeb.Dto.CategoryDto;
using ErenMotorWeb.Dto.ProductDto;
using ErenMotorWeb.Dto.ProductImageDto;
using ErenMotorWeb.Entities;

namespace ErenMotorWeb.BLL.Helper
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<Brand, CreateBrandDto>().ReverseMap();
            CreateMap<Brand, UpdateBrandDto>().ReverseMap();
            CreateMap<Brand, ResultBrandDto>().ReverseMap();

            CreateMap<Category, CreateCategoryDto>().ReverseMap();
            CreateMap<Category, UpdateCategoryDto>().ReverseMap();
            CreateMap<Category, ResultCategoryDto>()
        .ForMember(
            dest => dest.ParentCategoryName,
            opt => opt.MapFrom(src => src.ParentCategory != null
                ? src.ParentCategory.Name
                : null));

            CreateMap<Product, CreateProductDto>().ReverseMap();
            CreateMap<Product, UpdateProductDto>().ReverseMap();
            CreateMap<Product, ResultProductDto>()
                .ForMember(
                    dest => dest.CategoryName,
                    opt => opt.MapFrom(src => src.Category != null ? src.Category.Name : null))
                .ForMember(
                    dest => dest.BrandName,
                    opt => opt.MapFrom(src => src.Brand != null ? src.Brand.Name : null));

            CreateMap<ProductImage, CreateProductImageDto>().ReverseMap();
            CreateMap<ProductImage, UpdateProductImageDto>().ReverseMap();
            CreateMap<ProductImage, ResultProductImageDto>().ReverseMap();
        }
    }
}
