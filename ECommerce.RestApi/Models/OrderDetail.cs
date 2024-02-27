using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace ECommerce.RestApi.Models
{
    [Serializable]
    public class OrderDetail
    {
        [BsonId,BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("orderId"), BsonRepresentation(BsonType.String)]
        public string OrderId { get; set; }

        [BsonElement("productId"), BsonRepresentation(BsonType.String)]
        public string ProductId { get; set; }

        [BsonElement("quantity"), BsonRepresentation(BsonType.Int32)]
        public int Quantity { get; set; }

        [BsonElement("price"), BsonRepresentation(BsonType.Decimal128)]
        public decimal Price { get; set; }
    }
}
