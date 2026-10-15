using ErenMotorWeb.UI.Controllers;
using ErenMotorWeb.UI.Core.Dtos.ProductImageDto;
using ErenMotorWeb.UI.Core.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace ErenMotorWeb.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/{controller}")]
    public class ProductImageController(
        IProductImageService productImageService) : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("AddProductImage")]
        public IActionResult AddProductImage(int productId)
        {
            var model = new CreateProductImageDto
            {
                ProductId = productId
            };

            return View(model);
        }

        [HttpPost("AddProductImage")]
        public async Task<IActionResult> AddProductImage(
            CreateProductImageDto createProductImage,
            List<IFormFile> images)
        {
            if (images == null || images.Count == 0)
            {
                ModelState.AddModelError(
                    "images",
                    "Lütfen en az bir görsel seçin.");

                return View(createProductImage);
            }

            var productFolder = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot",
                "uploads",
                "products",
                createProductImage.ProductId.ToString());

            if (!Directory.Exists(productFolder))
                Directory.CreateDirectory(productFolder);

            for (int i = 0; i < images.Count; i++)
            {
                var image = images[i];

                if (image.Length == 0)
                    continue;

                var extension = Path.GetExtension(image.FileName);

                var fileName = $"{Guid.NewGuid()}{extension}";

                var filePath = Path.Combine(
                    productFolder,
                    fileName);

                await using (var stream = new FileStream(
                    filePath,
                    FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }

                var imageDto = new CreateProductImageDto
                {
                    ProductId = createProductImage.ProductId,

                    ImageUrl =
                        $"/uploads/products/{createProductImage.ProductId}/{fileName}",

                    AltText = createProductImage.AltText,

                    SortOrder = i,

                    IsMain = i == 0
                };

                var response =
                    await productImageService.CreateAsync(imageDto);

                if (!response.Success)
                {
                    return HandleErrorResponse(
                        response,
                        "Products",
                        createProductImage);
                }
            }

            return RedirectToAction(
                "Products",
                "Product",
                new { area = "Admin" });
        }
    }
}