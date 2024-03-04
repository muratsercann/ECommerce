using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ECommerce.RestApi.Models
{
    [Serializable]
    public class ShoppingCart
    {
        [BsonElement("items")]
        public List<ShoppingCartItem> Items { get; set; } = new List<ShoppingCartItem>();

        
        [BsonElement(elementName: "totalItemCount")]
        public int TotalItemCount { get; set; }

        [BsonIgnore]//msercan todo : delete this. This information should be in the dto object.
        public decimal TotalPrice { get; set; }

    }
}
