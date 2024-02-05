using LibraryData.Models;

namespace LibraryData.Service.IService
{
    public interface ICategoryService
    {
        Task<List<Category>> GetAllCategories();
        Task<Category> GetCategoryById(int id);
        Task AddCategory(Category category);
        Task EditCategory(Category category);
        Task DeleteCategory(int id);
        bool Unique(string ten);
    }
}
