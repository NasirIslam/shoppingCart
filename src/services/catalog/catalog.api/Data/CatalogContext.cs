using catalog.api.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace catalog.api.Data
{
    public class CatalogContext : ICatalogContext
    {
        public IMongoCollection<Products> Product { get; }
        public CatalogContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));
            Product = database.GetCollection<Products>(configuration.GetValue<string>("DatabaseSettings:CollectioNames"));

        }
    }
}
