using MongoDB.Bson;
using MongoDB.Bson.Serialization.IdGenerators;

namespace ECommerce.RestApi.Models.DTOs
{
    public record class AddToCartDTO(string userId, string productId, int quantity);



    public record class CategoryDTO(string Name, string ParentId = "", string Description = "");

    public record class ProductDTO
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

    public record class UserDTO(
           string Username,
           string Password,
           string FirstName,
           string LastName,
           string? Email = "",
          List<Address>? Addresses = null

       );
    public record class CartItemDTO(ProductDTO product, int quantity);

    public record class CartDTO(
       List<CartItemDTO> Items,
       decimal TotalPrice,
       int TotalItemCount
    );

   

}
