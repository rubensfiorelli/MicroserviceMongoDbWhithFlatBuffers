using Core.Contracts;
using Core.Entities;
using MongoDB.Driver;

namespace Data.Repositories
{
    public sealed class ProductRepository : IProductRepository
    {
        private readonly IMongoCollection<Product> _dbCollection;
        private readonly FilterDefinitionBuilder<Product> _filterBuilder = Builders<Product>.Filter;

        public ProductRepository(IMongoDatabase database, string collectionName)
        {
            _dbCollection = database.GetCollection<Product>(collectionName);
        }

        public async Task AddAsync(Product product)
        {
            ArgumentNullException.ThrowIfNull(product);

            await _dbCollection.InsertOneAsync(product);

        }

        public async Task DeleteAsync(string productId)
        {
            FilterDefinition<Product> filter = _filterBuilder.Eq(n => n.ProductId, productId);
            await _dbCollection.DeleteOneAsync(filter);

        }

        public async IAsyncEnumerable<Product> GetAllAsync()
        {
            var products = await _dbCollection
                .Find(_filterBuilder
                .Where(n => !n.Inactive))
                .ToListAsync();

            foreach (var item in products)
                yield return item;

        }

        public async Task<Product> GetId(string productId)
        {
            FilterDefinition<Product> filter = _filterBuilder.Eq(n => n.ProductId, productId);

            return await _dbCollection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(Product product)
        {
            ArgumentNullException.ThrowIfNull(product);

            FilterDefinition<Product> filter = _filterBuilder.Eq(existing=> existing.ProductId, product.ProductId);
            await _dbCollection.ReplaceOneAsync(filter, product);

        }
    }
}
