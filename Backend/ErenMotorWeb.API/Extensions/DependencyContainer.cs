using ErenMotorWeb.BLL.Abstract;
using ErenMotorWeb.BLL.Concrete;
using ErenMotorWeb.BLL.Helper;
using ErenMotorWeb.DAL.Abstract;
using ErenMotorWeb.DAL.Concrete;
using ErenMotorWeb.DAL.Identity;
using ErenMotorWeb.DAL.Repositories.EFRepositories;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

using Serilog;
using Serilog.Events;

using Swashbuckle.AspNetCore.Filters;

using System.Text;

namespace ErenMotorWeb.API.Extensions
{
    public static class DependencyContainer
    {
        public static void ConfigureMyServices(
            this IServiceCollection services,
            IConfiguration configuration,
            IHostBuilder hostBuilder)
        {
            // ============================================
            // DATABASE
            // ============================================

            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection")
                )
            );

            services.AddHttpClient();

            // ============================================
            // IDENTITY
            // ============================================

            services.AddIdentity<AppUser, AppRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();


            // ============================================
            // SERILOG
            // ============================================

            #region Serilog configurations

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.File(
                    "Logs/log.txt",
                    rollingInterval: RollingInterval.Hour
                )
                .CreateLogger();

            hostBuilder.UseSerilog(
                (hostingContext, loggerConfiguration) =>
                    loggerConfiguration
                        .ReadFrom.Configuration(hostingContext.Configuration)
                        .WriteTo.Console()
            );

            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddSerilog(dispose: true);
            });

            #endregion


            // ============================================
            // AUTOMAPPER
            // ============================================

            //services.AddAutoMapper(typeof(MapProfile).Assembly);
            // AutoMapper
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<MapProfile>();
            });


            // ============================================
            // FLUENT VALIDATION
            // ============================================

            // services.AddValidatorsFromAssemblyContaining<CreateAboutValidator>();
            // services.AddFluentValidationAutoValidation();
            // services.AddFluentValidationClientsideAdapters();


            // ============================================
            // DAL
            // ============================================

            #region DAL

            services.AddScoped<IBrandDAL, EFBrandRepository>();
            services.AddScoped<ICategoryDAL, EFCategoryRepository>();
            services.AddScoped<IProductDAL, EFProductRepository>();
            services.AddScoped<IProductImageDAL, EFProductImageRepository>();
            services.AddScoped<IAuthDAL, EFAuthRepository>();
            services.AddScoped<IPlatformConnectionDAL, EFPlatformConnectionRepository>();
            services.AddScoped<IProcessedOrderDAL, EFProcessedOrderRepository>();
            services.AddScoped<IProductImageDAL, EFProductImageRepository>();
            services.AddScoped<IProductMappingDAL, EFProductMappingRepository>();
            services.AddScoped<IStockMovementDAL, EFStockMovementRepository>();
            services.AddScoped<ISyncLogDAL, EFSyncLogRepository>();
            services.AddScoped<ISupplierDAL, EFSupplierRepository>();
            services.AddScoped<IOriginDAL, EFOriginRepository>();
            #endregion


            // ============================================
            // BLL
            // ============================================

            #region BLL

            services.AddScoped<IBrandService, BrandManager>();
            services.AddScoped<ICategoryService, CategoryManager>();
            services.AddScoped<IProductService, ProductManager>();
            services.AddScoped<IProductImageService, ProductImageManager>();
            services.AddScoped<IPlatformConnectionService, PlatformConnectionManager>();
            services.AddScoped<IProcessedOrderService, ProcessedOrderManager>();
            services.AddScoped<IProductImageService, ProductImageManager>();
            services.AddScoped<IProductMappingService, ProductMappingManager>();
            services.AddScoped<IStockMovementService, StockMovementManager>();
            services.AddScoped<ISyncLogService, SyncLogManager>();
            services.AddScoped<ITrendyolProductService, TrendyolProductManager>();
            services.AddScoped<ISupplierService, SupplierManager>();
            services.AddScoped<IOriginService, OriginManager>();
            services.AddScoped<PlatformCredentialProtector>();
            #endregion


            // ============================================
            // JWT AUTHENTICATION
            // ============================================

            #region Authentication with JWT

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme =
                    JwtBearerDefaults.AuthenticationScheme;

                options.DefaultChallengeScheme =
                    JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters =
                    new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateIssuerSigningKey = true,
                        ValidateLifetime = true,

                        ValidIssuer = configuration["Jwt:Issuer"],

                        ValidAudience = configuration["Jwt:Audience"],

                        IssuerSigningKey =
                            new SymmetricSecurityKey(
                                Encoding.UTF8.GetBytes(
                                    configuration["Jwt:Key"]!
                                )
                            )
                    };
            });


            // ============================================
            // AUTHORIZATION
            // ============================================

            services.AddAuthorizationBuilder()

                .SetDefaultPolicy(
                    new AuthorizationPolicyBuilder()
                        .RequireAuthenticatedUser()
                        .Build()
                )

                .AddPolicy(
                    "Admin",
                    policy =>
                        policy
                            .RequireRole("Admin")
                            .RequireAuthenticatedUser()
                )

                .AddPolicy(
                    "Member",
                    policy =>
                        policy
                            .RequireRole("Member")
                            .RequireAuthenticatedUser()
                );


            // ============================================
            // SWAGGER + JWT
            // ============================================

            services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition(
                    "oauth2",
                    new OpenApiSecurityScheme
                    {
                        In = ParameterLocation.Header,
                        Name = "Authorization",
                        Type = SecuritySchemeType.ApiKey
                    }
                );

                options.OperationFilter<SecurityRequirementsOperationFilter>();
            });

            #endregion
        }
    }
}