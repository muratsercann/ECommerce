using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ECommerce.RestApi.Models
{
    [Serializable]
    public class User
    {
        [BsonId, BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement(elementName: "username"), BsonRepresentation(BsonType.String)]
        public string Username { get; set; }
        [BsonElement(elementName: "password"), BsonRepresentation(BsonType.String)]
        public string Password { get; set; }
        [BsonElement(elementName: "firstname"), BsonRepresentation(BsonType.String)]
        public string FirstName { get; set; }
        [BsonElement(elementName: "lastname"), BsonRepresentation(BsonType.String)]
        public string LastName { get; set; }
        [BsonElement(elementName: "email"), BsonRepresentation(BsonType.String)]
        public string Email { get; set; }
        [BsonElement(elementName: "adress"), BsonRepresentation(BsonType.String)]
        public string Address { get; set; }
    }
}
