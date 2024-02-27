using ECommerce.RestApi;
using ECommerce.RestApi.Services;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);




// Add services to the container.

//builder.Services.AddScoped<IUserService, UserService>();    
builder.Services.Configure<ECommerceDbSettings>(builder.Configuration.GetSection(nameof(ECommerceDbSettings)));
builder.Services.AddSingleton<ECommerceContext>();
builder.Services.AddScoped<IUserService,UserService>();
builder.Services.AddScoped<ICategoryService,CategoryService>();
builder.Services.AddScoped<IProductService,ProductService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<ICartItemService, CartItemService>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    await SeedData.InitializeAsync(services); 
}

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
