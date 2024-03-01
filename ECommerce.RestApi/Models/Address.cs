using MongoDB.Bson.Serialization.Attributes;

namespace ECommerce.RestApi.Models
{
    public class Address
    {
        [BsonElement("city")]
        public string City { get; set; }

        [BsonElement("district")]
        public string District { get; set; }

        [BsonElement("street")]
        public string Street { get; set; }

        [BsonElement(elementName: "zipcode")]
        public string ZipCode { get; set; }

        [BsonElement("apartmentNumber")]
        public string ApartmentNumber { get; set; }

        [BsonElement("floor")]
        public string Floor { get; set; }

    }
}
