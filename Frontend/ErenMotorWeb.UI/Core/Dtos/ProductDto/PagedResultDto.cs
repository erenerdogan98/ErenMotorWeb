namespace ErenMotorWeb.UI.Core.Dtos.ProductDto
{
    public record PagedResultDto<T>
    {
        public IEnumerable<T> Items { get; set; } = [];
        public int TotalCount { get; set; }
        public int ActiveCount { get; set; }

        public int OutOfStockCount { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
    }
}
