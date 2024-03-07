using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ECommerce.RestApi.Models
{
    [Serializable]
    public class ShoppingCart
    {
        [BsonElement("items")]
        public List<ShoppingCartItem> Items { get; set; } = new List<ShoppingCartItem>();

        
        [BsonElement(elementName: "totalItemQuantity")]
        public int TotalItemQuantity { get; set; }
    }
}
