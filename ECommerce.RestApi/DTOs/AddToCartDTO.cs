namespace ECommerce.RestApi.DTOs
{
    public record class AddToCartDTO(string userId, string productId, int quantity);
}
