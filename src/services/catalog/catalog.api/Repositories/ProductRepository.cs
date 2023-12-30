using catalog.api.Data;
using catalog.api.Entities;
using MongoDB.Driver;

namespace catalog.api.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private ICatalogContext _context;
        public ProductRepository(ICatalogContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Products>> GetProducts()
        {
            return await _context.
                         Product.
                         Find(p => true).
                         ToListAsync();

        }

        public async Task CreateProduct(Products product)
        {
            await _context.Product.InsertOneAsync(product);

        }

        public async Task DeleteProduct(string productId)
        {
            await _context.Product.DeleteOneAsync(p => p.Id == productId);
        }

        public async Task<Products> GetProductById(string productId)
        {
            return await _context.Product.Find(p => p.Id == productId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Products>> GetProductsByCategory(string category)
        {
            FilterDefinition<Products> filters = Builders<Products>.Filter.Eq(p => p.Category, category);
            return await _context.Product.Find<Products>(filters).ToListAsync();
        }

        public async Task<IEnumerable<Products>> GetProductsByName(string productName)
        {
            FilterDefinition<Products> filters = Builders<Products>.Filter.Eq(p => p.Name, productName);
            return await _context.Product.Find(filters).ToListAsync();
        }

        public async Task<bool> UpdateProduct(Products product)
        {
            var updateResult = await _context.Product.ReplaceOneAsync(filter: g => g.Id == product.Id, replacement: product);
            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }
    }
}
