namespace ECommerce.RestApi.Dto
{
    public record AddToCartDto(string userId, string productId, int quantity);

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
