using ECommerce.RestApi.Models;

namespace ECommerce.RestApi.DTOs
{
    public record class CartDTO(
        List<CartItemDTO> Items,
        decimal TotalPrice,
        int Quantity
        );
}
