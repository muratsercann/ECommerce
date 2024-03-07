namespace ECommerce.RestApi.Dto
{
    public record AddToCartDto(string userId, string productId, int quantity);

    public record ShoppingCartItemDto(ProductDto product, int quantity, decimal price);

    public record ShoppingCartDto(
         List<ShoppingCartItemDto> Items,
         decimal TotalPrice,
         int TotalItemQuantity
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
