using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ECommerce.RestApi.Models
{
    [Serializable]
    public class Order
    {
        [BsonId, BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        
        [BsonElement("userId"), BsonRepresentation(BsonType.String)]
        public string UserId { get; set; }
        
        [BsonElement("date"), BsonRepresentation(BsonType.DateTime)]
        public DateTime Date { get; set; }
        
        [BsonElement("price"), BsonRepresentation(BsonType.Decimal128)]
        public DateTime Price { get; set; }

        [BsonIgnore]
        public OrderDetail OrderDetail  { get; set; }
    }
}
