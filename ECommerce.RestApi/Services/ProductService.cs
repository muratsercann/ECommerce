using AutoMapper;
using ECommerce.RestApi.Models;
using ECommerce.RestApi.Dto;
using MongoDB.Driver;

namespace ECommerce.RestApi.Services
{
    public class ProductService : IProductService
    {
        private readonly ECommerceContext _mongoContext;
        private readonly IMapper _mapper;

        public ProductService(ECommerceContext mongoContext, IMapper mapper)
        {
            _mongoContext = mongoContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductDto>> CreateManyAsync(IEnumerable<ProductDto> productsDto)
        {
            var products = _mapper.Map<IEnumerable<Product>>(productsDto);
            await _mongoContext.Products.InsertManyAsync(products);
            return productsDto;
        }

        public async Task<AddProductDto> CreateOneAsync(AddProductDto addProductDto)
        {
            var product = _mapper.Map<Product>(addProductDto);
            await _mongoContext.Products.InsertOneAsync(product);
            return addProductDto;
        }

        public async Task<bool> DeleteAsync(string productId)
        {
            var filter = Builders<Product>.Filter.Eq(p => p.Id, productId);
            var result = await _mongoContext.Products.DeleteOneAsync(filter);
            return result.IsAcknowledged;
        }

        public async Task<long> GetCountAsync()
        {
            return await _mongoContext.Products.CountDocumentsAsync(FilterDefinition<Product>.Empty);
        }

        public async Task<ProductDto> GetProductDtoAsync(string productId)
        {
            var product = await GetProductAsync(productId);
            var productDto = _mapper.Map<ProductDto>(product);
            return productDto;
        }

        public async Task<IEnumerable<ProductDto>> GetProductsDtoAsync(IEnumerable<string> productIds)
        {
            var products = await GetProductsAsync(productIds);
            var productsDto = _mapper.Map<IEnumerable<ProductDto>>(products);
            return productsDto;
        }

        public async Task<IEnumerable<ProductDto>> GetProductsDtoAsync()
        {
            var products = await _mongoContext.Products.Find(Builders<Product>.Filter.Empty).ToListAsync();
            var productsDto = _mapper.Map<IEnumerable<ProductDto>>(products);
            return productsDto;
        }

        public async Task<IEnumerable<ProductDto>> GetShoppingCartProductsDto(ShoppingCart shoppingCart)
        {
            if (shoppingCart?.Items is null || shoppingCart.Items.Count == 0)
            {
                return new List<ProductDto>();
            }

            var filter = Builders<Product>.Filter.In(p => p.Id, shoppingCart.Items.Select(f => f.ProductId));
            var products = await _mongoContext.Products.Find(filter).ToListAsync();
            var productsDto = _mapper.Map<IEnumerable<ProductDto>>(products);
            return productsDto;
        }

        public async Task<bool> IsExistingProductAsync(string productId)
        {
            var filter = Builders<Product>.Filter.Eq(p => p.Id, productId);
            var isExist = await _mongoContext.Products.Find(filter).AnyAsync();

            return isExist;
        }

        public async Task<IEnumerable<ProductDto>> GetProductsDtoByCategoryAsync(string categoryId)
        {
            var products = await GetProductsByCategoryAsync(categoryId);
            var productsDto = _mapper.Map<IEnumerable<ProductDto>>(products);
            return productsDto;
        }

        public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(string categoryId)
        {
            var filter = Builders<Product>.Filter.Eq(p => p.CategoryId, categoryId);
            var products = await _mongoContext.Products.Find(filter).ToListAsync();
            return products;
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await _mongoContext.Products.Find(Builders<Product>.Filter.Empty).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsAsync(IEnumerable<string> productIds)
        {

            var filter = Builders<Product>.Filter.In(u => u.Id, productIds);
            var products = await _mongoContext.Products.Find(filter).ToListAsync();
            return products;
        }

        public async Task<Product> GetProductAsync(string productId)
        {
            var filter = Builders<Product>.Filter.Eq(p => p.Id, productId);
            return await _mongoContext.Products.Find(filter).FirstOrDefaultAsync();
        }

        
    }
}
