using AutoMapper;
using ECommerce.RestApi.Models;
using ECommerce.RestApi.Models.DTOs;

internal class Program
{
    private static void Main(string[] args)
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<User, UserDetailDto>()
                .ForMember(dest => dest.Favorites, opt => opt.MapFrom(src => GetFavoriteProducts(src.Favorites)))
                .ForMember(dest => dest.ShoppingCart, opt => opt.MapFrom(src => GetShoppingCartDto(src.Cart)));
        });

        // AutoMapper örneği oluştur
        IMapper mapper = config.CreateMapper();
        var user = new User
        {
            Id = "1",
            FirstName = "murat",
            LastName = "sercan",
            Description = "Descrpt....",
            Addresses = new List<Address> { new Address
            {
                City = "Manisa",
                District = "Sarıgöl",
                Street = "Bardakçılar Cd.",
                ApartmentNumber = "48",
                Floor = "1",
                ZipCode = "45470"
            } },
            Favorites = new List<string> { "1", "2", "3" }
        };

        user.Cart = new ShoppingCart
        {
            Items = new List<ShoppingCartItem> {
                new ShoppingCartItem { ProductId = "1", Quantity = 2 },
                new ShoppingCartItem { ProductId = "2", Quantity = 1 },
            }
        };

        UserDetailDto destinationObject = mapper.Map<User, UserDetailDto>(user);


        Console.WriteLine(destinationObject);

        Console.ReadLine();
    }


    public static IEnumerable<ProductDto> GetFavoriteProducts(List<string> favoriteIds)
    {
        var productDtos = favoriteIds.Select(x => new ProductDto
        {
            Id = $"id_{x}",
            Name = $"name_{x}",
            Price = 45,
            Description = $"desc_{x}"
        });
        return productDtos;
    }

    public static ShoppingCartDto GetShoppingCartDto(ShoppingCart cart)
    {
        if ((cart?.Items?.Count ?? 0) == 0)
        {
            return new ShoppingCartDto();
        }

        var items = new List<ShoppingCartItemDto>();

        foreach (var x in cart.Items)
        {
            items.Add(new ShoppingCartItemDto(
               new ProductDto
               {
                   Id = $"id_{x.ProductId}",
                   Name = $"name_{x.ProductId}",
                   Price = 45,
                   Description = $"desc_{x.ProductId}"

               },
               x.Quantity
            ));
        }

        var cartDto = new ShoppingCartDto(items, 0, 0);

        return cartDto;
    }
}

public class User
{
    public string Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Description { get; set; }

    public List<Address>? Addresses { get; set; }

    public List<string> Favorites { get; set; } = new List<string>();

    public ShoppingCart Cart { get; set; } = new ShoppingCart();

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


