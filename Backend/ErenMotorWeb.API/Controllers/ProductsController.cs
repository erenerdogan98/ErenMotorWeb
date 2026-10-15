using ClosedXML.Excel;
using ErenMotorWeb.BLL.Abstract;
using ErenMotorWeb.Dto.ProductDto;
using Microsoft.AspNetCore.Mvc;

namespace ErenMotorWeb.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(IProductService productService) : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var response = await productService.TGetAllAsync();

            return CreateApiResponse(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var response = await productService.TGetByIdAsync(id);

            return CreateApiResponse(response);
        }
        [HttpGet("search")]
        public async Task<IActionResult> SearchProducts([FromQuery] string search)
        {
            var response = await productService.TGetAllAsync(x =>
        x.ProductName.Contains(search) ||
        x.Barcode.Contains(search));

            return CreateApiResponse(response);
        }
        [HttpPost]
        public async Task<IActionResult> CreateProduct(
            [FromBody] CreateProductDto createProductDto)
        {
            var response = await productService.TAddAsync(createProductDto);

            return CreateApiResponse(response);
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var response = await productService.TDeleteAsync(id);

            return CreateApiResponse(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct(
            [FromBody] UpdateProductDto updateProductDto)
        {
            var response = await productService.TUpdateAsync(updateProductDto);

            return CreateApiResponse(response);
        }

        [HttpPost("import-excel")]
        public async Task<IActionResult> ImportExcel(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("Excel dosyası seçilmedi.");
            }

            var products = new List<ImportProductRowDto>();

            try
            {
                using var stream = file.OpenReadStream();
                using var workbook = new XLWorkbook(stream);

                var worksheet = workbook.Worksheets.FirstOrDefault();

                if (worksheet == null)
                {
                    return BadRequest(
                        "Excel dosyasında çalışma sayfası bulunamadı.");
                }

                var rows = worksheet
                    .RowsUsed()
                    .Skip(1);

                foreach (var row in rows)
                {
                    var productName =
    row.Cell(1).GetString().Trim();

                    var barcode =
                        row.Cell(2).GetString().Trim();

                    var modelCode =
                        row.Cell(3).GetString().Trim();

                    var sku =
                        row.Cell(4).GetString().Trim();

                    var priceText =
                        row.Cell(5).GetString().Trim();

                    var stockText =
                        row.Cell(6).GetString().Trim();

                    if (string.IsNullOrWhiteSpace(productName) &&
                        string.IsNullOrWhiteSpace(barcode))
                    {
                        continue;
                    }

                    decimal price = 0;

                    if (!string.IsNullOrWhiteSpace(priceText))
                    {
                        decimal.TryParse(
                            priceText,
                            out price);
                    }

                    int stock = 0;

                    if (!string.IsNullOrWhiteSpace(stockText))
                    {
                        int.TryParse(
                            stockText,
                            out stock);
                    }

                    products.Add(new ImportProductRowDto
                    {
                        RowNumber = row.RowNumber(),

                        ProductName = productName,

                        Barcode = barcode,

                        ModelCode = modelCode,
                        Sku = sku,
                        Price = price,

                        Stock = stock
                    });
                }

                var response =
                    await productService.TImportProductsAsync(products);

                return CreateApiResponse(response);
            }
            catch (Exception ex)
            {
                return BadRequest(
                    $"Excel okunurken hata oluştu: {ex.Message}");
            }
        }

        [HttpGet("paged")]
        public async Task<IActionResult> GetPagedProducts(
    [FromQuery] int page = 1,
    [FromQuery] int pageSize = 20)
        {
            var response =
                await productService.TGetPagedAsync(page, pageSize);

            return CreateApiResponse(response);
        }

        [HttpPut("remove/{id}")]
        public async Task<IActionResult> RemoveProduct(int id)
        {
            var response = await productService.TRemoveAsync(id);

            return CreateApiResponse(response);
        }
    }
}
