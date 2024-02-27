using ECommerce.RestApi.Models;
using ECommerce.RestApi.Services;
using MongoDB.Driver;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace ECommerce.RestApi
{
    public static class SeedData
    {
        //static readonly string dbname = "ECommerceDB";
        //static readonly string connectionString = "mongodb://localhost:27017";

        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            //    var mongoClient = new MongoClient(connectionString);
            //    var db = mongoClient.GetDatabase(dbname);


            var userService = serviceProvider.GetService<IUserService>()!;
            var categoryService = serviceProvider.GetService<ICategoryService>()!;
            var productService = serviceProvider.GetService<IProductService>()!;
            //var orderService = serviceProvider.GetService<IOrderService>()!;
            //var orderDetailservice = serviceProvider.GetService<IOrderDetailService>()!;
            //var cartService = serviceProvider.GetService<ICartService>()!;
            //var cartItemService = serviceProvider.GetService<ICartItemService>()!;
            
            var userCount = await userService.GetUsersCountAsync();
            var categoryCount = await categoryService.GetCategoryCountAsync();
            var productCount = await productService.GetCountAsync();

            if (userCount == 0)
                await SeedUserCollection(userService);
            if (categoryCount == 0)
                await SeedCategoryCollection(categoryService);
            if (productCount == 0)
                await SeedProductCollection(categoryService,productService);
        }

        private static async Task SeedUserCollection(IUserService userService)
        {
            var users = new List<User>();
            for (int i = 1; i <= 500; i++)
            {
                users.Add(new User
                {
                    Username = $"user_{i}",
                    FirstName = $"Firstname_{i}",
                    LastName = $"Lastname_{i}",
                    Address = $"Address_{i}",
                    Email = $"user_{i}@mail.com",
                });
            }
            await userService.CreateManyAsync(users);
        }

        private static async Task SeedCategoryCollection(ICategoryService categoryService)
        {
            var categories = new List<Category>();

            for (int i = 1; i <= 10; i++)
            {
                categories.Add(new Category
                {
                    Name = $"Category_{i}",
                    Description = $"Category_{i} Description"
                });
            }

            await categoryService.CreateManyAsync(categories);
        }

        private static async Task SeedProductCollection(ICategoryService categoryService,IProductService productService)
        {
            var categories = await categoryService.GetCategoriesAsync();
            var products = new List<Product>();
            foreach (var c in categories)
            {
                for (int i = 1; i <= 5; i++)
                {
                    products.Add(new Product
                    {
                        CategoryId = c.Id,
                        Name = $"Product_{c.Id}_{i+1}",
                        ImageUrl = $"Product_{c.Id}_{i + 1}.png",
                        Price = 45M,
                        Rating = i,
                        Stock = 5,
                        Description = $"Product Description {c.Id}_{i + 1}"
                    });
                }
            }

            await productService.CreateManyAsync(products);

        }


    }
}
