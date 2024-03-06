using ECommerce.RestApi.Models;
using ECommerce.RestApi.Dto;

namespace ECommerce.RestApi.Services
{
    public interface ICategoryService
    {
        public Task<CategoryDto> GetCategoryDtoAsync(string categoryId);
        
        public Task<IEnumerable<CategoryDto>> GetCategoriesDtoAsync();

        public Task<long> GetCategoryCountAsync();

        public Task<bool> CreateOneAsync(CreateCategoryDto categoryDto);

        public Task<bool> CreateManyAsync(IEnumerable<CreateCategoryDto> categories);

        public Task<bool> DeleteAsync(string categoryId);

        public Task<bool> UpdateAsync(Category category);


    }
}
