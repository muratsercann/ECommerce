using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ECommerce.RestApi.Models
{
    [Serializable]
    public class ShoppingCartItem
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string ProductId { get; set; }
        
        [BsonElement("quantity"), BsonRepresentation(BsonType.Int32)]
        public int Quantity { get; set; }
    }
}
