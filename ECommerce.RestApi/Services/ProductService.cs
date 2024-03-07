using AutoMapper;
using ECommerce.RestApi.Models;
using ECommerce.RestApi.Dto;
using MongoDB.Driver;
using ECommerce.RestApi.Repositories;

namespace ECommerce.RestApi.Services
{
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;

        public ProductService(IMapper mapper, IProductRepository productRepository)
        {
            _mapper = mapper;
            _productRepository = productRepository;
        }


        public async Task<ProductDto> GetProductDtoAsync(string productId)
        {
            var productDto = await _productRepository.GetByIdAsync(productId, ProductDto.Selector);
            return productDto;
        }

        public async Task<IEnumerable<ProductDto>> GetProductsDtoAsync(IEnumerable<string> productIds)
        {
            IEnumerable<ProductDto> productsDto = await _productRepository.GetByIdAsync(productIds, ProductDto.Selector);
            return productsDto;
        }


        public async Task<IEnumerable<ProductDto>> GetProductsDtoAsync()
        {
            IEnumerable<ProductDto> productsDto = await _productRepository.GetAllAsync(ProductDto.Selector);
            return productsDto;
        }

        public async Task<IEnumerable<ProductDto>> GetProductsDtoByCategoryAsync(string categoryId)
        {
            var productsDto = await _productRepository.GetByCategoryAsync(categoryId, ProductDto.Selector);
            return productsDto;
        }

        public async Task<long> GetCountAsync()
        {
            return await _productRepository.GetCountAsync();
        }

        public async Task<bool> CreateOneAsync(CreateProductDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            bool result = await _productRepository.AddAsync(product);
            return result;
        }

        public async Task<bool> CreateManyAsync(IEnumerable<ProductDto> productsDto)
        {
            var products = _mapper.Map<IEnumerable<Product>>(productsDto);
            bool result = await _productRepository.AddAsync(products);
            return result;
        }

        public async Task<bool> DeleteAsync(string productId)
        {
            bool result = await _productRepository.DeleteAsync(productId);
            return result;
        }

        public async Task<bool> ExistsProductAsync(string productId)
        {
            var result = await _productRepository.ExistsAsync(productId);
            return result;
        }

    }
}
