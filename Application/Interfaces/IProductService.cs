using Application.DTOs;
using Core.Contracts;

namespace Application.Interfaces
{
    public interface IProductService
    {
        Task<bool> Add(ProductRequest productRequest);
        Task<IResult> Update(string productId, ProductRequest request);
        Task Delete(string productId);
        IAsyncEnumerable<ProductResponse> GetAll(int skip = 0, int take = 25);
        Task<ProductResponse> GetId(string productId);

    }
}
