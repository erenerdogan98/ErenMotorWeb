using ErenMotorWeb.UI.Core.Dtos.CategoryDto;
using ErenMotorWeb.UI.Core.Dtos.GetResponseDto;
using ErenMotorWeb.UI.Core.Helper;
using ErenMotorWeb.UI.Core.Services.Abstract;

namespace ErenMotorWeb.UI.Core.Services.Concrete
{
    public class CategoryService(HttpClientHelper httpClient) : GenericService<CreateCategoryDto, UpdateCategoryDto, ResultCategoryDto, int>(httpClient), ICategoryService
    {
        private readonly HttpClientHelper _httpClientHelper = httpClient;
        private HttpClient CreateClient() => _httpClientHelper.CreateClient();
        //private HttpClient CreateClient() => httpClient.CreateClient();
        public async Task<GetApiResponseDto<IEnumerable<ResultCategoryDto>>> TGetAllWithParentAsync()
        {
            var client = CreateClient();
            var response = await client.GetAsync($"categories/with-parent");
            return await HandleResponse<IEnumerable<ResultCategoryDto>>(response);
        }

        public async Task<GetApiResponseDto<IEnumerable<ResultCategoryDto>>> TGetByIdWithParentAsync(int id)
        {
            var client = CreateClient();
            var response = await client.GetAsync($"categories/{id}/sub-category");
            return await HandleResponse<IEnumerable<ResultCategoryDto>>(response);
        }
    }
}
