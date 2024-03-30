using Core.Entities;

namespace Application.DTOs
{
    public readonly record struct ProductRequest
    {
        public string Description { get; init; }
        public DateTimeOffset Manufacturing { get; init; }
        public DateTimeOffset Validate { get; init; }
        public int SupplierCode { get; init; }
        public string SupplierDescription { get; init; }
        public string SupplierCNPJ { get; init; }

        public static implicit operator Product(ProductRequest request)
        {
            return new Product()
            {
                Description = request.Description,
                Manufacturing = request.Manufacturing,
                Validate = request.Validate,
                SupplierCode = request.SupplierCode,
                SupplierDescription = request.SupplierDescription,
                SupplierCNPJ = request.SupplierCNPJ

            };
        }
    }
}
