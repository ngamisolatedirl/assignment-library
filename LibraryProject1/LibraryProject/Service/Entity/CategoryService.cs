using LibraryData.Models;
using LibraryData.Service.IService;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace LibraryData.Service.Entity
{
    public class CategoryService : ICategoryService
    {
        private readonly LibraryDataContext _context;
        public CategoryService(LibraryDataContext context)
        {
            _context = context;
        }
        public async Task AddCategory(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
        }

       

        public async Task DeleteCategory(int id)
        {

            var category = await _context.Categories.FindAsync(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
            }
        }

        public async Task EditCategory(Category category)
        {
            _context.Entry(category).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<List<Category>> GetAllCategories()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category> GetCategoryById(int id)
        {
            return await _context.Categories.FindAsync(id);
        }

        public bool Unique(String name)
        {
            var unique =!_context.Categories.Any(e => e.CategoryName == name);
            return unique;
        }
    }
}
