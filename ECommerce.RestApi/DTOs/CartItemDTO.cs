using ECommerce.RestApi.Models;

namespace ECommerce.RestApi.DTOs
{
    public record class CartItemDTO(Product product, int quantity, decimal price);
}
