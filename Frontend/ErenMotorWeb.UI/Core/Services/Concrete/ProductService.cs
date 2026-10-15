using ErenMotorWeb.UI.Core.Constants;
using ErenMotorWeb.UI.Core.Dtos.GetResponseDto;
using ErenMotorWeb.UI.Core.Dtos.ProductDto;
using ErenMotorWeb.UI.Core.Helper;
using ErenMotorWeb.UI.Core.Services.Abstract;
using Serilog;
using System.Text.Json;

namespace ErenMotorWeb.UI.Core.Services.Concrete
{
    public class ProductService(HttpClientHelper httpClient) : GenericService<CreateProductDto, UpdateProductDto, ResultProductDto, int>(httpClient,ApiEndpoints.Products.Base), IProductService
    {
        private readonly HttpClientHelper _httpClient = httpClient;

        private readonly JsonSerializerOptions _options = new()
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        public async Task<GetApiResponseDto<IEnumerable<ResultProductDto>>> SearchAsync(
            string search)
        {
            try
            {
                var client = _httpClient.CreateClient();

                var response = await client.GetAsync(
             ApiEndpoints.Products.Search(search));

                return await HandleResponse<IEnumerable<ResultProductDto>>(response);
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);

                return new GetApiResponseDto<IEnumerable<ResultProductDto>>(
                    null,
                    false,
                    "Ürün araması sırasında bir hata oluştu.",
                    500);
            }
        }

        public async Task<
    GetApiResponseDto<PagedResultDto<ResultProductDto>>>GetPagedAsync(int page, int pageSize)
        {
            try
            {
                var client = _httpClient.CreateClient();

                var response = await client.GetAsync(
                    ApiEndpoints.Products.Paged(page, pageSize));

                return await HandleResponse<
                    PagedResultDto<ResultProductDto>>(response);
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Sayfalı ürün listesi alınırken hata oluştu.");

                return new GetApiResponseDto<
                    PagedResultDto<ResultProductDto>>(
                        null,
                        false,
                        "Ürün listesi alınırken bir hata oluştu.",
                        500);
            }
        }

        public async Task<GetApiResponseDto<string>> RemoveAsync(int id)
        {
            try
            {
                var client = _httpClient.CreateClient();

                var response = await client.PutAsync(
                    ApiEndpoints.Products.Remove(id),
                    null);

                return await HandleResponse<string>(response);
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Ürün kaldırılırken hata oluştu. ProductId: {ProductId}",
                    id);

                return new GetApiResponseDto<string>(
                    null,
                    false,
                    "Ürün kaldırılırken bir hata oluştu.",
                    500);
            }
        }
    }
}
