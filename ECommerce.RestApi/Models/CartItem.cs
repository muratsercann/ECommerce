using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ECommerce.RestApi.Models
{
    [Serializable]
    public class CartItem
    {
        [BsonId, BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        
        [BsonElement("cartId"), BsonRepresentation(BsonType.String)]
        public string CartId { get; set; }
        
        [BsonElement( "productId"), BsonRepresentation(BsonType.String)]
        public string ProductId { get; set; }
        
        [BsonElement("quantity"), BsonRepresentation(BsonType.Int32)]
        public int Quantity { get; set; }
    }
}
