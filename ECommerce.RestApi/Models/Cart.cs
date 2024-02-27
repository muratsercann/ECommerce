using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ECommerce.RestApi.Models
{
    [Serializable]
    public class Cart
    {
        [BsonId, BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("userId"), BsonRepresentation(BsonType.String)]
        public string UserId { get; set; }

        [BsonIgnore]
        public decimal TotalPrice { get; set; }

        [BsonIgnore]
        public List<CartItem> Products { get; set; }
    }
}
