using ECommerce.RestApi.Models;
using System.Linq.Expressions;

namespace ECommerce.RestApi.Dto
{
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
        public int ShoppingCartItemsCount { get; init; }

        public static Expression<Func<User, UserSummaryDto>> Selector
        {
            get
            {
                return user => new UserSummaryDto()
                {
                    Username = user.Username,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    FavoritesCount = user.Favorites.Count,
                    ShoppingCartItemsCount = user.Cart != null ? user.Cart.Items.Sum(i => i.Quantity) : 0,

                };
            }
        }
    }
}
