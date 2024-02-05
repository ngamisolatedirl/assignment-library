using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibraryData.Models;
using LibraryData.Service.IService;

namespace LibraryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;
        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var books = await _bookService.GetAllBooks();
            return Ok(books);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetBook(int id)
        {
            var book = await _bookService.GetBookById(id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }
        [HttpPost]
        public async Task<IActionResult> PostBook(Book model)
        {
            var book = new Book
            {
               BookName = model.BookName,
               Photo = model.Photo,
               Author = model.Author,
               CategoryId = model.CategoryId,
            };
            await _bookService.AddBook(book);
            return Ok(book);
        }
        [HttpPut]
        public async Task<IActionResult> EditMember( int id)
        {
            var book = await _bookService.GetBookById(id);
            if (book == null)
            {
                return BadRequest();
            }
            await _bookService.EditBook(book);
            return Ok(book);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteMember(int id)
        {
            var member = await _bookService.GetBookById(id);
            if (member == null)
            {
                return BadRequest();
            }
            await _bookService.DeleteBook(id);
            return Ok(member);
        }
    }
}
