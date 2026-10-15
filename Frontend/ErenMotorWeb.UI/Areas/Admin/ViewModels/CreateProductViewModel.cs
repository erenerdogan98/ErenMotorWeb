using ErenMotorWeb.UI.Core.Dtos.ProductDto;

namespace ErenMotorWeb.UI.Areas.Admin.ViewModels
{
    public class CreateProductViewModel
    {
        public CreateProductDto Product { get; set; } = new();

        public List<IFormFile> Images { get; set; } = new();
    }
}
