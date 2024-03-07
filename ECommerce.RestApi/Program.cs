using ECommerce.RestApi;
using ECommerce.RestApi.Repositories;
using ECommerce.RestApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.Configure<ECommerceDbSettings>(builder.Configuration.GetSection(nameof(ECommerceDbSettings)));
builder.Services.AddSingleton<ECommerceContext>();

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IFavoriteRepository, FavoriteRepository>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IFavoritesService, FavoritesService>();
builder.Services.AddScoped<IShoppingCartService, ShoppingCartService>();
builder.Services.AddAutoMapper(typeof(MappingProfile));


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


//using (var scope = app.Services.CreateScope())
//{
//    var services = scope.ServiceProvider;

//    await SeedData.InitializeAsync(services); 
//}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
