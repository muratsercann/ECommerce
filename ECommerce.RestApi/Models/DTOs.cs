using MongoDB.Bson;
using MongoDB.Bson.Serialization.IdGenerators;
using System.Security.Cryptography.X509Certificates;

namespace ECommerce.RestApi.Models.DTOs
{
    public record AddToCartDTO(string userId, string productId, int quantity);

    public record CategoryDto(string Name, string ParentId = "", string Description = "");

    public record ProductDto
    {
        public string Id { get; set; }
        public string CategoryId { get; init; }
        public string Name { get; init; }
        public int Rating { get; init; }
        public int Stock { get; init; }
        public decimal Price { get; init; }
        public string Description { get; init; } = "";
        public string ImageUrl { get; init; } = "";
    }

    public record AddProductDto
    {
        public string Name { get; init; }
        public int Rating { get; init; }
        public int Stock { get; init; }
        public decimal Price { get; init; }
        public string Description { get; init; } = "";
        public string ImageUrl { get; init; } = "";
    }
    public record CreateUserDto(
           string Username,
           string Password,
           string FirstName,
           string LastName,
           string? Email = "",
           List<Address>? Addresses = null

       );

    public record UserSummaryDto
    {
        public string Username { get; init; }
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public string Email { get; init; }
        public int FavoritesCount { get; init; }
        public int ShoppingCarItemsCount { get; init; }

        
    }

    public record UserDetailDto()
    {
        public string Username { get; set; }
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public string? Email { get; init; }
        public List<Address>? Addresses { get; init; }
        public IEnumerable<ProductDto> Favorites { get; init; }
        public ShoppingCartDto ShoppingCart { get; init; }
    }

    public record ShoppingCartItemDto(ProductDto product, int quantity);

    public record ShoppingCartDto(
         List<ShoppingCartItemDto> Items,
         decimal TotalPrice,
         int TotalItemCount
        )
    {

        public ShoppingCartDto() : this
            (new List<ShoppingCartItemDto>(),
            0,
            0)
        {
        }

    }


}
