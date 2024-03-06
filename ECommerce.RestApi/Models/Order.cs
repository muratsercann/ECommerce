using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ECommerce.RestApi.Models
{
    [Serializable]
    public class Order
    {
        [BsonElement("date"), BsonRepresentation(BsonType.DateTime)]
        public DateTime Date { get; set; }

        [BsonElement("items")]
        public List<ShoppingCartItem> Items { get; set; }

        //public Address DeliveryAddress { get; set; }
    }
}
