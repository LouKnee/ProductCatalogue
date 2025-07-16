using Application.CategoryRepository;
using Application.DTO;
using Application.Interfaces;

namespace Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository) => _categoryRepository = categoryRepository;

        public async Task<CategoryDTO> CreateCategoryAsync(string name, string desription)
        {
            var category = await _categoryRepository.CreateCategoryAsync(name, desription);
            return new CategoryDTO
            {
                //CategoryId = category.CategoryId,
                Name = category.Name,
                Description = category.Description
            };
        }

        public async Task<CategoryDTO> GetCategoryAsync(string name)
        {
            var category = await _categoryRepository.GetCategoryAsync(name);
            return new CategoryDTO
            {
                //CategoryId = category.CategoryId,
                Name = category.Name,
                Description = category.Description
            };  
        }
    }
}
