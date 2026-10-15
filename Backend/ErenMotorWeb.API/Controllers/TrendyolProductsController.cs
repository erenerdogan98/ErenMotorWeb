using ErenMotorWeb.BLL.Abstract;
using ErenMotorWeb.Dto.TrendyolDto;
using Microsoft.AspNetCore.Mvc;

namespace ErenMotorWeb.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrendyolProductsController(ITrendyolProductService trendyolProductService) : BaseController
    {
        [HttpGet("on-sale")]
        public async Task<IActionResult> GetOnSaleProducts()
        {
            var response =
                await trendyolProductService.GetOnSaleProductsAsync();

            return StatusCode(
                response.StatusCode,
                response);
        }

        [HttpGet("match-check")]
        public async Task<IActionResult> CheckMatches(
    [FromServices] IProductService productService)
        {
            var localResponse =
                await productService.TGetAllAsync();

            if (!localResponse.Success ||
                localResponse.Data == null)
            {
                return CreateApiResponse(localResponse);
            }

            var trendyolResponse =
                await trendyolProductService.GetOnSaleProductsAsync();

            if (!trendyolResponse.Success ||
                trendyolResponse.Data == null)
            {
                return CreateApiResponse(trendyolResponse);
            }

            var trendyolProducts =
                trendyolResponse.Data;

            var results = localResponse.Data
    .Select(localProduct =>
    {
        var trendyolProduct = trendyolProducts
            .FirstOrDefault(x =>
                x.Variants != null &&
                x.Variants.Any(v =>
                    !string.IsNullOrWhiteSpace(v.Barcode) &&
                    !string.IsNullOrWhiteSpace(localProduct.Barcode) &&
                    string.Equals(
                        v.Barcode.Trim(),
                        localProduct.Barcode.Trim(),
                        StringComparison.OrdinalIgnoreCase)));

        var trendyolVariant = trendyolProduct?
            .Variants?
            .FirstOrDefault(v =>
                !string.IsNullOrWhiteSpace(v.Barcode) &&
                !string.IsNullOrWhiteSpace(localProduct.Barcode) &&
                string.Equals(
                    v.Barcode.Trim(),
                    localProduct.Barcode.Trim(),
                    StringComparison.OrdinalIgnoreCase));

        return new TrendyolProductMatchDto
        {
            ProductId = localProduct.Id,
            ProductName = localProduct.ProductName,
            Barcode = localProduct.Barcode ?? string.Empty,

            IsMatched = trendyolProduct != null,

            TrendyolProductName =
                trendyolProduct?.Title,

            TrendyolBarcode =
                trendyolVariant?.Barcode
        };
    })
    .ToList();

            return Ok(new
            {
                TotalLocalProducts = results.Count,

                MatchedProducts =
                    results.Count(x => x.IsMatched),

                UnmatchedProducts =
                    results.Count(x => !x.IsMatched),

                Products = results
            });
        }
    }
}
