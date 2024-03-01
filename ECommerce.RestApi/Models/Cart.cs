using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ECommerce.RestApi.Models
{
    [Serializable]
    public class Cart
    {
        [BsonElement("items")]
        public List<CartItem> Items { get; set; } = new List<CartItem>();

        
        [BsonElement(elementName: "totalItemCount")]
        public int TotalItemCount { get; set; }

        [BsonIgnore]//msercan todo : delete this. This information should be in the dto object.
        public decimal TotalPrice { get; set; }

    }
}
