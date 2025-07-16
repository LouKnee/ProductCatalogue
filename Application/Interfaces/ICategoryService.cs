using Application.DTO;

namespace Application.Interfaces
{
    public interface ICategoryService
    {
        Task<CategoryDTO> CreateCategoryAsync(string name, string desription);
        Task<CategoryDTO> GetCategoryAsync(string name);
    }
}
