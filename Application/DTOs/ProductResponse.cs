using Core.Entities;

namespace Application.DTOs
{
    public readonly record struct ProductResponse
    {
        public string ProductId { get; init; }
        public string Description { get; init; }
        public bool Active { get; init; }
        public DateTimeOffset Manufacturing { get; init; }
        public DateTimeOffset Validate { get; init; }
        public int SupplierCode { get; init; }
        public string SupplierDescription { get; init; }
        public string SupplierCNPJ { get; init; }

        public static implicit operator ProductResponse(Product product)
        {
            return new()
            {
                ProductId = product.ProductId,
                Description = product.Description,
                Active = product.Active,
                Manufacturing = product.Manufacturing,
                Validate = product.Validate,
                SupplierCode = product.SupplierCode,
                SupplierDescription = product.SupplierDescription,
                SupplierCNPJ = product.SupplierCNPJ

            };
        }
    }
}
