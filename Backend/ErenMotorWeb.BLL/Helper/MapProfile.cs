using AutoMapper;
using ErenMotorWeb.Dto.BrandDto;
using ErenMotorWeb.Dto.CategoryDto;
using ErenMotorWeb.Dto.Enums;
using ErenMotorWeb.Dto.PlatformConnectionDto;
using ErenMotorWeb.Dto.ProductDto;
using ErenMotorWeb.Dto.ProductImageDto;
using ErenMotorWeb.Dto.ProductMappingDto;
using ErenMotorWeb.Dto.ProcessedOrderDto;
using ErenMotorWeb.Dto.StockMovementDto;
using ErenMotorWeb.Dto.SyncLogDto;
using ErenMotorWeb.Entities;
using ErenMotorWeb.Entities.Enums;

namespace ErenMotorWeb.BLL.Helper
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {

            CreateMap<PlatformTypeDto, PlatformType>();
            CreateMap<PlatformType, PlatformTypeDto>();

            CreateMap<StockChangeSourceDto, StockChangeSource>();
            CreateMap<StockChangeSource, StockChangeSourceDto>();

            CreateMap<SyncStatusDto, SyncStatus>();
            CreateMap<SyncStatus, SyncStatusDto>();

            CreateMap<SyncTypeDto, SyncType>();
            CreateMap<SyncType, SyncTypeDto>();

            // =========================
            // BRAND
            // =========================

            CreateMap<Brand, CreateBrandDto>().ReverseMap();
            CreateMap<Brand, UpdateBrandDto>().ReverseMap();
            CreateMap<Brand, ResultBrandDto>().ReverseMap();


            // =========================
            // CATEGORY
            // =========================

            CreateMap<Category, CreateCategoryDto>().ReverseMap();

            CreateMap<Category, UpdateCategoryDto>().ReverseMap();

            CreateMap<Category, ResultCategoryDto>()
                .ForMember(
                    dest => dest.ParentCategoryName,
                    opt => opt.MapFrom(src =>
                        src.ParentCategory != null
                            ? src.ParentCategory.Name
                            : null));


            // =========================
            // PRODUCT
            // =========================

            CreateMap<Product, CreateProductDto>().ReverseMap();

            CreateMap<Product, UpdateProductDto>().ReverseMap();

            CreateMap<Product, ResultProductDto>()
                .ForMember(
                    dest => dest.CategoryName,
                    opt => opt.MapFrom(src =>
                        src.Category != null
                            ? src.Category.Name
                            : null))
                .ForMember(
                    dest => dest.BrandName,
                    opt => opt.MapFrom(src =>
                        src.Brand != null
                            ? src.Brand.Name
                            : null));


            // =========================
            // PRODUCT IMAGE
            // =========================

            CreateMap<ProductImage, CreateProductImageDto>().ReverseMap();
            CreateMap<ProductImage, UpdateProductImageDto>().ReverseMap();
            CreateMap<ProductImage, ResultProductImageDto>().ReverseMap();


            // =========================
            // PLATFORM CONNECTION
            // =========================

            CreateMap<PlatformConnection, CreatePlatformConnectionDto>()
                .ForMember(
                    dest => dest.ApiKey,
                    opt => opt.MapFrom(src => src.ApiKeyEncrypted))
                .ForMember(
                    dest => dest.ApiSecret,
                    opt => opt.MapFrom(src => src.ApiSecretEncrypted));

            CreateMap<CreatePlatformConnectionDto, PlatformConnection>()
                .ForMember(
                    dest => dest.ApiKeyEncrypted,
                    opt => opt.MapFrom(src => src.ApiKey))
                .ForMember(
                    dest => dest.ApiSecretEncrypted,
                    opt => opt.MapFrom(src => src.ApiSecret));

            CreateMap<PlatformConnection, UpdatePlatformConnectionDto>()
                .ForMember(
                    dest => dest.ApiKey,
                    opt => opt.MapFrom(src => src.ApiKeyEncrypted))
                .ForMember(
                    dest => dest.ApiSecret,
                    opt => opt.MapFrom(src => src.ApiSecretEncrypted));

            CreateMap<UpdatePlatformConnectionDto, PlatformConnection>()
                .ForMember(
                    dest => dest.ApiKeyEncrypted,
                    opt => opt.MapFrom(src => src.ApiKey))
                .ForMember(
                    dest => dest.ApiSecretEncrypted,
                    opt => opt.MapFrom(src => src.ApiSecret));

            CreateMap<PlatformConnection, ResultPlatformConnectionDto>();


            // =========================
            // PRODUCT MAPPING
            // =========================

            CreateMap<ProductMapping, CreateProductMappingDto>().ReverseMap();

            CreateMap<ProductMapping, UpdateProductMappingDto>().ReverseMap();

            CreateMap<ProductMapping, ResultProductMappingDto>()
                .ForMember(
                    dest => dest.ProductName,
                    opt => opt.MapFrom(src =>
                        src.Product != null
                            ? src.Product.ProductName
                            : null))
                .ForMember(
                    dest => dest.PlatformName,
                    opt => opt.MapFrom(src =>
                        src.PlatformConnection != null
                            ? src.PlatformConnection.Name
                            : null));


            // =========================
            // STOCK MOVEMENT
            // =========================

            CreateMap<StockMovement, ResultStockMovementDto>()
                .ForMember(
                    dest => dest.ProductName,
                    opt => opt.MapFrom(src =>
                        src.Product != null
                            ? src.Product.ProductName
                            : null));

            CreateMap<CreateStockMovementDto, StockMovement>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.Ignore())
                .ForMember(
                    dest => dest.PreviousStock,
                    opt => opt.Ignore())
                .ForMember(
                    dest => dest.NewStock,
                    opt => opt.Ignore())
                .ForMember(
                    dest => dest.Change,
                    opt => opt.Ignore())
                .ForMember(
                    dest => dest.CreatedAt,
                    opt => opt.Ignore());


            // =========================
            // PROCESSED ORDER
            // =========================

            CreateMap<ProcessedOrder, ResultProcessedOrderDto>()
                .ForMember(
                    dest => dest.ProductName,
                    opt => opt.MapFrom(src =>
                        src.Product != null
                            ? src.Product.ProductName
                            : null))
                .ForMember(
                    dest => dest.PlatformName,
                    opt => opt.MapFrom(src =>
                        src.PlatformConnection != null
                            ? src.PlatformConnection.Name
                            : null));

            CreateMap<CreateProcessedOrderDto, ProcessedOrder>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.Ignore())
                .ForMember(
                    dest => dest.StockDeducted,
                    opt => opt.Ignore())
                .ForMember(
                    dest => dest.StockRestored,
                    opt => opt.Ignore())
                .ForMember(
                    dest => dest.CreatedAt,
                    opt => opt.Ignore())
                .ForMember(
                    dest => dest.UpdatedAt,
                    opt => opt.Ignore());


            // =========================
            // SYNC LOG
            // =========================

            CreateMap<SyncLog, ResultSyncLogDto>()
                .ForMember(
                    dest => dest.ProductName,
                    opt => opt.MapFrom(src =>
                        src.Product != null
                            ? src.Product.ProductName
                            : null))
                .ForMember(
                    dest => dest.PlatformName,
                    opt => opt.MapFrom(src =>
                        src.PlatformConnection != null
                            ? src.PlatformConnection.Name
                            : null));

        }
    }
}