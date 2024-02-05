using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LibraryData.Models;
using LibraryData.Service.IService;
using LibraryData.Service.Entity;

namespace LibraryASP.Controllers
{
    public class CategoriesController : Controller
    {
        
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            
            _categoryService = categoryService;
        }

        // GET: Categories
        public async Task<IActionResult> Index()
        {
              var categories = await _categoryService.GetAllCategories();
            return View(categories);
        }

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(int id)
        {
           var category = await _categoryService.GetCategoryById(id);
            if(category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // GET: Categories/Create
        public IActionResult Create()
        {
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( Category model)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrWhiteSpace(model.CategoryName))
                {
                    // Nếu tên để trống, thêm lỗi vào ModelState và hiển thị lại form
                    ModelState.AddModelError("Name", "Category name is required.");
                    return View(model);
                }
                if (!_categoryService.Unique(model.CategoryName))
                {
                    ModelState.AddModelError("Name", "Tên đã tồn tại");
                    return View(model);
                } ;
                var category = new Category
                {
                    CategoryName = model.CategoryName,
                };
               await _categoryService.AddCategory(category);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var category = await _categoryService.GetCategoryById(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Category model)
        {
            if (id != model.CategoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (string.IsNullOrWhiteSpace(model.CategoryName))
                {
                    // Nếu tên để trống, thêm lỗi vào ModelState và hiển thị lại form
                    ModelState.AddModelError("Name", "Category name is required.");
                    return View(model);
                }
                if (!_categoryService.Unique(model.CategoryName))
                {
                    ModelState.AddModelError("Name", "Tên đã tồn tại");
                    return View(model);
                };

                await _categoryService.EditCategory(model);
                return RedirectToAction("Index");
                
              
            }
            return View(model);
        }


        
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _categoryService.GetCategoryById(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _categoryService.DeleteCategory(id);
            return RedirectToAction("Index"); 
        }
    }
}
