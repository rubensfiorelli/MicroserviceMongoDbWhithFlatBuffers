
using Core.Contracts;
using Core.Notifications;
using Core.Validations;
using MongoDB.Bson.Serialization.Attributes;

#nullable disable
namespace Core.Entities
{
    [BsonIgnoreExtraElements]
    public class Product : IValidation, IContract
    {

        private List<Notification> _notifications;

        public string ProductId { get; init; } = Guid.NewGuid().ToString();
        public DateTimeOffset CreatedAt { get; init; } = DateTimeOffset.UtcNow;
        public string Description { get; init; }
        public bool Active { get; init; } = true;
        public bool Inactive { get; init; } = false;
        public DateTimeOffset Manufacturing { get; init; }
        public DateTimeOffset Validate { get; init; }
        public int SupplierCode { get; init; }
        public string SupplierDescription { get; init; }
        public string SupplierCNPJ { get; init; }
        public IReadOnlyList<Notification> Notifications => _notifications;

        public void InactivateProduct() => _ = new Product() { Inactive = true };
        public void AddNotifications(List<Notification> notifications) => _notifications = notifications;
        public void SetupProduct(string description,
                                 DateTimeOffset manufaturing,
                                 DateTimeOffset validate,
                                 int supplierCode,
                                 string supplierDescription,
                                 string supplierCNPJ)
        {
            _ = new Product() { Description = description };
            _ = new Product() { Manufacturing = manufaturing };
            _ = new Product() { Validate = validate };
            _ = new Product() { SupplierCode = supplierCode };
            _ = new Product() { SupplierDescription = supplierDescription };
            _ = new Product() { SupplierCNPJ = supplierCNPJ };

        }

        public bool IsValid()
        {
            var contracts = new ContractValidation<Product>()
                .ValidManufactureIsOk(Manufacturing, Validate, "Invalid manufaturing date", nameof(Manufacturing));

            return contracts.IsValid();
        }

        public static readonly Product NULL = new NullProduct();
        private class NullProduct : Product { }

    }



}
