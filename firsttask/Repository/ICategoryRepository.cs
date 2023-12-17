using firsttask.Models;

namespace firsttask.Repository
{
    public interface ICategoryRepository
    {
        Task<List<CategoryViewModel>> GetAllCategoriesAsync(int page, int pagesize);
        Task<CategoryViewModel> GetByIdCategoryAsync(int id);
        Task<int> AddCategoryAsync(PostCategoryModel category);
        Task<bool> DeleteCategoryAsync(int id);
        Task<int> UpdateCategoryAsync(int id, PostCategoryModel category);
    }
}
