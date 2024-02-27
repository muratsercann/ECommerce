using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ECommerce.RestApi.Models
{
    [Serializable]
    public class Category
    {
        [BsonId, BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        
        [BsonElement("name"), BsonRepresentation(BsonType.String)]
        public string Name { get; set; }
        
        [BsonElement("parentId"), BsonRepresentation(BsonType.String)]
        public string ParentId { get; set; }
        
        [BsonElement("description"), BsonRepresentation(BsonType.String)]
        public string Description { get; set; }
    }
}
