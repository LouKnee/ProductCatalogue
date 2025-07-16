using Domain.Entities;

namespace Application.CategoryRepository
{
    public interface ICategoryRepository
    {
        Task<Category> CreateCategoryAsync(string name, string desription);
        Task<Category> GetCategoryAsync(string name);
    }
}
