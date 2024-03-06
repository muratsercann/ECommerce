using ECommerce.RestApi.Models;
using System.Linq.Expressions;

namespace ECommerce.RestApi.Dto
{
    public record ProductDto
    {
        public string Id { get; init; }
        public string CategoryId { get; init; }
        public string Name { get; init; }
        public int Rating { get; init; }
        public int Stock { get; init; }
        public decimal Price { get; init; }
        public string Description { get; init; } = "";
        public string ImageUrl { get; init; } = "";

        public static Expression<Func<Product, ProductDto>> Selector
        {
            get
            {
                return product => new ProductDto()
                {
                    Id = product.Id,
                    CategoryId = product.CategoryId,
                    Name = product.Name,
                    Rating = product.Rating,
                    Stock = product.Stock,
                    Price = product.Price,
                    Description = product.Description,
                    ImageUrl = product.ImageUrl,
                };

            }
        }
    }

    public record CreateProductDto
    {
        public string Name { get; init; }
        public int Rating { get; init; }
        public int Stock { get; init; }
        public decimal Price { get; init; }
        public string Description { get; init; } = "";
        public string ImageUrl { get; init; } = "";
    }

}
