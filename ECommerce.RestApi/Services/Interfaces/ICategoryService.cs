﻿using ECommerce.RestApi.Models;

namespace ECommerce.RestApi.Services
{
    public interface ICategoryService
    {
        public Task<List<Category>> GetCategoriesAsync();
        public Task<Category> GetCategoryAsync(string categoryId);
        public Task<long> GetCategoryCountAsync();
        public Task<Category> CreateOneAsync(Category category);
        public Task<List<Category>> CreateManyAsync(List<Category> categories);
        public Task<Category> UpdateAsync(Category category);

        public Task<bool> DeleteAsync(string categoryId);

    }
}
