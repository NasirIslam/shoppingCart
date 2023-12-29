using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
namespace catalog.api.Entities

{
    public class Products
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        private string id;
        public string Id { get=>id; set=>id; }
        [BsonElement("Name")]
        public string? Name { get; set; }

        public string? Category { get; set; }
        public string? Summary { get; set; }
        public string? Description { get; set; }
        public string? ImageFile { get; set; }
        public string? Price { get; set; }

        
    }
}
