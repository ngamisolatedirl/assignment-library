using LibraryData.Models;
using LibraryData.Service.IService;
using Microsoft.EntityFrameworkCore;

namespace LibraryData.Service.Entity
{
    public class BookService : IBookService
    {
        private readonly LibraryDataContext _context;
        public BookService(LibraryDataContext context)
        {
            _context = context;
        }
        public async Task AddBook(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
            }
        }

        public async Task EditBook(Book book)
        {

            _context.Entry(book).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<List<Book>> GetAllBooks()
        {
            return await _context.Books.ToListAsync();
        }

        public async Task<Book> GetBookById(int id)
        {
            return await _context.Books.FindAsync(id);
        }
        public bool Unique(String name)
        {
            var unique = !_context.Books.Any(e => e.BookName == name);
            return unique;
        }
    }
}
