using catalog.api.Entities;
using MongoDB.Driver;

namespace catalog.api.Data
{
    public interface ICatalogContext
    {
         IMongoCollection<Products> Product {get; }

    }
}
