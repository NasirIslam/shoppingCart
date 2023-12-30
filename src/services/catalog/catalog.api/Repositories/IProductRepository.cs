using catalog.api.Entities;

namespace catalog.api.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Products>> GetProducts();
        Task<Products> GetProductById(string productId);
        Task<IEnumerable<Products>> GetProductsByName(string productName);
        Task<IEnumerable<Products>> GetProductsByCategory(string category);
        Task CreateProduct(Products product);
        Task<bool> UpdateProduct(Products product);
        Task DeleteProduct(string productId);

    }
}
