using ErenMotorWeb.UI.Core.Helper;
using ErenMotorWeb.UI.Core.Services.Abstract;
using ErenMotorWeb.UI.Core.Services.Concrete;
using ErenMotorWeb.UI.Core.Tools;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ErenMotorWeb.UI.Core.Extensions
{
    public static class DependencyContainer
    {
        public static void ConfigureMyServicesHttpClient(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ServiceApiSettings>(configuration.GetSection("ServiceApiSettings"));

            var apiValues = configuration.GetSection("ServiceApiSettings").Get<ServiceApiSettings>() ?? throw new ArgumentNullException("ServiceApiSettings");

            services.AddHttpContextAccessor();

            services.AddTransient<Helper.TokenHandler>();

            services.AddHttpClient("ApiClient", client =>
            {
                client.BaseAddress = new Uri(apiValues.BaseUrl);
            }).AddHttpMessageHandler<Helper.TokenHandler>();

            // IHttpContextAccessor
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddTransient<HttpClientHelper>();

            // session
            services.AddMemoryCache();
            services.AddSession(options =>
            {
                options.IOTimeout = TimeSpan.FromMinutes(60);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            // JWT
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                    .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            ValidIssuer = configuration["Jwt:Issuer"],
                            ValidAudience = configuration["Jwt:Audience"],
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
                        };
                    });

            // Authorization Policy
            _ = services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
            });
         

            services.AddTransient<IBrandService, BrandService>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IProductImageService, ProductImageService>();
         
        }
    }
}
