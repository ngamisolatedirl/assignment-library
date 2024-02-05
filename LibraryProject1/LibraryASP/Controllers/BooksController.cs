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
    public class BooksController : Controller
    {
        private readonly LibraryDataContext _context;
        private readonly IBookService _bookService;
        private readonly ICategoryService _categoryService;

        public BooksController(LibraryDataContext context,IBookService bookService,ICategoryService categoryService)
        {
            _context = context; 
            _bookService = bookService;
            _categoryService = categoryService;
        }

        // GET: Books
        public async Task<IActionResult> Index()
        {
            var books = await _bookService.GetAllBooks();
            var categories = await _categoryService.GetAllCategories();
            ViewData["CategoryId"] = new SelectList(categories, "CategoryId", "CategoryName");
            return View(books);
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var categories = await _categoryService.GetAllCategories();
            var book = await _bookService.GetBookById(id);
            ViewData["CategoryId"] = new SelectList(categories, "CategoryId", "CategoryName");
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // GET: Books/Create
        public async Task<IActionResult> Create()
        {
           var categories = await _categoryService.GetAllCategories();
            ViewData["CategoryId"] = new SelectList(categories, "CategoryId", "CategoryName");
            return View();
        }

        // POST: Books/Create
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Book model)
        {
            
            if (ModelState.IsValid)
            {
                

                if (string.IsNullOrWhiteSpace(model.BookName))
                {
                    // Nếu tên để trống, thêm lỗi vào ModelState và hiển thị lại form
                    ModelState.AddModelError("Name", "Category name is required.");
                    return View(model);
                }

                await _bookService.AddBook(model);

                return RedirectToAction("Index"); 
            }


            var categories = await _categoryService.GetAllCategories();
            ViewData["CategoryId"] = new SelectList(categories, "CategoryId", "CategoryName");

            return View(model);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null || _context.Books == null)
            {
                return NotFound();
            }

            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName");
            return View(book);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Book book)
        {
            var categories = await _categoryService.GetAllCategories();
            if (id != book.BookId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
               
                    if (string.IsNullOrWhiteSpace(book.BookName))
                    {

                    ModelState.AddModelError("BookName", "Book name is required.");
                   
                    ViewData["CategoryId"] = new SelectList(categories, "CategoryId", "CategoryName");
                    return View(book);
                    }

                await _bookService.EditBook(book);
               

                return RedirectToAction("Index");
            }
            
            ViewData["CategoryId"] = new SelectList(categories, "CategoryId", "CategoryName", book.CategoryId);

            return View(book);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var book = await _bookService.GetBookById(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _bookService.DeleteBook(id);
            return RedirectToAction("Index");

        }

       
    }
}
