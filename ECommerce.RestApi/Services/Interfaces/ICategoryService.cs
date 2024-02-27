using ECommerce.RestApi.Models;

namespace ECommerce.RestApi.Services
{
    public interface ICategoryService
    {
        public Task<List<Category>> GetCategoriesAsync();
        public Task<long> GetCategoryCountAsync();

        public Task<Category> CreateOneAsync(Category category);
        public Task<List<Category>> CreateManyAsync(List<Category> categories);
        public Task<Category> UpdateAsync(Category category);
        public Task<Category> DeleteAsync(Category category);

    }
}
