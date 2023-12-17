using firsttask.Models;
namespace firsttask.Repository
{
    public interface IProductRepository
    {
        Task<List<ProductViewModel>> GetAllProductsAsync(int page, int pagesize);
        Task<ProductViewModel> GetByIdProductAsync(int id);
        Task<int> AddProductAsync(PostProductModel product);
        Task<bool> DeleteProductAsync(int id);
        Task<int> UpdateProductAsync(int id, PostProductModel product);
    }
}
