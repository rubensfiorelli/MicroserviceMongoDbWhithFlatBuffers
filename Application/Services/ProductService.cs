using Application.DTOs;
using Application.Interfaces;
using Application.Results;
using Core.Contracts;
using Core.Entities;
using System.Data.Common;

namespace Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Add(ProductRequest productRequest)
        {
            Result result;
            var entity = (Product)productRequest;
            if (entity.IsValid())
            {
                try
                {
                    await _repository.AddAsync(entity);
                    result = new Result(201, $"{entity.ProductId} successfully created", true, productRequest);
                    return result.Success;

                }
                catch (DbException)
                {
                    result = new Result(400, $"Fail to create product", false, productRequest);
                    
                }

            }
            result = new Result(500, $"Internal server error", false, productRequest);            
            result.AddNotification([.. result.Notifications]);
            return result.Success;
        }

        public async Task Delete(string productId)
        {
            await _repository.DeleteAsync(productId);
            
        }

        public async IAsyncEnumerable<ProductResponse> GetAll(int skip = 0, int take = 25)
        {
            var getAll = _repository.GetAllAsync();

            await foreach (var item in getAll)
                yield return item;
            
        }

        public async Task<ProductResponse> GetId(string productId)
        {
            var getId = await _repository.GetId(productId);
            if (getId is null)
                return Product.NULL;

            return (ProductResponse)getId;
        }

        public async Task<IResult> Update(string productId, ProductRequest request)
        {
            Result result;
            var entity = (Product)request;

            await _repository.GetId(productId);
            if (entity is null)
                return (IResult)Product.NULL;

            if (entity.IsValid())
            {
                try
                {
                    entity.SetupProduct(request.Description,
                        request.Manufacturing,
                        request.Validate,
                        request.SupplierCode,
                        request.SupplierDescription,
                        request.SupplierCNPJ);

                    await _repository.UpdateAsync(entity);
                    result = new Result(200, $"{entity.ProductId} successfully updated", true, entity);                  

                }
                catch (DbException)
                {
                    _ = new Result(400, $"Fail update product", false, entity);
                }
                result = new Result(500, $"Internal Server error", false, entity);
                result.AddNotification([.. result.Notifications]);
                return result;
            }
            return (IResult)Product.NULL;
        }
    }
}
