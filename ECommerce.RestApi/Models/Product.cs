﻿using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using ECommerce.RestApi.Dto;

namespace ECommerce.RestApi.Models
{
    [Serializable]
    public class Product
    {
        [BsonId, BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("categoryId"), BsonRepresentation(BsonType.String)]
        public string CategoryId { get; set; }

        [BsonElement("name"), BsonRepresentation(BsonType.String)]
        public string Name { get; set; }

        [BsonElement("description"), BsonRepresentation(BsonType.String)]
        public string Description { get; set; }

        [BsonElement("rating"), BsonRepresentation(BsonType.Int32)]
        public int Rating { get; set; }

        [BsonElement("imageUrl"), BsonRepresentation(BsonType.String)]
        public string ImageUrl { get; set; }

        [BsonElement("stock"), BsonRepresentation(BsonType.Int32)]
        public int Stock { get; set; }

        [BsonElement("price"), BsonRepresentation(BsonType.Decimal128)]
        public decimal Price { get; set; }

        public static ProductDto ConvertToproductDTO(Product product)
        {
            var productDTO = new ProductDto
            {
                Id = product.Id,
                CategoryId = product.CategoryId,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Rating = product.Rating,
                Stock = product.Stock,
                ImageUrl = product.ImageUrl,
            };

            return productDTO;
        }

        public static IEnumerable<ProductDto> ConvertToProductDTO(IEnumerable<Product> products)
        {
            IEnumerable<ProductDto> productsDto = products.Select(p => Product.ConvertToproductDTO(p));
            return productsDto;
        }
    }
}
