using AutoMapper;
using ErenMotorWeb.BLL.Abstract;
using ErenMotorWeb.DAL.Abstract;
using ErenMotorWeb.Dto.ProductDto;
using ErenMotorWeb.Dto.ServiceResponseDto;
using ErenMotorWeb.Entities;
using Serilog;

namespace ErenMotorWeb.BLL.Concrete
{
    public class ProductManager(IProductDAL productDAL, IMapper mapper)
        : GenericManager<Product, CreateProductDto, UpdateProductDto, ResultProductDto>(productDAL, mapper),
          IProductService
    {
        private static string GenerateSlug(string text)
        {
            text = text
                .ToLowerInvariant()
                .Replace("ç", "c")
                .Replace("ğ", "g")
                .Replace("ı", "i")
                .Replace("ö", "o")
                .Replace("ş", "s")
                .Replace("ü", "u");

            text = System.Text.RegularExpressions.Regex.Replace(
                text,
                @"[^a-z0-9]+",
                "-");

            return text.Trim('-');
        }
        public async Task<ApiResponseDto<ProductImportResultDto>> TImportProductsAsync(
    IEnumerable<ImportProductRowDto> products)
        {
            var result = new ProductImportResultDto();

            try
            {
                var importRows = products.ToList();

                result.TotalCount = importRows.Count;

                const int defaultCategoryId = 1;
                const int defaultBrandId = 1;
                const int defaultSupplierId = 1;
                const int defaultOriginId = 1;

                var productsToAdd = new List<Product>();

                foreach (var item in importRows)
                {
                    try
                    {
                        if (string.IsNullOrWhiteSpace(item.ProductName))
                        {
                            result.ErrorCount++;
                            result.Errors.Add(
                                $"{item.RowNumber}. satır: Ürün adı boş.");

                            continue;
                        }

                        if (string.IsNullOrWhiteSpace(item.Barcode))
                        {
                            result.ErrorCount++;
                            result.Errors.Add(
                                $"{item.RowNumber}. satır: Barkod boş.");

                            continue;
                        }

                        var barcode = item.Barcode.Trim();

                        var existingProduct = await productDAL.GetAllAsync(
                            x => x.Barcode == barcode);

                        if (existingProduct.Any())
                        {
                            result.ExistingCount++;
                            continue;
                        }

                        var duplicateInExcel = productsToAdd.Any(
                            x => x.Barcode == barcode);

                        if (duplicateInExcel)
                        {
                            result.ExistingCount++;
                            continue;
                        }

                        var product = new Product
                        {
                            ProductName = item.ProductName.Trim(),

                            Barcode = barcode,

                            ModelCode = string.IsNullOrWhiteSpace(item.ModelCode)
                                ? barcode
                                : item.ModelCode.Trim(),
                            Sku = string.IsNullOrWhiteSpace(item.Sku)? barcode: item.Sku.Trim(),
                            Price = item.Price,

                            Stock = Math.Max(0, item.Stock),
                            Slug = GenerateSlug(item.ProductName),

                            CategoryId = defaultCategoryId,

                            BrandId = defaultBrandId,

                            SupplierId = defaultSupplierId,

                            OriginId = defaultOriginId,

                            Color = Entities.Enums.ProductColors.Black,

                            IsActive = true,

                            IsFeatured = false,

                            CreatedAt = DateTime.Now,

                            UpdatedAt = DateTime.Now
                        };

                        productsToAdd.Add(product);

                        result.AddedCount++;
                    }
                    catch (Exception ex)
                    {
                        result.ErrorCount++;

                        result.Errors.Add(
                            $"{item.RowNumber}. satır: {ex.Message}");
                    }
                }

                if (productsToAdd.Count != 0)
                {
                    await productDAL.AddRangeAsync(productsToAdd);
                }

                return new ApiResponseDto<ProductImportResultDto>(
                    result,
                    true,
                    200,
                    "Ürün aktarımı tamamlandı.");
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Product Excel import sırasında hata oluştu.");

                return new ApiResponseDto<ProductImportResultDto>(
                    result,
                    false,
                    500,
                    ex.Message);
            }
        }

        public override async Task<ApiResponseDto<IEnumerable<ResultProductDto>>> TGetAllAsync()
        {
            try
            {
                var entities = await productDAL.GetAllWithDetailsAsync();
                var dtos = mapper.Map<IEnumerable<ResultProductDto>>(entities);

                return new ApiResponseDto<IEnumerable<ResultProductDto>>(
                    dtos, true, 200, "Product values fetched successfully");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occured while fetching values Product");
                return new ApiResponseDto<IEnumerable<ResultProductDto>>(null, false, 500, ex.Message);
            }
        }

        public override async Task<ApiResponseDto<ResultProductDto>> TGetByIdAsync(int id)
        {
            try
            {
                if (id <= 0)
                    return new ApiResponseDto<ResultProductDto>(default, false, 422, "Invalid Id");

                var entity = await productDAL.GetByIdWithDetailsAsync(id);
                if (entity is null)
                    return new ApiResponseDto<ResultProductDto>(default, false, 404, "Product values not found by Id");

                var dto = mapper.Map<ResultProductDto>(entity);
                return new ApiResponseDto<ResultProductDto>(dto, true, 200, "Product values fecthed successfully by Id");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occured while fetching data : Product by Id : {Id}", id);
                return new ApiResponseDto<ResultProductDto>(default, false, 500, ex.Message);
            }
        }

        public override async Task<ApiResponseDto<string>> TUpdateAsync(
    UpdateProductDto updateDto)
        {
            try
            {
                if (updateDto == null)
                {
                    return new ApiResponseDto<string>(
                        null,
                        false,
                        400,
                        "Bad Request!");
                }

                var product = await productDAL.GetByIdAsync(updateDto.Id);

                if (product == null)
                {
                    return new ApiResponseDto<string>(
                        null,
                        false,
                        404,
                        "Product not found");
                }

                mapper.Map(updateDto, product);

                await productDAL.UpdateAsync(product);

                return new ApiResponseDto<string>(
                    product.Id.ToString(),
                    true,
                    200,
                    "Product updated successfully");
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "An error occurred while updating Product. ProductId: {ProductId}",
                    updateDto?.Id);

                return new ApiResponseDto<string>(
                    null,
                    false,
                    500,
                    ex.Message);
            }
        }

        public override async Task<ApiResponseDto<string>> TAddAsync(
     CreateProductDto createDto)
        {
            try
            {
                if (createDto == null)
                {
                    return new ApiResponseDto<string>(
                        null,
                        false,
                        400,
                        "Bad Request!");
                }

                if (createDto.Variants == null || createDto.Variants.Count == 0)
                {
                    return new ApiResponseDto<string>(
                        null,
                        false,
                        422,
                        "At least one product variant is required.");
                }

                // Aynı renk bir ürün içerisinde tekrar edemez.
                var duplicateColors = createDto.Variants
                    .GroupBy(x => x.Color)
                    .Where(x => x.Count() > 1)
                    .Select(x => x.Key)
                    .ToList();

                if (duplicateColors.Count != 0)
                {
                    return new ApiResponseDto<string>(
                        null,
                        false,
                        422,
                        "The same color cannot be added more than once.");
                }

                // Aynı request içerisinde aynı barkod tekrar edemez.
                var duplicateBarcodes = createDto.Variants
                    .GroupBy(x => x.Barcode)
                    .Where(x => x.Count() > 1)
                    .Select(x => x.Key)
                    .ToList();

                if (duplicateBarcodes.Count != 0)
                {
                    return new ApiResponseDto<string>(
                        null,
                        false,
                        422,
                        "The same barcode cannot be used more than once.");
                }

                var products = new List<Product>();

                foreach (var variant in createDto.Variants)
                {
                    // Ortak ürün bilgilerini mapliyoruz.
                    var product = mapper.Map<Product>(createDto);

                    // Varyanta özel bilgileri aynı Product üzerine mapliyoruz.
                    mapper.Map(variant, product);

                    products.Add(product);
                }

                await productDAL.AddRangeAsync(products);

                Log.Information(
                    "{Count} product variant(s) added successfully. ModelCode: {ModelCode}",
                    products.Count,
                    createDto.ModelCode);

                return new ApiResponseDto<string>(
                    createDto.ModelCode,
                    true,
                    201,
                    $"{products.Count} product variant(s) added successfully");
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Error occurred while creating Product variants. ModelCode: {ModelCode}",
                    createDto?.ModelCode);

                return new ApiResponseDto<string>(
                    null,
                    false,
                    500,
                    ex.Message);
            }
        }

        public async Task<ApiResponseDto<PagedResultDto<ResultProductDto>>>TGetPagedAsync(int page, int pageSize)
        {
            try
            {
                if (page < 1)
                    page = 1;

                if (pageSize < 1)
                    pageSize = 20;

                var result =
                    await productDAL.GetPagedWithDetailsAsync(page, pageSize);

                var productDtos =
                    mapper.Map<IEnumerable<ResultProductDto>>(result.Items);

                var totalPages = (int)Math.Ceiling(
                    result.TotalCount / (double)pageSize);

                var pagedResult =
                    new PagedResultDto<ResultProductDto>
                    {
                        Items = productDtos,
                        TotalCount = result.TotalCount,
                        ActiveCount = result.ActiveCount,
                        OutOfStockCount = result.OutOfStockCount,
                        Page = page,
                        PageSize = pageSize,
                        TotalPages = totalPages
                    };

                return new ApiResponseDto<
                    PagedResultDto<ResultProductDto>>(
                        pagedResult,
                        true,
                        200,
                        "Product values fetched successfully with pagination.");
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "An error occurred while fetching paged Product values.");

                return new ApiResponseDto<
                    PagedResultDto<ResultProductDto>>(
                        null,
                        false,
                        500,
                        ex.Message);
            }
        }

        public async Task<ApiResponseDto<string>> TRemoveAsync(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return new ApiResponseDto<string>(
                        null,
                        false,
                        422,
                        "Invalid Id");
                }

                var product = await productDAL.GetByIdAsync(id);

                if (product == null)
                {
                    return new ApiResponseDto<string>(
                        null,
                        false,
                        404,
                        "Product not found");
                }

                if (product.IsDeleted)
                {
                    return new ApiResponseDto<string>(
                        null,
                        false,
                        409,
                        "Product is already removed");
                }

                product.IsDeleted = true;
                product.IsActive = false;
                product.UpdatedAt = DateTime.Now;

                await productDAL.UpdateAsync(product);

                Log.Information(
                    "Product removed successfully. ProductId: {ProductId}",
                    id);

                return new ApiResponseDto<string>(
                    product.Id.ToString(),
                    true,
                    200,
                    "Product removed successfully");
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "An error occurred while removing Product. ProductId: {ProductId}",
                    id);

                return new ApiResponseDto<string>(
                    null,
                    false,
                    500,
                    ex.Message);
            }
        }
    }
}
