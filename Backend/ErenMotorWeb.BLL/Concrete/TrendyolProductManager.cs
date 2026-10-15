using ErenMotorWeb.BLL.Abstract;
using ErenMotorWeb.BLL.Helper;
using ErenMotorWeb.DAL.Abstract;
using ErenMotorWeb.Dto.ServiceResponseDto;
using ErenMotorWeb.Dto.TrendyolDto;
using ErenMotorWeb.Dto.TrendyolDtos;
using ErenMotorWeb.Entities.Enums;
using Serilog;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace ErenMotorWeb.BLL.Concrete
{
    public class TrendyolProductManager(
        HttpClient httpClient,
        IPlatformConnectionDAL platformConnectionDAL,
        PlatformCredentialProtector credentialProtector, IProductDAL productDAL)
        : ITrendyolProductService
    {
        private const string TrendyolBaseUrl =
            "https://apigw.trendyol.com/integration/product/sellers";

        private const int PageSize = 100;

        public async Task<ApiResponseDto<List<TrendyolProductDto>>>
            GetOnSaleProductsAsync()
        {
            try
            {
                // ---------------------------------------------------------
                // 1. Aktif Trendyol bağlantısını getir
                // ---------------------------------------------------------

                var connection =
                    await platformConnectionDAL
                        .GetByPlatformTypeAsync(PlatformType.Trendyol);

                if (connection == null)
                {
                    return new ApiResponseDto<List<TrendyolProductDto>>(
                        null,
                        false,
                        404,
                        "Active Trendyol platform connection not found.");
                }

                // ---------------------------------------------------------
                // 2. SupplierId kontrolü
                // ---------------------------------------------------------

                if (string.IsNullOrWhiteSpace(connection.SupplierId))
                {
                    return new ApiResponseDto<List<TrendyolProductDto>>(
                        null,
                        false,
                        400,
                        "Trendyol SupplierId is not defined.");
                }

                // ---------------------------------------------------------
                // 3. API Key ve API Secret'i çöz
                // ---------------------------------------------------------

                string apiKey;
                string apiSecret;

                try
                {
                    apiKey = credentialProtector.Unprotect(
                        connection.ApiKeyEncrypted);

                    apiSecret = credentialProtector.Unprotect(
                        connection.ApiSecretEncrypted);
                }
                catch (Exception ex)
                {
                    Log.Error(
                        ex,
                        "Trendyol API credentials could not be decrypted.");

                    return new ApiResponseDto<List<TrendyolProductDto>>(
                        null,
                        false,
                        500,
                        "Trendyol API credentials could not be decrypted.");
                }

                if (string.IsNullOrWhiteSpace(apiKey) ||
                    string.IsNullOrWhiteSpace(apiSecret))
                {
                    return new ApiResponseDto<List<TrendyolProductDto>>(
                        null,
                        false,
                        400,
                        "Trendyol API credentials are empty.");
                }

                // ---------------------------------------------------------
                // 4. Basic Authentication
                // ---------------------------------------------------------

                var credentials = $"{apiKey}:{apiSecret}";

                var encodedCredentials =
                    Convert.ToBase64String(
                        Encoding.UTF8.GetBytes(credentials));

                // ---------------------------------------------------------
                // 5. Trendyol ürün endpoint'i
                // ---------------------------------------------------------

                var url =
                    $"{TrendyolBaseUrl}/" +
                    $"{connection.SupplierId}/products/approved" +
                    $"?size={PageSize}";

                Log.Information(
                    "Fetching Trendyol products.");

                // ---------------------------------------------------------
                // 6. HTTP Request oluştur
                // ---------------------------------------------------------

                using var request = new HttpRequestMessage(
                    HttpMethod.Get,
                    url);

                request.Headers.Authorization =
                    new AuthenticationHeaderValue(
                        "Basic",
                        encodedCredentials);

                request.Headers.UserAgent.ParseAdd(
                    $"{connection.SupplierId} - SelfIntegration");

                // ---------------------------------------------------------
                // 7. Trendyol API'ye istek gönder
                // ---------------------------------------------------------

                using var response =
                    await httpClient.SendAsync(request);

                var responseContent =
                    await response.Content.ReadAsStringAsync();

                // ---------------------------------------------------------
                // 8. HTTP hata kontrolü
                // ---------------------------------------------------------

                if (!response.IsSuccessStatusCode)
                {
                    Log.Error(
                        "Trendyol API request failed. " +
                        "StatusCode: {StatusCode}, Response: {Response}",
                        (int)response.StatusCode,
                        responseContent);

                    return new ApiResponseDto<List<TrendyolProductDto>>(
                        null,
                        false,
                        (int)response.StatusCode,
                        $"Trendyol API request failed: {responseContent}");
                }

                // ---------------------------------------------------------
                // 9. JSON → DTO
                // ---------------------------------------------------------

                var trendyolResponse =
                    JsonSerializer.Deserialize<TrendyolProductResponseDto>(
                        responseContent,
                        new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });

                if (trendyolResponse == null)
                {
                    Log.Error(
                        "Trendyol API response could not be deserialized.");

                    return new ApiResponseDto<List<TrendyolProductDto>>(
                        null,
                        false,
                        500,
                        "Trendyol API response could not be deserialized.");
                }

                // ---------------------------------------------------------
                // 10. Gelen ürünleri listeye ekle
                // ---------------------------------------------------------

                var products = new List<TrendyolProductDto>();

                if (trendyolResponse.Content != null &&
                    trendyolResponse.Content.Count > 0)
                {
                    products.AddRange(
                        trendyolResponse.Content);
                }

                // ---------------------------------------------------------
                // 11. Log
                // ---------------------------------------------------------

                Log.Information(
                    "Trendyol products fetched successfully. " +
                    "Product count: {ProductCount}",
                    products.Count);

                // ---------------------------------------------------------
                // 12. Sonucu döndür
                // ---------------------------------------------------------

                return new ApiResponseDto<List<TrendyolProductDto>>(
                    products,
                    true,
                    200,
                    $"Successfully fetched {products.Count} Trendyol products.");
            }
            catch (HttpRequestException ex)
            {
                Log.Error(
                    ex,
                    "HTTP error occurred while fetching Trendyol products.");

                return new ApiResponseDto<List<TrendyolProductDto>>(
                    null,
                    false,
                    503,
                    "Could not connect to Trendyol API.");
            }
            catch (TaskCanceledException ex)
            {
                Log.Error(
                    ex,
                    "Trendyol API request timed out.");

                return new ApiResponseDto<List<TrendyolProductDto>>(
                    null,
                    false,
                    504,
                    "Trendyol API request timed out.");
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "An unexpected error occurred while fetching Trendyol products.");

                return new ApiResponseDto<List<TrendyolProductDto>>(
                    null,
                    false,
                    500,
                    ex.Message);
            }
        }

        public async Task<ApiResponseDto<List<TrendyolProductMatchDto>>>
    CheckProductMatchesAsync()
        {
            try
            {
                var trendyolResponse = await GetOnSaleProductsAsync();

                if (!trendyolResponse.Success ||
                    trendyolResponse.Data == null)
                {
                    return new ApiResponseDto<List<TrendyolProductMatchDto>>(
                        null,
                        false,
                        trendyolResponse.StatusCode,
                        trendyolResponse.Message);
                }

                var localProducts =
                    await productDAL.GetAllAsync();

                var trendyolProducts =
                    trendyolResponse.Data;

                var trendyolByBarcode = trendyolProducts
                    .Where(x => !string.IsNullOrWhiteSpace(x.Barcode))
                    .GroupBy(x => x.Barcode.Trim())
                    .ToDictionary(
                        x => x.Key,
                        x => x.First(),
                        StringComparer.OrdinalIgnoreCase);

                var results = localProducts
                    .Select(localProduct =>
                    {
                        var barcode =
                            localProduct.Barcode?.Trim();

                        TrendyolProductDto? trendyolProduct = null;

                        if (!string.IsNullOrWhiteSpace(barcode))
                        {
                            trendyolByBarcode.TryGetValue(
                                barcode,
                                out trendyolProduct);
                        }

                        return new TrendyolProductMatchDto
                        {
                            ProductId = localProduct.Id,
                            ProductName = localProduct.ProductName,
                            Barcode = localProduct.Barcode ?? string.Empty,

                            IsMatched = trendyolProduct != null,

                            TrendyolProductName = trendyolProduct?.Title,
                            TrendyolBarcode = trendyolProduct?.Barcode
                        };
                    })
                    .ToList();

                return new ApiResponseDto<List<TrendyolProductMatchDto>>(
                    results,
                    true,
                    200,
                    $"{results.Count(x => x.IsMatched)} / {results.Count} product matched with Trendyol.");
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "An error occurred while checking Trendyol product matches.");

                return new ApiResponseDto<List<TrendyolProductMatchDto>>(
                    null,
                    false,
                    500,
                    ex.Message);
            }
        }
    }
}

