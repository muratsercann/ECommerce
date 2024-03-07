using AutoMapper;
using ECommerce.RestApi.Models;
using ECommerce.RestApi.Dto;
using MongoDB.Driver;
using System.Linq.Expressions;
using MongoDB.Bson;

internal class Program
{
    static readonly string userId = "65e0848b83524b9750fee6b3";
    private static void Main(string[] args)
    {
        var client = new MongoClient("mongodb://localhost:27017");
        var database = client.GetDatabase("ECommerceDB"); // Veritabanı adınızı belirtin
        var _productCollection = database.GetCollection<Product>("product");
        var _userCollection = database.GetCollection<User>("user");


        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Product, ProductDto>();
        });

        IMapper mapper = config.CreateMapper();


        //Favorite Products

        var filter = Builders<User>.Filter.Eq(u => u.Id, userId);
        var uFavor = _userCollection.Find(filter)
            .Project(user => user.Favorites)
            .FirstOrDefault();

        var cart = new ShoppingCart();
        var result = cart.Items.Sum(item => item.Quantity);


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
               x.Quantity,
               4
            ));
        }

        var cartDto = new ShoppingCartDto(items, 0, 0);

        return cartDto;
    }
}

public record UserFavoritesDto()
{
    public string UserId { get; init; }
    public List<Product> Products { get; init; }
}


