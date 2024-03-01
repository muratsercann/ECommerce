using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ECommerce.RestApi.Models
{
    [Serializable]
    public class User
    {
        [BsonId, BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("username")]
        public string Username { get; set; }
        [BsonElement("password")]
        public string Password { get; set; }
        [BsonElement("firstName")]
        public string FirstName { get; set; }
        [BsonElement("lastName")]
        public string LastName { get; set; }
        [BsonElement("email")]
        public string Email { get; set; }
        [BsonElement("addresses")]
        public List<Address> Addresses { get; set; } = new List<Address>();

        [BsonElement("favorites")]
        public List<string> Favorites { get; set; } = new List<string>();

        [BsonElement(elementName: "cart")]
        public Cart Cart { get; set; } = new Cart();    

        [BsonElement("orders")]
        public List<Order> Orders { get; set; } = new List<Order>();    
    }
}
