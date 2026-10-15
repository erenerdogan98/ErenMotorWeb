using ErenMotorWeb.UI.Core.Dtos.GetResponseDto;
using ErenMotorWeb.UI.Core.Helper;
using ErenMotorWeb.UI.Core.Services.Abstract;
using Serilog;
using System.Text.Json;

namespace ErenMotorWeb.UI.Core.Services.Concrete
{
    public class GenericService<TCreateDto, TUpdateDto, TResultDto, TKey>(HttpClientHelper httpClientHelper, string baseEndpoint) : IGenericService<TCreateDto, TUpdateDto, TResultDto, TKey>
    {
        private readonly JsonSerializerOptions _options = new() { PropertyNameCaseInsensitive = true, PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
        private HttpClient CreateClient() => httpClientHelper.CreateClient();

        #region old
        public async Task<GetApiResponseDto<string>> CreateAsync(TCreateDto dto)
        {
            try
            {
                var client = CreateClient();
                var response = await client.PostAsJsonAsync(baseEndpoint, dto);
                return await HandleResponse<string>(response);
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return new GetApiResponseDto<string>(string.Empty, false, $"Error occurred while creating ", 500);
            }
        }

        public async Task<GetApiResponseDto<string>> DeleteAsync(TKey id)
        {
            try
            {
                var client = CreateClient();
                var response = await client.DeleteAsync($"{baseEndpoint}/{id}");
                return await HandleResponse<string>(response);
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return new GetApiResponseDto<string>(null, false, $"Error occurred while deleting with ID : {id}", 500);
            }
        }

        public async Task<GetApiResponseDto<IEnumerable<TResultDto>>> GetAllAsync()
        {
            try
            {
                var client = CreateClient();
                var response = await client.GetAsync(baseEndpoint);
                return await HandleResponse<IEnumerable<TResultDto>>(response);
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return new GetApiResponseDto<IEnumerable<TResultDto>>(null, false, $"An error occurred while fetching the data", 500);
            }
        }

        public async Task<GetApiResponseDto<TResultDto>> GetByIdAsync(TKey id)
        {
            try
            {
                var client = CreateClient();
                var response = await client.GetAsync($"{baseEndpoint}/{id}");
                return await HandleResponse<TResultDto>(response);
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return new GetApiResponseDto<TResultDto>(default, false, $"An error occurred while fetching the data by Id : {id} ", 500);
            }
        }

        public async Task<GetApiResponseDto<string>> UpdateAsync(TUpdateDto dto)
        {
            try
            {
                var client = CreateClient();
                var response = await client.PutAsJsonAsync(baseEndpoint, dto);
                return await HandleResponse<string>(response);
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return new GetApiResponseDto<string>(null, false, $"Error occurred while updating ", 500);
            }
        }

        public async Task<GetApiResponseDto<T>> HandleResponse<T>(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<GetApiResponseDto<T>>(_options);
                return result with { StatusCode = (int)response.StatusCode };
            }
            else
            {
                string errorMessage = await response.Content.ReadAsStringAsync();
                Log.Error("API Response Error: {StatusCode}, Message: {ErrorMessage}", response.StatusCode, errorMessage);

                if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    var errorResponse = JsonSerializer.Deserialize<GetApiResponseDto<Dictionary<string, List<string>>>>(errorMessage, _options);
                    return new GetApiResponseDto<T>(default, false, errorResponse?.Message, (int)response.StatusCode, errorResponse?.Errors);
                }

                return new GetApiResponseDto<T>(default, false, response.ReasonPhrase, (int)response.StatusCode);
            }
        }

        #endregion
    }
}
