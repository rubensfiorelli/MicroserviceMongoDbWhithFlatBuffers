using Core.Entities;

namespace Core.Contracts
{
    public interface IProductRepository
    {
        Task AddAsync(Product product);
        Task UpdateAsync(Product product);
        Task DeleteAsync(string productId);
        IAsyncEnumerable<Product> GetAllAsync();
        Task<Product> GetId(string productId);
    }
}
