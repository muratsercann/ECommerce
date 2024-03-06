using ECommerce.RestApi.Models;
using ECommerce.RestApi.Dto;
using ECommerce.RestApi.Repositories;
using AutoMapper;

namespace ECommerce.RestApi.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(IMapper mapper, ICategoryRepository categoryRepository)
        {
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }

        public async Task<CategoryDto> GetCategoryDtoAsync(string categoryId)
        {
            CategoryDto category = await _categoryRepository.GetByIdAsync(categoryId, CategoryDto.Selector);
            return category;
        }

        public async Task<IEnumerable<CategoryDto>> GetCategoriesDtoAsync()
        {
            IEnumerable<CategoryDto> categoriesDto =
                await _categoryRepository.GetAllAsync(CategoryDto.Selector);
            return categoriesDto;
        }

        public async Task<long> GetCategoryCountAsync()
        {
            return await _categoryRepository.GetCountAsync();
        }

        public async Task<bool> CreateOneAsync(CreateCategoryDto categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);
            bool result = await _categoryRepository.AddAsync(category);
            return result;
        }

        public async Task<bool> CreateManyAsync(IEnumerable<CreateCategoryDto> categoryDtos)
        {
            IEnumerable<Category> categories = _mapper.Map<IEnumerable<Category>>(categoryDtos);
            bool result = await _categoryRepository.AddAsync(categories);
            return result;
        }

        public async Task<bool> DeleteAsync(string categoryId)
        {
            bool result = await _categoryRepository.DeleteAsync(categoryId);
            return result;
        }

        public async Task<bool> UpdateAsync(Category category)
        {
            throw new NotImplementedException();
        }
    }
}
