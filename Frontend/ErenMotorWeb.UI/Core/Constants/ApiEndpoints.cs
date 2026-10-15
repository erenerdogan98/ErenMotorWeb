namespace ErenMotorWeb.UI.Core.Constants
{
    public static class ApiEndpoints
    {
        public static class Brands
        {
            public const string Base = "Brands";

            public static string GetAll => Base;

            public static string GetById(int id)
                => $"{Base}/{id}";

            public static string Create => Base;

            public static string Update => Base;

            public static string Delete(int id)
                => $"{Base}/{id}";
        }

        public static class Categories
        {
            public const string Base = "Categories";

            public static string GetAll => Base;

            public static string GetById(int id)
                => $"{Base}/{id}";     
            public static string Create => Base;

            public static string Update => Base;

            public static string Delete(int id)
                => $"{Base}/{id}";
            // Özel Category endpointleri
            public static string GetAllWithParent
                => $"{Base}/with-parent";

            public static string GetSubCategories(int id)
                => $"{Base}/{id}/sub-category";
        }

        public static class Products
        {
            public const string Base = "Products";

            public static string GetAll => Base;

            public static string GetById(int id)
                => $"{Base}/{id}";

            public static string Create => Base;

            public static string Update => Base;

            public static string Delete(int id)
                => $"{Base}/{id}";
            public static string Search(string search)
        => $"{Base}/search?search={Uri.EscapeDataString(search)}";
            public static string Paged(int page, int pageSize)
    => $"{Base}/paged?page={page}&pageSize={pageSize}";
            public static string Remove(int id) => $"{Base}/remove/{id}";
        }

        public static class ProductImages
        {
            public const string Base = "ProductImages";

            public static string GetAll => Base;

            public static string GetById(int id)
                => $"{Base}/{id}";

            public static string Create => Base;

            public static string Update => Base;

            public static string Delete(int id)
                => $"{Base}/{id}";
        }
        public static class PlatformConnections
        {
            public const string Base = "PlatformConnections";

            public static string GetAll => Base;

            public static string GetById(int id)
                => $"{Base}/{id}";

            public static string Create => Base;

            public static string Update => Base;

            public static string Delete(int id)
                => $"{Base}/{id}";
        }

        public static class Origins
        {
            public const string Base = "Origins";

            public static string GetAll => Base;

            public static string GetById(int id)
                => $"{Base}/{id}";

            public static string Create => Base;

            public static string Update => Base;

            public static string Delete(int id)
                => $"{Base}/{id}";
        }

        public static class Supplier
        {
            public const string Base = "Suppliers";

            public static string GetAll => Base;

            public static string GetById(int id)
                => $"{Base}/{id}";

            public static string Create => Base;

            public static string Update => Base;

            public static string Delete(int id)
                => $"{Base}/{id}";
        }
    }
}
